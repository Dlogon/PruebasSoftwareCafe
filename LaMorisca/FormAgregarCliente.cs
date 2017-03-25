using Herramientas;
using Npgsql;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Connection;
namespace LaMorisca
{
    public partial class FormAgregarCliente : Form
    {
        private QueryBuilder builder;
        public FormAgregarCliente()
        {
            InitializeComponent();
            builder = new QueryBuilder(Program.conexion);
        }
      
        private void FormAgregarCliente_Load(object sender, EventArgs e)
        {
            string tipe = "CLI";
            try
            {
                string ultimo=builder.getField("Cliente", "idcliente", "order by idcliente desc");

                if (ultimo==null)
                {
                    txtIDGen.Text = tipe + "1";
                }
                else
                {
                    MessageBox.Show(ultimo);
                    int ls = Convert.ToInt32(ultimo.Substring(3,ultimo.Length-3));
                    ls++;
                    txtIDGen.Text = tipe+ls;
                }

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int aux;
            if (Tools.checkBoxEmptys(this.Controls))
                MessageBox.Show("faltan campos");
            else if (!(int.TryParse(txtTelefono.Text, out aux)))
                Program.retornarError("El telefono es de formato incorrecto", "ERROR");
            else
            {
                try
                {
                    builder.insertFields("CLIENTE",
                        txtIDGen.Text,
                        txtNombre.Text,
                        txtDireccion.Text,
                        txtTelefono.Text
                        ,"0","0"
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }
    }
}
