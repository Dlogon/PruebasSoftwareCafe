using Herramientas;
using Npgsql;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LaMorisca
{
    public partial class FormAgregarCliente : Form
    {
        public FormAgregarCliente()
        {
            InitializeComponent();
        }
        internal bool checkemptys()
        {
            foreach (Control c in Controls)
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

        private void FormAgregarCliente_Load(object sender, EventArgs e)
        {
            string tipe = "CLI";
            string sigue;
            try
            {
                IDbConnection conexion = new SqlConnection(Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT idcliente FROM Cliente order by idcliente desc";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    string len = string.Empty;
                    int i = 0;
                    sigue = Convert.ToString(reader.GetString(reader.GetOrdinal("idcliente")));
                    int next = Convert.ToInt32(sigue.Substring(3));
                    next++;

                    len += tipe;
                    while (i < ((next.ToString().Length - 5) * -1))
                    {
                        len += '0';
                        i++;
                    }
                    len += next.ToString();
                    txtIDGen.Text = len;
                    conexion.Close();
                }
                else
                {
                    string len = string.Empty;
                    int i = 0;
                    len += tipe;
                    while (i < 4)
                    {
                        len += '0';
                        i++;
                    }
                    len += "1";
                    txtIDGen.Text = len;
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
                    IDbConnection conexion = new SqlConnection(Program.conexion);
                    conexion.Open();
                    IDbCommand comando = conexion.CreateCommand();
                    comando.CommandText =
                        "INSERT INTO CLIENTE VALUES('" + txtIDGen.Text + "'," +
                        "'" + txtNombre.Text + "', " +
                        "'" + txtDireccion.Text + "', " +
                        "'" + txtTelefono.Text + "', " +
                        "0);";
                    IDataReader ejecutor = comando.ExecuteReader();
                    conexion.Close();

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
