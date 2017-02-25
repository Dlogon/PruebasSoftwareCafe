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
    public partial class FormBuscarProveedor : Form
    {
        public FormBuscarProveedor()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bool encontro = false;
            try
            {
                IDbConnection conexion = new SqlConnection(Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT * FROM PROVEEDOR where idproveedor='" + txtID.Text + "';";

                IDataReader read = dbcmd.ExecuteReader();
                while (read.Read())
                {
                    txtNombre.Text = read.GetString(read.GetOrdinal("Nombre"));
                    txtCiudad.Text = read.GetString(read.GetOrdinal("ciudad"));
                    txtEstado.Text = read.GetString(read.GetOrdinal("estado"));
                    txtTelefono.Text = read.GetString(read.GetOrdinal("telefono"));
                    txtDireccion.Text = read.GetString(read.GetOrdinal("direccion"));
                    txtTiempoentrega.Text = read.GetInt32(read.GetOrdinal("tiempoentrega")).ToString();
                    btnEditar.Enabled = true;

                    encontro = true;
                }
                if (!encontro)
                {
                    MessageBox.Show("No se encuentra el registro");
                    Tools.setBoxemptys(Controls);
                    btnEditar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
        }

        private void FormBuscarProveedor_Load(object sender, EventArgs e)
        {
            btnmodificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEliminar.Enabled = true;
            btnmodificar.Enabled = true;
            txtID.Enabled = false;
            txtCiudad.Enabled = true;
            txtDireccion.Enabled = true;
            txtEstado.Enabled = true;
            txtNombre.Enabled = true;
            txtTelefono.Enabled = true;
            txtTiempoentrega.Enabled = true;
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            if (!ModifyTools.ModifyProveedor(Program.conexion, txtID.Text, txtNombre.Text, txtCiudad.Text, txtEstado.Text, txtTelefono.Text
                , txtDireccion.Text, Convert.ToInt32(txtTiempoentrega.Text)))
                MessageBox.Show("Ocurrio un error al modificar el registro");
            else
            {
                Tools.setBoxemptys(Controls);
                btnEliminar.Enabled = false;
                btnmodificar.Enabled = false;
                txtID.Enabled = true;
                txtCiudad.Enabled = false;
                txtDireccion.Enabled = false;
                txtEstado.Enabled = false;
                txtNombre.Enabled = false;
                txtTelefono.Enabled = false;
                txtTiempoentrega.Enabled = false;
                txtID.Focus();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string info = DeleteParameters.DeletewhitString(Program.conexion, "Proveedor", "idproveedor", "proveedor", txtID.Text);
            if (info.Substring(0, 12) == "ERROR: 23503")
                MessageBox.Show("Este proveedor ya tiene productos registrados, para borrarlo, modifique sus productos y cambielos de proveedor. si ya ha echo pedidos a este proveedor, no es posible Eliminarlo");
            else
            {
                Tools.setBoxemptys(Controls);
                btnEliminar.Enabled = false;
                btnmodificar.Enabled = false;
                txtID.Enabled = true;
                txtCiudad.Enabled = false;
                txtDireccion.Enabled = false;
                txtEstado.Enabled = false;
                txtNombre.Enabled = false;
                txtTelefono.Enabled = false;
                txtTiempoentrega.Enabled = false;
                txtID.Focus();
            }
        }
    }
}
