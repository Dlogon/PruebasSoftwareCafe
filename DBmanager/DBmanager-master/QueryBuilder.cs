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
        private bool openConnection()
        {
            try
            {
                conn = new SqlConnection(conString);
                command = new SqlCommand();
                command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                conn.Open();

                return true;
            }
            catch(SqlException e)
            {
                if (e.Number == 53)
                {
                    MessageBox.Show("Parece que no estas conectado a internet, o la red se encuentra congestionada\n"+
                         "Asegurate de tener una conexion a internet, si el problema persiste contacta a sistemas"
                         , "ERROR "+e.Number,
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Error en la operacion, contacte a sistemas e informe el siguiente mensaje:\n" + e.Message, e.Number.ToString(),
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch(Exception e)
            {
                MessageBox.Show("Error en la operacion, contacte a sistemas e informe el siguiente mensaje:\n" +
                    "\n Exception" + e.InnerException  +"\n"+ e.Message, "Error",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

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
                if (openConnection())
                {
                    command.CommandText = "SELECT " + field + " FROM " + table + " " + conditions;
                    command.Parameters.Add(new SqlParameter("@" + field, field));
                    string ob = Convert.ToString(command.ExecuteScalar());
                    if (ob=="")
                        return null;
                    else
                        return ob;
                }
                else
                    throw new Exception();
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
                if (openConnection())
                {

                    string campos = string.Join(" , ", fields);
                    command.CommandText = "SELECT " + campos + " FROM " + table + " " + conditions;
                    if (command.ExecuteNonQuery() <= 0)
                        return null;
                    IDataReader reader = command.ExecuteReader();
                    int i = 0, k = 0;
                    Dictionary<string, string> ret = new Dictionary<string, string>();
                    while (reader.Read())
                    {
                        i = 0;
                        while (i < fields.Length)
                        {
                            ret.Add(fields[i].Trim() + k.ToString(), Convert.ToString(reader.GetValue(i)));
                            i++;
                        }
                        k++;
                    }

                    return ret;
                }
                else
                    throw new Exception();
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
            if(openConnection())
            {
                string campos = string.Join(" , ", fields);
                command.CommandText = "SELECT " + campos + " FROM " + table + " " + conditions;
                SqlDataReader reader= command.ExecuteReader();
                if (!reader.HasRows)
                    return null;
                return reader;
            }
            return null;
        }

        public bool insertFields(string table, params string[] fields)
        {
            if (openConnection())
            {
                string[] parameters = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++)
                    parameters[i] = "@param" + i;
                string campos = string.Join(" , ", parameters);
                command.CommandText = "INSERT INTO " + table + " VALUES ( " + campos + " )";
                for (int i = 0; i < fields.Length; i++)
                    command.Parameters.Add(new SqlParameter("@param"+i,fields[i]));
                if (command.ExecuteNonQuery() <= 0)
                    return false;
                return true;
            }
            return false;
        }
        [Obsolete("Metodo inseguro, puede permitir inyeccion de codigo!.")]
        public int queryejecutor(string query)
        {
            if (openConnection())
            {
                command.CommandText = query;
                return command.ExecuteNonQuery();
            }
            return -1;
            
        }
    }
}

