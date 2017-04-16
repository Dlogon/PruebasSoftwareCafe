using Herramientas;
using System;
using System.Windows.Forms;
using Connection;
namespace LaMorisca
{
    public partial class FormAgregarEmpleado : Form
    {
        private QueryBuilder builder;
        public FormAgregarEmpleado()
        {
            InitializeComponent();
            builder = new QueryBuilder(Program.conexion);
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
                        txtTelefono.Text, "0","0"
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
            try
            {
                string ultimo = builder.getField("Empleado", "idempleado", " order by idempleado desc ");

                if (ultimo == null)
                {
                    txtIDGenerado.Text = selected + "1";
                }
                else
                {
                    //MessageBox.Show(ultimo);
                    int ls = Convert.ToInt32(ultimo.Substring(2, ultimo.Length - 2));
                    ls++;
                    txtIDGenerado.Text = selected + ls;
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