using Herramientas;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Connection;

namespace LaMorisca
{
    public partial class FormAgregarProducto : Form
    {
        private QueryBuilder builder;
        public FormAgregarProducto()
        {
            InitializeComponent();
            builder = new QueryBuilder(Program.conexion);
        }

        private void FormAgregarProducto_Load(object sender, EventArgs e)
        {
            Tools.FillCombos(txtProveedor, Program.conexion, "idproveedor", "proveedor");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtIProducto.Text == "")
            {
                Program.retornarError("falta el campo ID producto", "Error");
            }
            else if (proveedorValida.Valid(txtProveedor.Text, Program.conexion)==null)
                Program.retornarError("El proveedor no existe", "Provedor no valido");
            else
            {
                try
                {
                    IDbConnection conexion = new SqlConnection(Program.conexion);
                    conexion.Open();
                    IDbCommand comando = conexion.CreateCommand();
                    comando.CommandText =
                        "INSERT INTO PRODUCTO VALUES('" + txtIProducto.Text + "'," +
                        "" +numPrecioPublic.Value + ", " +
                        "'" + txtDetalles.Text + "', " +
                        "'" + txtNombre.Text + "', " +
                        "'" + txtProveedor.Text +"',"+
                        "" + numPreciolista.Value + "); ";

                    builder.insertFields("PRODUCTO", new string[] {
                        "'" + txtIProducto.Text + "'",
                        (numPrecioPublic.Value).ToString(),
                        "'" + txtDetalles.Text + "'",
                        "'" + txtNombre.Text + "'",
                        "'" + txtProveedor.Text +"'",
                        (numPreciolista.Value).ToString()
                    }
                    );
                    MessageBox.Show("Registro Guardado correctamente...");
                    Tools.setBoxemptys(Controls);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void txtIdProveedor_Leave(object sender, EventArgs e)
        {
            if (proveedorValida.Valid(txtProve.Text, Program.conexion)==null)
                Program.retornarError("El proveedor no existe", "Provedor no valido");
        }

        private void txtProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string nom = proveedorValida.Valid(txtProveedor.Text, Program.conexion);
                if (nom != null)
                {
                    txtProve.Text = nom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
