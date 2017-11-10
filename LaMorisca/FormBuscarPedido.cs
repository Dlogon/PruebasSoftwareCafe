using Herramientas;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connection;
namespace LaMorisca
{
    public partial class FormBuscarPedido : Form
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
        private QueryBuilder builder;
        public FormBuscarPedido()
        {
            InitializeComponent();
            builder = new QueryBuilder(Program.conexion);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bool encontro = false;
            dtaview.DataSource = null;
            dtaview.ColumnCount = 0;
            DataTable datos = new DataTable();
            DataTable datos1 = new DataTable();
            datos1.Columns.Add("Producto Id");
            datos1.Columns.Add("Nombre");
            datos1.Columns.Add("Cantidad");
            datos1.Columns.Add("Precio");
            datos1.Columns.Add("Importe");

            try
            {
                
                IDbConnection conexion = new SqlConnection
                            (Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT * FROM pedido  "
                    + " INNER JOIN proveedor on proveedor.idproveedor=pedido.proveedor INNER JOIN "+
                    "sucursal on sucursal.idsucursal=pedido.sucursal   where folio=" + txtfolio.Text + ";";

                IDataReader read = dbcmd.ExecuteReader();
                while (read.Read())
                {
                    txtProveedor.Text = read.GetString(read.GetOrdinal("idproveedor"));
                    txtNombreProv.Text = read.GetString(read.GetOrdinal("nombre"));
                   // DateTime t = read.GetDateTime(read.GetOrdinal("fecha"));
                    txtfecha.Text = read.GetString(read.GetOrdinal("fecha"));
                    txtIdSucursal.Text = read.GetString(read.GetOrdinal("sucursal"));
                    txtNombreSucursal.Text = read.GetString(read.GetOrdinal("direccion"));

                    SqlDataAdapter cons = new SqlDataAdapter(
                        "SELECT  producto as IDproducto, producto.nombre,  cantidad, pedidodetalle.precio, cantidad*pedidodetalle.precio as importe " +
                        " FROM pedidodetalle INNER JOIN producto on producto.idproducto=pedidodetalle.producto " +
                        " where folio=" + txtfolio.Text + "", Program.conexion)
                    ;
                    cons.Fill(datos);
                    dtaview.DataSource = datos.DefaultView;
                    conexion.Close();
                    dtaview.Columns[3].ValueType = typeof(String);

                    dtaview.Columns[0].ReadOnly = true;
                    dtaview.Columns[1].ReadOnly = true;
                    dtaview.Columns[2].ReadOnly = true;
                    dtaview.Columns[3].ReadOnly = true;
                    dtaview.Columns[4].ReadOnly = true;
                    foreach (DataGridViewRow row in dtaview.Rows)
                    {
                        datos1.Rows.Add(
                            row.Cells[0].Value, row.Cells[1].Value,
                            row.Cells[2].Value,
                            row.Cells[3].Value, row.Cells[4].Value
                            );
                    }
                    datos1.Rows.RemoveAt(datos1.Rows.Count - 1);
                    dtaview.DataSource = datos1.DefaultView;
                    dtaview.Columns[4].ReadOnly = true;
                    // dtaview.Columns.Add("Importe", "Importe");
                    decimal subtotal = 0;
                    for (int k = 0; k < dtaview.RowCount - 1; k++)
                    {
                        //dtaview.Rows[k].Cells[4].Value = Convert.ToInt32(dtaview.Rows[k].Cells[2].Value) * Convert.ToDecimal(dtaview.Rows[k].Cells[3].Value);
                        subtotal += Convert.ToInt32(dtaview.Rows[k].Cells[2].Value) * Convert.ToDecimal(dtaview.Rows[k].Cells[3].Value);
                    }

                    for (int k = 0; k < dtaview.RowCount - 1; k++)
                    {
                        // dtaview.Rows[k].Cells[3].ValueType = typeof(Object);
                        dtaview.Rows[k].Cells[3].Value = dtaview.Rows[k].Cells[3].Value.ToString() + "$";
                        dtaview.Rows[k].Cells[4].Value = dtaview.Rows[k].Cells[4].Value.ToString() + "$";
                    }
                    txtsubtotal.Text = subtotal.ToString() + "$";
                    txtIva.Text = Convert.ToString(Convert.ToDouble(txtsubtotal.Text.Substring(0, txtsubtotal.Text.Length - 1)) * .16) + "$";
                    txtTotal.Text = Convert.ToString(Convert.ToDouble(txtsubtotal.Text.Substring(0, txtsubtotal.Text.Length - 1)) * 1.16) + "$";
                    btnImprimir.Enabled = true;
                    // dtaview.Columns[5].ReadOnly = true;
                    encontro = true;
                }
                if (!encontro)
                {
                    MessageBox.Show("No se encuentra el registro");
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
        Font fuente = new Font("Arial", 12);
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            PrintDocument formulario = new PrintDocument();
            formulario.PrintPage += new PrintPageEventHandler(Datos);
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = formulario;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                formulario.Print();
            }
        }

        private void Datos(object obj, PrintPageEventArgs ev)
        {
           
            int a;
            float b = 395;
            float pos_x = 10;
            float pos_y = 40;
            Image logo = (Image)resources.GetObject("$this.BackgroundImage");
            ev.Graphics.DrawImage(logo, 80, 50, 110, 95);
            
            ev.Graphics.DrawString("Folio: " + txtfolio.Text + "", fuente, Brushes.Black, pos_x + 625, pos_y + 10, new StringFormat());
            ev.Graphics.DrawString("Fecha: " + txtfecha.Text + "", fuente, Brushes.Black, pos_x + 625, pos_y + 30, new StringFormat());
            ev.Graphics.DrawString("LA MORISCA cafe", fuente, Brushes.Black, pos_x + 200, pos_y, new StringFormat());
            ev.Graphics.DrawString("Guadalajara, Jalisco", fuente, Brushes.Black, pos_x + 200, pos_y + 20, new StringFormat());
            ev.Graphics.DrawString("    ", fuente, Brushes.Black, pos_x + 200, pos_y + 40, new StringFormat());
            ev.Graphics.DrawString("Pedido para la sucursal: "+txtNombreSucursal.Text, fuente, Brushes.Black, pos_x-20 + 200, pos_y + 80, new StringFormat());
            ev.Graphics.DrawString("De el proveedor: " + txtNombreProv.Text + "", fuente, Brushes.Black, pos_x + 40, pos_y + 100, new StringFormat());

            ev.Graphics.DrawString("Producto \nid ", fuente, Brushes.Black, pos_x + 40, pos_y + 310, new StringFormat());
            ev.Graphics.DrawString("Producto: ", fuente, Brushes.Black, pos_x + 125, pos_y + 310, new StringFormat());
            ev.Graphics.DrawString("Cantidad: ", fuente, Brushes.Black, pos_x + 450, pos_y + 310, new StringFormat());
            ev.Graphics.DrawString("Precio: ", fuente, Brushes.Black, pos_x + 580, pos_y + 310, new StringFormat());
            ev.Graphics.DrawString("Importe ", fuente, Brushes.Black, pos_x + 680, pos_y + 310, new StringFormat());
            for (a = 0; a < dtaview.RowCount-1; a++)
            {
                ev.Graphics.DrawString("" + dtaview.Rows[a].Cells[0].Value + "", fuente, Brushes.Black, pos_x + 40, b, new StringFormat());
                ev.Graphics.DrawString("" + dtaview.Rows[a].Cells[1].Value + "", fuente, Brushes.Black, pos_x + 125, b, new StringFormat());
                ev.Graphics.DrawString("" + dtaview.Rows[a].Cells[2].Value + "", fuente, Brushes.Black, pos_x + 450, b, new StringFormat());
                ev.Graphics.DrawString("" + dtaview.Rows[a].Cells[3].Value + "", fuente, Brushes.Black, pos_x + 580, b, new StringFormat());
                ev.Graphics.DrawString("" + dtaview.Rows[a].Cells[4].Value + "", fuente, Brushes.Black, pos_x + 680, b, new StringFormat());
                b = b + 30;
            }

            ev.Graphics.DrawString("SUBTOTAL: " + txtsubtotal.Text+ "", fuente, Brushes.Black, pos_x + 500, 930, new StringFormat());
            ev.Graphics.DrawString("IVA:      " + txtIva.Text + "", fuente, Brushes.Black, pos_x + 500, 950, new StringFormat());
            ev.Graphics.DrawString("TOTAL:    " + txtTotal.Text + "", fuente, Brushes.Black, pos_x + 500, 970, new StringFormat());
        }

        private void FormBuscarPedido_Load(object sender, EventArgs e)
        {

        }

        private void txtfolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }
    }
}
