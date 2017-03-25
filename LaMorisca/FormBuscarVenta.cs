using Herramientas;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connection;
namespace LaMorisca
{
    public partial class FormBuscarVenta : Form
    {
        QueryBuilder builder;
        public FormBuscarVenta()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
        }
        private string encontroFolio;
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
                dbcmd.CommandText = "SELECT *, empleado.nombre as emp FROM Venta  "
                    + " INNER JOIN empleado on empleado.idempleado=venta.realizo INNER JOIN " +
                    "cliente on cliente.idcliente=venta.cliente where folio=" + txtfolio.Text + ";";

                IDataReader read = dbcmd.ExecuteReader();
                while (read.Read())
                {
                    txtStatus.Text = read.GetString(read.GetOrdinal("status"));
                    DateTime t = read.GetDateTime(read.GetOrdinal("fecharealizada"));
                    txtfechaR.Text = t.Day + "/" + t.Month + "/" + t.Year;
                    if (txtStatus.Text.Equals("No entregada"))
                        txtFechaEn.Text = "";
                    else
                        txtFechaEn.Text = read.GetDateTime(read.GetOrdinal("fechaentrega")).ToShortDateString();
                    txtEmpleado.Text = read.GetString(read.GetOrdinal("realizo"));
                    txtCliente.Text = read.GetString(read.GetOrdinal("cliente"));
                    txtNombreCliente.Text = read.GetString(read.GetOrdinal("nombre"));
                    txtNombreEmpleado.Text = read.GetString(read.GetOrdinal("emp"));
                    
                    if (txtStatus.Text.Equals("Entregada") || txtStatus.Text.Equals("Con devolucion"))
                    {
                        btnentregada.Enabled = false;
                    }
                    else
                        btnentregada.Enabled = true;
                    encontro = true;
                    encontroFolio = txtfolio.Text;
                    NpgsqlDataAdapter cons = new NpgsqlDataAdapter(
                        "SELECT  producto as IDproducto, producto.nombre,  cantidad, detalleventa.precio, cantidad*detalleventa.precio as importe " +
                        " FROM detalleventa INNER JOIN producto on producto.idproducto=detalleventa.producto " +
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
                    Tools.setBoxemptys(Controls);
                    dtaview.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnentregada_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea marcar la venta como entregada", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conexion = new SqlConnection
                                (Program.conexion);
                conexion.Open();
                IDbCommand comando = conexion.CreateCommand();
                comando.CommandText = "update venta set status='Entregada', fechaentrega ='" + DateTime.Now.ToShortDateString() + " 'where folio=" + encontroFolio + ";";
                comando.ExecuteNonQuery();
                comando.CommandText= "Update cliente set saldo = saldo -" + txtTotal.Text.Substring(0, txtTotal.Text.Length - 1) + "where idcliente='" + txtCliente.Text + "'";
                comando.ExecuteNonQuery();
                btnentregada.Enabled = false;
                MessageBox.Show("Se entrego la venta con el folio " + encontroFolio +"se restaron "+ txtTotal.Text + "al saldo del cliente", "ENTREGADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            
            int tam = dtaview.Height;
            Point plbltot =lblTot.Location;
            Point plblsub = lblSub.Location;
            Point plbliva = lblIva.Location;
            Point ptxttot = txtTotal.Location;
            Point ptxtiva = txtIva.Location;
            Point ptxtsub = txtsubtotal.Location;
            Size _this= Size;
            dtaview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtaview.Height = dtaview.RowCount * 25;
            lblTot.Location = new Point(lblTot.Location.X, lblTot.Location.Y + dtaview.Height-tam);
            lblSub.Location = new Point(lblSub.Location.X, lblSub.Location.Y + dtaview.Height - tam);
            lblIva.Location = new Point(lblIva.Location.X, lblIva.Location.Y + dtaview.Height - tam);
            txtTotal.Location = new Point(txtTotal.Location.X, txtTotal.Location.Y + dtaview.Height - tam);
            txtIva.Location = new Point(txtIva.Location.X, txtIva.Location.Y + dtaview.Height - tam);
            txtsubtotal.Location = new Point(txtsubtotal.Location.X, txtsubtotal.Location.Y + dtaview.Height - tam);
            Height = Height + (dtaview.Height-tam);
            btnRegresar.Hide();
            btnAgregar.Hide();
            btnentregada.Hide();
            btnImprimir.Hide();

            PrintDocument formulario = new PrintDocument();
            formulario.PrintPage += new PrintPageEventHandler(Datos);
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = formulario;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                formulario.Print();
            }
            dtaview.Height = 7 * 20;
            Size = _this;
            lblTot.Location = plbltot;
            lblSub.Location = plblsub;
            lblIva.Location = plbliva;
            txtTotal.Location = ptxttot;
            txtIva.Location = ptxtiva;
            txtsubtotal.Location = ptxtsub;
            btnRegresar.Show();
            btnAgregar.Show(); ;
            btnentregada.Show();
            btnImprimir.Show();

        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private Bitmap memoryImage;
        ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
        private void Datos(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            Bitmap b = new Bitmap(Application.StartupPath + "\\imageinfo\\venta.png");
            // Image logo = new Bitmap(Application.StartupPath + "\\imageinfo\\venta"); 
            
            Graphics mygraphics = CreateGraphics();
            Size s = Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
            e.Graphics.DrawImage(b, 0, 0, s.Width, 200);
            e.Graphics.DrawImage(memoryImage, 0,200);
        }

        private void txtfolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }
    }
}
