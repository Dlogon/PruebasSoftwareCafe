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
    public partial class FormAgregarProveedor : Form
    {
        public FormAgregarProveedor()
        {
            InitializeComponent();
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
                    IDbConnection conexion = new SqlConnection(Program.conexion);
                    conexion.Open();
                    IDbCommand comando = conexion.CreateCommand();
                    comando.CommandText =
                        "INSERT INTO PROVEEDOR VALUES('" +txtIdGen.Text + "'," +
                        "'" + TxtProveedor.Text + "', " +
                        "'" + txtCiudad.Text + "', " +
                        "'" + txtEstado.Text + "', " +
                        "'" + txtTelefono.Text + "', " +
                        "'" + txtDireccion.Text + "', " +
                        "" + numericDias1.Value + " , 0 ); ";
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

        private void FormProveedor_Load(object sender, EventArgs e)
        {
            string tipe = "PRO";
            string sigue;
            try
            {
                IDbConnection conexion = new SqlConnection(Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT idproveedor FROM PROVEEDOR order by idproveedor desc";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    string len = string.Empty;
                    int i = 0;
                    sigue = Convert.ToString(reader.GetString(reader.GetOrdinal("idproveedor")));
                    int next = Convert.ToInt32(sigue.Substring(3));
                    next++;

                    len += tipe;
                    while (i < ((next.ToString().Length - 5) * -1))
                    {
                        len += '0';
                        i++;
                    }
                    len += next.ToString();
                    txtIdGen.Text = len;
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
                    txtIdGen.Text = len;
                }

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
