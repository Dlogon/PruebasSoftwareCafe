using Herramientas;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connection;
namespace LaMorisca
{
    public partial class FormBuscarProducto : Form
    {
        private QueryBuilder builder;
        public FormBuscarProducto()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool encontro = false;
            try
            {
                IDbConnection conexion = new SqlConnection
                            (Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT *, proveedor.nombre as nompov FROM PRODUCTO " +
                    " INNER JOIN proveedor on proveedor.idproveedor = producto.proveedor " +
                    "where idproducto='" +txtIProducto.Text + "';";

                IDataReader read = dbcmd.ExecuteReader();
                while (read.Read())
                {
                   txtNombre.Text = read.GetString(read.GetOrdinal("Nombre"));
                    txtDetalles.Text = read.GetString(read.GetOrdinal("detalle"));
                    txtPrecioList.Text = Convert.ToString(read.GetDecimal(read.GetOrdinal("preciolista")))+"$";
                    txtPrecioPub.Text = Convert.ToString(read.GetDecimal(read.GetOrdinal("preciopub")))+"$";
                    txtIdProveedor.Text = read.GetString(read.GetOrdinal("proveedor"));
                    txtnomproveedor.Text = read.GetString(read.GetOrdinal("nompov"));
                   // txtexistencia.Text = Convert.ToString(read.GetInt32(read.GetOrdinal("cant")));
                    btnEditar.Enabled = true;
                    encontro = true;

                    dbcmd.CommandText = " SELECT SUM(cantidad) as cant from existencia where  producto = '" + txtIProducto.Text + "';";
                    IDataReader read1 = dbcmd.ExecuteReader();
                    while(read1.Read())
                        txtexistencia.Text = Convert.ToString(read1.GetValue(read1.GetOrdinal("cant")));
                }
                if (!encontro)
                {
                    MessageBox.Show("No se encuentra el registro");
                    Tools.setBoxemptys(Controls);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
        }

        private void FormBuscarProducto_Load(object sender, EventArgs e)
        {
            btnmodificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEliminar.Enabled = true;
            btnmodificar.Enabled = true;
            txtIProducto.Enabled = false;
            txtDetalles.Enabled = true;
            txtIdProveedor.Enabled = true;
            txtNombre.Enabled = true;
            txtPrecioList.Enabled = true;
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            if (proveedorValida.Valid(txtIdProveedor.Text, Program.conexion)!=null)
            {
                if (!ModifyTools.ModifyProduct(Program.conexion, txtIProducto.Text, txtNombre.Text, Convert.ToInt32(txtPrecioList.Text), txtDetalles.Text, txtIdProveedor.Text))
                    MessageBox.Show("Ocurrio un error al modificar el registro");
                else
                {
                    Tools.setBoxemptys(Controls);
                    btnEliminar.Enabled = false;
                    btnmodificar.Enabled = false;
                    txtIProducto.Enabled = true;
                    txtDetalles.Enabled = false;
                    txtIdProveedor.Enabled = false;
                    txtNombre.Enabled = false;
                    txtPrecioList.Enabled = false;
                    txtIProducto.Focus();
                }
            }
            else
                Program.retornarError("El proveedor no existe", "ErrOr");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string info = DeleteParameters.DeletewhitString(Program.conexion, "Producto", "idproducto", "producto", txtIProducto.Text);
            if (info.Substring(0, 12) == "ERROR: 23503")
                MessageBox.Show("Este producto ya esta en una transaccion(venta, devolucion, movimiento), por lo que no es posible borrarlo");
            else
            {
                Tools.setBoxemptys(Controls);
                btnEliminar.Enabled = false;
                btnmodificar.Enabled = false;
                txtIProducto.Enabled = true;
                txtDetalles.Enabled = false;
                txtIdProveedor.Enabled = false;
                txtNombre.Enabled = false;
                txtPrecioList.Enabled = false;
                txtIProducto.Focus();
            }
        }

        private void btnexisporsuc_Click(object sender, EventArgs e)
        {
            new Formexistencias(txtIProducto.Text).Show();
        }
    }
}
