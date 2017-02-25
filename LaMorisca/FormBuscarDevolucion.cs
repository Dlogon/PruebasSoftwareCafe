using Herramientas;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaMorisca
{
    public partial class FormBuscarDevolucion : Form
    {
        public FormBuscarDevolucion()
        {
            InitializeComponent();
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
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
                dbcmd.CommandText = "SELECT folio, fecha, causas, autoriza, empleado.nombre as auth, devolucion.cliente, cliente.nombre as cli, folioventa FROM devolucion "+
                    " INNER JOIN empleado on empleado.idempleado=autoriza INNER JOIN cliente on cliente.idcliente=devolucion.cliente " +
                    
                   " where folio=" + txtfolio.Text + ";";

                IDataReader read = dbcmd.ExecuteReader();
                while (read.Read())
                {
                    txtCausa.Text = read.GetString(read.GetOrdinal("causas"));
                    DateTime t = read.GetDateTime(read.GetOrdinal("fecha"));
                    txtfecha.Text = t.Day + "/" + t.Month + "/" + t.Year;
                    txtempleado.Text = read.GetString(read.GetOrdinal("autoriza"));
                    txtCliente.Text = read.GetString(read.GetOrdinal("cliente"));
                    txtnombrecliente.Text = read.GetString(read.GetOrdinal("cli"));
                    txtnombreempleado.Text = read.GetString(read.GetOrdinal("auth"));
                    txtFolioVenta.Text = Convert.ToString(read.GetInt32(read.GetOrdinal("folioventa")));

                    NpgsqlDataAdapter cons = new NpgsqlDataAdapter(
                        "SELECT producto as IDproducto, producto.nombre,  cantidad " +
                        " FROM detalledevolucion INNER JOIN producto on producto.idproducto=detalledevolucion.producto " +
                        " where folio=" + txtfolio.Text + "", Program.conexion)
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

        private void txtfolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }
    }
}
