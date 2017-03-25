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
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Herramientas.empleadoValida.Valid(txtID.Text, Program.conexion) == null)
            {
                MessageBox.Show("El empleado no existe");
                Tools.setBoxemptys(Controls);
                pcfoto.Image = null;
                btnEditar.Enabled = false;
            }
            else
            {
                try
                {
                    IDbConnection conexion = new SqlConnection
                                (Program.conexion);
                    conexion.Open();
                    IDbCommand dbcmd = conexion.CreateCommand();
                    dbcmd.CommandText = "SELECT * FROM EMPLEADO where idempleado='" + txtID.Text + "';";

                    IDataReader read = dbcmd.ExecuteReader();
                    while (read.Read())
                    {
                        string puesto = read.GetString(read.GetOrdinal("idempleado"));

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
                        txtSucursal.Text = read.GetString(read.GetOrdinal("idsucursal"));
                        txtNombreEmpleado.Text = read.GetString(read.GetOrdinal("nombre"));
                        txtApellidoEmpleado.Text = read.GetString(read.GetOrdinal("apellido"));
                        txtDireccion.Text = read.GetString(read.GetOrdinal("direccion"));
                        txtTelefono.Text = read.GetString(read.GetOrdinal("telefono"));
                        string foto = read.GetString(read.GetOrdinal("imagen"));
                        string cargarfoto = Application.StartupPath + "\\Fotos\\" + foto;
                        if (System.IO.File.Exists(cargarfoto))
                            pcfoto.Image = new Bitmap(cargarfoto);
                        else
                        {
                            MessageBox.Show("Se ha eliminado el archivo de foto!");
                        }
                        btnEditar.Enabled = true;
                    }

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
            string info = DeleteParameters.DeletewhitString(Program.conexion, "empleado", "idempleado", "empleado", txtID.Text);
            if(info=="OK")
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
            else if (info.Substring(0, 12) == "ERROR: 23503")
                MessageBox.Show("Este empleado ya esta en una transaccion, por lo que no es posible borrarlo");
            
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }
    }
}
