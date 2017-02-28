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

namespace LaMorisca
{
    public partial class FormAgregarEmpleado : Form
    {
        public string nombreFoto="";
        public string completeroute;
        private string dest = Application.StartupPath + "\\Fotos\\";
        public FormAgregarEmpleado()
        {
            InitializeComponent();
        }

        private void copyfiles()
        {
            if (!(dest + nombreFoto == @completeroute))
                System.IO.File.Copy(@completeroute, dest + nombreFoto, true);
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Tools.checkBoxEmptys(this.Controls) || nombreFoto == "")
                MessageBox.Show("faltan campos");
            else if (SucursalValida.Valid(txtIdSucursal.Text, Program.conexion) == null)
                MessageBox.Show("La Sucursal no existe!!");
            else
            {
                try
                {
                    IDbConnection conexion = new SqlConnection
                        (Program.conexion);
                    conexion.Open();
                    IDbCommand comando = conexion.CreateCommand();
                    comando.CommandText =
                        "INSERT INTO EMPLEADO VALUES('" + txtIdSucursal.Text + "'," +
                        "'" + txtIDGenerado.Text + "', " +
                        "'" + txtNombreEmpleado.Text + "', " +
                        "'" + txtApellidoEmpleado.Text + "', " +
                        "'" + txtDireccion.Text + "', " +
                        "'" + txtTelefono.Text + "' , 0, " +
                        "'" + nombreFoto + "' ); ";

                    IDataReader ejecutor = comando.ExecuteReader();
                    conexion.Close();

                    MessageBox.Show("Registro Guardado correctamente...");
                    copyfiles();
                    Tools.setBoxemptys(Controls);
                    nombreFoto = "";

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
            OpenFileDialog BuscarImagen = new OpenFileDialog();
            BuscarImagen.Filter = "Archivos de imagen(*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            BuscarImagen.FileName = "";
            BuscarImagen.Title = "Selecciona foto del empleado";
            BuscarImagen.InitialDirectory =
                Application.StartupPath + "\\Fotos\\";
            if (BuscarImagen.ShowDialog() == DialogResult.OK)
            {
                nombreFoto = BuscarImagen.SafeFileName;
                completeroute = BuscarImagen.FileName;
                String Direccion = BuscarImagen.FileName;
            }
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