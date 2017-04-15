using Herramientas;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Connection;
namespace LaMorisca
{
    public partial class FormAgregarEmpleado : Form
    {
        private QueryBuilder builder;
        public FormAgregarEmpleado()
        {
            InitializeComponent();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Tools.checkBoxEmptys(this.Controls))
                MessageBox.Show("faltan campos");
            else if (SucursalValida.Valid(txtIdSucursal.Text, Program.conexion) == null)
                MessageBox.Show("La Sucursal no existe!!");
            else
            {
                try
                {
                    builder.insertFields(
                        "empleado",
                        txtIdSucursal.Text,
                        txtIDGenerado.Text,
                        txtNombreEmpleado.Text,
                        txtApellidoEmpleado.Text,
                        txtDireccion.Text,
                        txtTelefono.Text, "0"
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

        private void button1_Click(object sender, EventArgs e)
        {
            Program.regresar(this);

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtSucursal_Leave(object sender, EventArgs e)
        {
            if (SucursalValida.Valid(txtSucursal.Text, Program.conexion) == null)
                MessageBox.Show("La Sucursal no existe!!");
        }

        private void txtSucursal_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = "E" + (char)comboRol.Text.ToCharArray().GetValue(0);
            string sigue;
            try
            {
                IDbConnection conexion = new SqlConnection(Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT idempleado FROM EMPLEADO WHERE idempleado LIKE '" + selected + "%' order by idempleado desc     ";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    string len = string.Empty;
                    int i = 0;
                    sigue = Convert.ToString(reader.GetString(reader.GetOrdinal("idempleado")));
                    int next = Convert.ToInt32(sigue.Substring(2));
                    next++;

                    len += selected;
                    while (i < ((next.ToString().Length - 5) * -1))
                    {
                        len += '0';
                        i++;
                    }
                    len += next.ToString();
                    txtIDGenerado.Text = len;
                    conexion.Close();
                }
                else
                {
                    string len = string.Empty;
                    int i = 0;
                    len += selected;
                    while (i < 4)
                    {
                        len += '0';
                        i++;
                    }
                    len += "1";
                    txtIDGenerado.Text = len;
                }

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
            }
        }


        private void btnCargar_Click(object sender, EventArgs e)
        {

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void FormAgregarEmpleado_Load(object sender, EventArgs e)
        {
            Tools.FillCombos(txtIdSucursal, Program.conexion, "idsucursal", "sucursal");
        }

        private void txtIdSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string dir = SucursalValida.Valid(txtIdSucursal.Text, Program.conexion);
                if (dir!=null)
                {
                    txtSucursal.Text = dir;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}