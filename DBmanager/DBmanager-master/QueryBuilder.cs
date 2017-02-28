using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Connection
{
    /// <summary>
    /// Crea Todas las interacciones con la base de datos
    /// </summary>
    public sealed class QueryBuilder
    {
        /// <summary>
        /// Cadena de conexion
        /// </summary>
        private string conString;
        private SqlConnection conn;
        private SqlCommand command;
        /// <summary>
        /// Constructor que define la cadena de conexión
        /// </summary>
        /// <param name="conString">cadena de conexion</param>
        public QueryBuilder(string conString)
        {
            this.conString = conString;
        }
        /// <summary>
        /// Abre la conexion a la base de datos y prepara el query a ejecutar
        /// </summary>
        private void openConnection()
        {
            conn = new SqlConnection(conString);
            conn.Open();
            command = new SqlCommand();
            command = conn.CreateCommand();
            command.CommandType = CommandType.Text;
        }
        /// <summary>
        /// Retorna un solo campo de una tabla, el primero que encuentre
        /// </summary>
        /// <param name="table">tabla a hacer el query</param>
        /// <param name="field">campo requerido</param>
        /// <param name="conditions">Condiciones de la consulta</param>
        /// <returns>Retorna el campo requerido en el parametro field, si no se encuentra retorna cadena vacia</returns>
        public string getField(string table, string field, string conditions)
        {
            
            try
            {
                openConnection();
                command.CommandText = "SELECT " + field + " FROM " + table + " "+ conditions;
                if (command.ExecuteNonQuery() <= 0)
                    return null;
                else
                    return Convert.ToString(command.ExecuteScalar());
            }
            catch(SqlException e)
            {
                MessageBox.Show("ERROR EN LA OPERACION oledb " + e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch(Exception e)
            {
                MessageBox.Show("ERROR EN LA OPERACION gen " + e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if(conn!=null)
                    conn.Close();
            }
        }

        /// <summary>
        /// Retorna un Diccionario asociativo de los campos, agrega al final de cada llave un contador, que depende del 
        /// numero de filas encontradas
        /// </summary>
        /// <param name="table">tabla a hacer el query</param>
        /// <param name="fields">campos requeridos</param>
        /// <param name="conditions">Condiciones de la consulta</param>
        /// <returns>un diccionario, agrega al final de cada llave un contador, que depende del 
        /// numero de filas encontradas</returns>
        public Dictionary<string, string> getField(string table, string conditions, params string[] fields)
        {
            try
            {
                openConnection();
                string campos = string.Join(" , ", fields);
                command.CommandText = "SELECT " + campos + " FROM " + table + " " + conditions;
                if (command.ExecuteNonQuery() <= 0)
                    return null;
                IDataReader reader = command.ExecuteReader();
                int i=0, k=0;
                Dictionary<string, string> ret = new Dictionary<string, string>();
                while (reader.Read())
                {
                    i = 0;
                    while (i < fields.Length)
                    {
                        ret.Add(fields[i].Trim()+k.ToString(), Convert.ToString(reader.GetValue(i)));
                        i++;
                    }
                    k++;
                }
                
                return ret;
            }
            catch (SqlException e)
            {
                MessageBox.Show("ERROR EN LA OPERACION oledb" + e.Message + e.ErrorCode, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
            catch (Exception e)
            {
                MessageBox.Show("ERROR EN LA OPERACION gen " + e.GetBaseException() + e.Message + e.Data, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (conn != null)
                        conn.Close();
            }
        }
        
        public IDataReader returnReader(string table, string conditions, params string[] fields)
        {
            openConnection();
            string campos = string.Join(" , ", fields);
            command.CommandText = "SELECT " + campos + " FROM " + table + " " + conditions;
            if (command.ExecuteNonQuery() <= 0)
                return null;
            return command.ExecuteReader();
        }

        public bool insertFields(string table, params string[] fields)
        {
            openConnection();
            string campos = string.Join(" , ", fields);
            command.CommandText = "INSERT INTO "+ table + " VALUES ( " + campos + " )";
            if (command.ExecuteNonQuery() <= 0)
                return false;
            return true;
        }
    }
}

