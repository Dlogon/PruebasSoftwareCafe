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
    public partial class FormBuscarCliente : Form
    {
        public FormBuscarCliente()
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
                dbcmd.CommandText = "SELECT * FROM CLIENTE where idcliente='" + txtIDCliente.Text + "';";

                IDataReader read = dbcmd.ExecuteReader();
                conexion.Close();
                while (read.Read())
                {
                    txtNombre.Text = read.GetString(read.GetOrdinal("Nombre"));
                    txtDireccion.Text = read.GetString(read.GetOrdinal("direccion"));
                    txtTelefono.Text = read.GetString(read.GetOrdinal("telefono"));
                    txtSaldo.Text = read.GetDecimal(read.GetOrdinal("saldo")).ToString()+"$";

                    encontro = true;
                    btnEditar.Enabled = true;
                }
                if (!encontro)
                {
                    MessageBox.Show("No se encuentra el registro");
                    Tools.setBoxemptys(Controls);
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

        private void FormBuscarCliente_Load(object sender, EventArgs e)
        {
            btnmodificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEliminar.Enabled = true;
            btnmodificar.Enabled = true;
            txtIDCliente.Enabled = false;
            txtDireccion.Enabled = true;
            txtNombre.Enabled = true;
            txtTelefono.Enabled = true;
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            if (!ModifyTools.ModifyCliente(Program.conexion, txtIDCliente.Text, txtNombre.Text, txtDireccion.Text, txtTelefono.Text))
                MessageBox.Show("Ocurrio un error al modificar el registro");
            else
            {
                Tools.setBoxemptys(Controls);
                btnEliminar.Enabled = false;
                btnmodificar.Enabled = false;
                txtIDCliente.Enabled = true;
                txtDireccion.Enabled = false;
                txtNombre.Enabled = false;
                txtTelefono.Enabled = false;
                txtIDCliente.Focus();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string info = DeleteParameters.DeletewhitString(Program.conexion, "cliente", "idcliente", "cliente", txtIDCliente.Text);
            if (info.Substring(0, 12) == "ERROR: 23503")
                MessageBox.Show("Este cliente ya esta en una transaccion, por lo que no es posible borrarlo");
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }
    }
}
