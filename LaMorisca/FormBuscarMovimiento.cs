using Herramientas;
using Npgsql;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaMorisca
{
    public partial class FormBuscarMovimiento : Form
    {
        public FormBuscarMovimiento()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            bool encontro = false;
            dtaview.DataSource = null;
            dtaview.ColumnCount = 0;
            DataTable datos = new DataTable();
            dtaview.DataSource = datos.DefaultView;
            try
            {
                IDbConnection conexion = new SqlConnection(Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT folio, causa, fecha, sucursalorigen, o.direccion as origen, sucursaldestino, d.direccion as destino, authempleado, empleado.nombre as auth "+
                     "   FROM movimientoinventario" +
                    " INNER JOIN sucursal as  o on o.idsucursal=sucursalorigen INNER JOIN sucursal  d on d.idsucursal=sucursaldestino INNER JOIN empleado on empleado.idempleado = authempleado " +
                    "where folio=" +txtFolio.Text + ";";

                IDataReader read = dbcmd.ExecuteReader();
                while (read.Read())
                {
                    txtCausa.Text = read.GetString(read.GetOrdinal("causa"));
                    DateTime t = read.GetDateTime(read.GetOrdinal("fecha"));
                    txtFecha.Text = t.Day + "/" + t.Month + "/" + t.Year;
                    txtOrigen.Text= read.GetString(read.GetOrdinal("sucursalorigen"));
                    txtDestino.Text= read.GetString(read.GetOrdinal("sucursaldestino"));
                    txtEmpleado.Text = read.GetString(read.GetOrdinal("authempleado"));
                    txtnomOri.Text = read.GetString(read.GetOrdinal("origen"));
                    txtNomDest.Text = read.GetString(read.GetOrdinal("destino"));
                    txtNomEmpleado.Text = read.GetString(read.GetOrdinal("auth"));
                    conexion.Close();
                    NpgsqlDataAdapter cons = new NpgsqlDataAdapter(
                        "SELECT producto as IDproducto, producto.nombre,  cantidad " +
                        " FROM detallemov INNER JOIN producto on producto.idproducto=detallemov.producto " +
                        " where folio=" + txtFolio.Text + "", Program.conexion)
                    ;
                    cons.Fill(datos);
                    encontro = true;
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

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }
    }
}
