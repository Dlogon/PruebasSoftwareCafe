using System;
using Npgsql;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Connection;
namespace Herramientas
{
    public static class Tools
    {
        public static void WriteOnlyDigits(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        public static void FillCombos(ComboBox txtCombo, string con, string id, string tabla)
        {

            QueryBuilder builder = new QueryBuilder(con);
            try
            {
                IDataReader reader = builder.returnReader(tabla, null, id);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        txtCombo.Items.Add(reader.GetString(reader.GetOrdinal(id)));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
            }
        }

        public static bool checkBoxEmptys(Control.ControlCollection cont)
        {
            foreach (Control c in cont)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (textBox.Text == string.Empty)
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        public static void setBoxemptys(Control.ControlCollection cont)
        {
            foreach (Control c in cont)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    textBox.Text = "";
                }
                if(c is ComboBox)
                {
                    ComboBox box = c as ComboBox;
                    box.Text = "";
                }
            }
        }

        public static int getFolio(string tabla, string con)
        {
            int sigue;
            IDbConnection conexion = new SqlConnection(con);
            try
            {
                
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT folio FROM " +tabla+ " order by folio desc   1";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    string len = string.Empty;
                    int i = 0;
                    sigue = Convert.ToInt32(reader.GetInt32(reader.GetOrdinal("folio")));
                    conexion.Close();
                    sigue++;
                    return sigue;
                    
                    
                }
                else
                {
                    return 1;
                }

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                return 0;
            }
            finally
            {
                conexion.Close();
            }

        }

        public static bool FirstExistenceUpdate(string idsuc,string idprod, string con)
        {
            IDbConnection conexion = new SqlConnection(con);
            try
            {
               
                       
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT * FROM existencia WHERE sucursal ='" + idsuc + "' AND producto ='"+ idprod+"';";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

    }

    public static class SucursalValida
    {
        
        public static string Valid(string id, string con)
        {
            IDbConnection conexion = new SqlConnection(con);
            try
            {
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT direccion FROM SUCURSAL WHERE idsucursal ='"+id+"';";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetString(reader.GetOrdinal("direccion"));
                }
                else
                {
                    return null;
                }

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
                return null;
            }
            finally
            {
                conexion.Close();
            }

        } 
    }

    public static class ProductoValida
    {

        public static bool Valid(string id, string con)
        {
            IDbConnection conexion = new SqlConnection
                        (con);
            try
            {
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT idproducto FROM producto WHERE idproducto ='" + id + "';";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                   
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }

        }
    }
    public static class proveedorValida
    {
        public static string Valid(string id, string con)
        {
            IDbConnection conexion = new SqlConnection
                        (con);
            try
            {
                
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT nombre FROM proveedor WHERE idproveedor ='" + id + "';";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetString(reader.GetOrdinal("nombre"));
                }
                else
                {
                    return null;
                }

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
                return null;
            }
            finally
            {
                conexion.Close();
            }

        }
    }
    public static class clienteValida
    {
        public static string Valid(string id, string con)
        {
            IDbConnection conexion = new SqlConnection
                        (con);
            try
            {
                
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT nombre FROM cliente WHERE idcliente ='" + id + "';";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetString(reader.GetOrdinal("nombre"));
                }
                else
                {
                    return null;
                }

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
                return null;
            }
            finally
            {
                conexion.Close();
            }

        }
    }
    public static class empleadoValida
    {
        public static string Valid(string id, string con)
        {
            IDbConnection conexion = new SqlConnection
                        (con);
            try
            {
                
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT nombre FROM EMPLEADO WHERE idempleado ='" + id + "';";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    conexion.Close();
                    return reader.GetString(reader.GetOrdinal("nombre"));
                    
                }
                else
                {
                    conexion.Close();
                    return null;
                }

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
                return null;
            }
            finally
            {
                conexion.Close();
            }

        }
    }


}
