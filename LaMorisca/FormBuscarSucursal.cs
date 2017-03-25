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
    public partial class FormBuscarSucursal : Form
    {
        private QueryBuilder builder;
        public FormBuscarSucursal()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (SucursalValida.Valid(txtID.Text, Program.conexion) == null)
            {
                MessageBox.Show("La sucursal no existe");
                Tools.setBoxemptys(Controls);
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
                    dbcmd.CommandText = "SELECT * FROM SUCURSAL where idsucursal='" + txtID.Text + "';";

                    IDataReader read = dbcmd.ExecuteReader();
                    while (read.Read())
                    {
                        char tipo = (char)read.GetString(read.GetOrdinal("idsucursal")).ToCharArray().GetValue(1);
                        switch (tipo)
                        {
                            case 'T':
                                txtTipo.Text = "Tienda";
                                break;
                            case 'B':
                                txtTipo.Text = "Bodega";
                                break;
                        }
                        txtDireccion.Text = read.GetString(read.GetOrdinal("direccion"));
                        txtCiudad.Text = read.GetString(read.GetOrdinal("ciudad"));
                        txtEstado.Text = read.GetString(read.GetOrdinal("estado"));
                        txtTelefono.Text = read.GetString(read.GetOrdinal("telefono"));
                        btnEditar.Enabled = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
        }

        private void FormBuscarSucursal_Load(object sender, EventArgs e)
        {
            btnmodificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnmodificar.Enabled = true;
            btnEliminar.Enabled = true;
            txtID.Enabled = false;
            txtCiudad.Enabled = true;
            txtDireccion.Enabled = true;
            txtEstado.Enabled = true;
            txtTelefono.Enabled = true;
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            if (!ModifyTools.ModifySucursal(Program.conexion, txtID.Text, txtDireccion.Text, txtCiudad.Text, txtEstado.Text, txtTelefono.Text))
                MessageBox.Show("Ocurrio un error al modificar el registro");
            else
            {
                Tools.setBoxemptys(Controls);
                btnmodificar.Enabled = false;
                btnEliminar.Enabled = false;
                txtID.Enabled = true;
                txtCiudad.Enabled = false;
                txtDireccion.Enabled = false;
                txtEstado.Enabled = false;
                txtTelefono.Enabled = false;
                txtID.Focus();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string info = DeleteParameters.DeletewhitString(Program.conexion, "sucursal", "idsucursal", "Sucursal", txtID.Text);
            if(info=="OK")
            {
                Tools.setBoxemptys(Controls);
                btnmodificar.Enabled = false;
                btnEliminar.Enabled = false;
                txtID.Enabled = true;
                txtCiudad.Enabled = false;
                txtDireccion.Enabled = false;
                txtEstado.Enabled = false;
                txtTelefono.Enabled = false;
                txtID.Focus();
            }
            else if(info=="NO")
            {
                
            }
            else if (info.Substring(0, 12) == "ERROR: 23503")
                MessageBox.Show("Esta sucursal ya tiene empleados registrados, para eliminarla, edite todos los empleados y cambielos de sucursal");
            else
            {
                Tools.setBoxemptys(Controls);
                btnmodificar.Enabled = false;
                btnEliminar.Enabled = false;
                txtID.Enabled = true;
                txtCiudad.Enabled = false;
                txtDireccion.Enabled = false;
                txtEstado.Enabled = false;
                txtTelefono.Enabled = false;
                txtID.Focus();
            }
        }
    }
}
