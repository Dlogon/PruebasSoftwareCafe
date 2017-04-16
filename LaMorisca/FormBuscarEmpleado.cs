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
using Connection;
namespace LaMorisca
{
    public partial class FormBuscarEmpleado : Form
    {
        private QueryBuilder builder;
        public FormBuscarEmpleado()
        {
            InitializeComponent();
            builder = new QueryBuilder(Program.conexion);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Herramientas.empleadoValida.Valid(txtID.Text, Program.conexion) == null)
            {
                MessageBox.Show("El empleado no existe");
                Tools.setBoxemptys(Controls);
                //pcfoto.Image = null;
                btnEditar.Enabled = false;
            }
            else
            {
                try
                {
                    Dictionary<string, string> dic =
                        builder.getField(
                            "EMPLEADO",
                            "where idempleado = '" + txtID.Text + "'",
                            "idempleado",
                            "idsucursal",
                            "nombre",
                            "apellido",
                            "direccion",
                            "telefono"
                            );
                    string puesto = dic["idempleado0"];
                    switch ((char)puesto.ToCharArray().GetValue(1))
                    {
                        case 'A':
                            txtRol.Text = "Administrativo";
                            break;
                        case 'G':
                            txtRol.Text = "Gerente";
                            break;
                        case 'C':
                            txtRol.Text = "Cajero";
                            break;
                        case 'B':
                            txtRol.Text = "Barman";
                            break;
                        case 'R':
                            txtRol.Text = "Repartidor";
                            break;
                        case 'E':
                            txtRol.Text = "Encargado de bodega";
                            break;
                    }
                    txtSucursal.Text = dic["idsucursal0"];
                    txtNombreEmpleado.Text = dic["nombre0"];
                    txtApellidoEmpleado.Text = dic["apellido0"];
                    txtDireccion.Text = dic["direccion0"];
                    txtTelefono.Text = dic["telefono0"];



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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEliminar.Enabled = true;
            btnmodificar.Enabled = true;
            txtID.Enabled = false;
            txtApellidoEmpleado.Enabled = true;
            txtDireccion.Enabled = true;
            txtNombreEmpleado.Enabled = true;
            txtSucursal.Enabled = true;
            txtTelefono.Enabled = true;
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            if (SucursalValida.Valid(txtSucursal.Text, Program.conexion)!=null)
            {
                Herramientas.ModifyTools.ModifyEmpleadoe(Program.conexion, txtID.Text, txtNombreEmpleado.Text, txtApellidoEmpleado.Text, txtDireccion.Text, txtTelefono.Text, txtSucursal.Text);
                btnEliminar.Enabled = false;
                btnmodificar.Enabled = false;
                txtID.Enabled = true;
                txtApellidoEmpleado.Enabled = false;
                txtDireccion.Enabled = false;
                txtNombreEmpleado.Enabled = false;
                txtSucursal.Enabled = false;
                txtTelefono.Enabled = false;
                txtID.Focus();

            }

            else
            {
                Program.retornarError("La sucursal no existe!!", "Error");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DeleteParameters.LogicDelete(builder, txtID.Text, "idempleado", "Empleado", "baja");
            if (DeleteParameters.LogicDelete(builder, txtID.Text, "idempleado", "Empleado", "baja"))
            {
                btnEliminar.Enabled = false;
                btnmodificar.Enabled = false;
                txtID.Enabled = true;
                txtApellidoEmpleado.Enabled = false;
                txtDireccion.Enabled = false;
                txtNombreEmpleado.Enabled = false;
                txtSucursal.Enabled = false;
                txtTelefono.Enabled = false;
                txtID.Focus();
            }
            else
                MessageBox.Show("Error al eliminar el empleado");
            
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }
    }
}
