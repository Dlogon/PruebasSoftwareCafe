using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Connection;
namespace Herramientas
{
    public static class ModifyTools
    {
        public static bool ModifyCliente(string con,string id, string nombre, string direccion, string telefono)
        {
            try
            {
               
                 SqlConnection conexion = new  SqlConnection
                        (con);
                conexion.Open();
                
                SqlCommand cmd = new SqlCommand
                    (
                        "update cliente set " +
                        "nombre= '" + nombre + "' ," +
                        "direccion = '" + direccion + "', " +
                        "telefono = '" + telefono + "' " +
                        "WHERE idcliente= '" + id + "';", conexion
                    );
                if (MessageBox.Show(
                    "Seguro que desea modificar el cliente con el id" + id, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == DialogResult.Yes
                    )
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro Modificado");
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool ModifyEmpleadoe(string con, string id, string nombre,string apellido ,  string direccion, string telefono, string sucursal)
        {
            try
            {
                 SqlConnection conexion = new  SqlConnection
                        (con);
                conexion.Open();

                SqlCommand cmd = new SqlCommand
                    (
                        "update empleado set " +
                        "nombre= '" + nombre + "' ," +
                        "direccion = '" + direccion + "', " +
                        "telefono = '" + telefono + "' ," +
                        "apellido = '" + apellido + "' ," +
                        "idsucursal = '" + sucursal + "' " +
                        "WHERE idempleado= '" + id + "';", conexion
                    );
                if (MessageBox.Show(
                    "Seguro que desea modificar el empleado con el id" + id, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == DialogResult.Yes
                    )
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro Modificado");
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static bool ModifyProduct(string con, string id, string nombre, int precio, string detalles, string proveedor)
        {
            try
            {
                 SqlConnection conexion = new  SqlConnection
                        (con);
                conexion.Open();

                SqlCommand cmd = new SqlCommand
                    (
                        "update producto set " +
                        "nombre= '" + nombre + "' ," +
                        "precio = " + precio + ", " +
                        "detalle = '" + detalles + "' ," +
                        "proveedor = '" + proveedor + "' ," +
                        "WHERE idproducto= '" + id + "';", conexion
                    );
                if (MessageBox.Show(
                    "Seguro que desea modificar el producto con el id " + id, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == DialogResult.Yes
                    )
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro Modificado");
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static bool ModifyProveedor(string con, string id, string nombre, string ciudad, string estado, string telefono, string direccion, int tiempo)
        {
            try
            {
                 SqlConnection conexion = new  SqlConnection
                        (con);
                conexion.Open();

                SqlCommand cmd = new SqlCommand
                    (
                        "update Proveedor set " +
                        "nombre= '" + nombre + "' ," +
                        "tiempoentrega = " + tiempo + ", " +
                        "ciudad = '" + ciudad + "' ," +
                        "estado = '" + estado + "' ," +
                        "telefono = '" + telefono + "' ," +
                        "direccion = '" + direccion + "' ," +
                        "WHERE idproveedor= '" + id + "';", conexion
                    );
                if (MessageBox.Show(
                    "Seguro que desea modificar a el proveedor con el id " + id, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == DialogResult.Yes
                    )
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro Modificado");
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool ModifySucursal(string con, string id, string direccion, string ciudad, string estado, string telefono)
        {
            try
            {
                 SqlConnection conexion = new  SqlConnection
                        (con);
                conexion.Open();

                SqlCommand cmd = new SqlCommand
                    (
                        "update SUCURSAL set " +
                        "direccion= '" + direccion + "' ," +
                        "ciudad = '" + ciudad + "' ," +
                        "estado = '" + estado + "' ," +
                        "telefono = '" + telefono + "' ," +
                        "WHERE idsucursal= '" + id + "';", conexion
                    );
                if (MessageBox.Show(
                    "Seguro que desea modificarla sucursal con el id " + id, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == DialogResult.Yes
                    )
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro Modificado");
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }

    public static class DeleteParameters
    {
        public static bool LogicDelete(QueryBuilder builder, string primaryKey, string primaryKeyName,  string table, string nameOfLogicBool)
        {
            try
            {
                return builder.updateTable(table, "WHERE " + primaryKeyName + "=" + primaryKey, 
                    new string[] { nameOfLogicBool }, "1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        [Obsolete("Se implementa borrado logico")]
        public static string DeletewhitString(string con, string table, string column, string tipe, string id)
        {
            try
            {
                 SqlConnection conexion = new  SqlConnection
                            (con);
                conexion.Open();
                SqlCommand cmd = new SqlCommand
                    (
                        "DELETE FROM " + table +  " WHERE " + column  + " = '" + id + "';", conexion
                    );
                if (MessageBox.Show(
                    "Seguro que desea Eliminar el "+tipe+" con el identificador " +id, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
                    == DialogResult.Yes
                    )
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro Eliminado");
                    return "OK";
                }
                return "NO";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
            finally
            {
                
            }
            
        }
    }
    
}
