using Herramientas;
using System;
using System.Windows.Forms;
using Connection;
using System.Data.SqlClient;
namespace LaMorisca
{
    public partial class FormAgregarProveedor : Form
    {
        private QueryBuilder builder;
        public FormAgregarProveedor()
        {
            InitializeComponent();
            builder = new QueryBuilder(Program.conexion);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int aux;
            if (Tools.checkBoxEmptys(Controls))
                Program.retornarError("Faltan DATOS", "ERROR");
            else if (!(int.TryParse(txtTelefono.Text, out aux)))
                Program.retornarError("El telefono es de formato incorrecto", "ERROR");
            else
            {
                try
                {
                    builder.insertFields("PROVEEDOR", new string[]
                    {
                        "" +txtIdGen.Text + "",
                        "" + TxtProveedor.Text + "",
                        "" + txtCiudad.Text + "",
                        "" + txtEstado.Text + "",
                        "" + txtTelefono.Text + "",
                        "" + txtDireccion.Text + "",
                        (numericDias1.Value).ToString(),
                        "0", "0"
                    }
                    );
                    MessageBox.Show("Registro Guardado correctamente...");
                    Tools.setBoxemptys(Controls);

                }
                catch(SqlException es)
                {
                    MessageBox.Show(es.Number + " "+ es.Message);
                    if(2627 == es.Number)
                        MessageBox.Show("El nombre del proveedor ya se encuentra registrado, favor de registrar uno diferente");
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

        private void FormProveedor_Load(object sender, EventArgs e)
        {
            string tipe = "PRO";
            try
            {
                string ultimo = builder.getField("PROVEEDOR", "idproveedor", " order by idproveedor desc ");

                if (ultimo == null)
                {
                    txtIdGen.Text = tipe + "1";
                }
                else
                {
                    //MessageBox.Show(ultimo.Substring(3, ultimo.Length - 3));
                    int ls = Convert.ToInt32(ultimo.Substring(3, ultimo.Length - 3));
                    ls++;
                    txtIdGen.Text = tipe + ls;
                }
            }
            catch (SqlException es)
            {
                MessageBox.Show(es.Number+ es.Message);
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.ToString());
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }
    }
}
