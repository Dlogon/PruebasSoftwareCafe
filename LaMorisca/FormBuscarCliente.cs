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
    public partial class FormBuscarCliente : Form
    {
        
        public FormBuscarCliente()
        {
            InitializeComponent();
            builder = new QueryBuilder(Program.conexion);
        }
        private QueryBuilder builder;
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> dic = builder.getField("ClIENTE",
                    "where idcliente = '" + txtIDCliente.Text + "'", "Nombre", "Direccion", "telefono", "saldo");
                if (dic != null)
                {
                    txtNombre.Text = dic["Nombre0"];
                    txtDireccion.Text = dic["direccion0"];
                    txtTelefono.Text = dic["telefono0"];
                    txtSaldo.Text = dic["saldo"].ToString() + "$";
                    btnEditar.Enabled = true;
                }
                else
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
           if(DeleteParameters.LogicDelete(builder, txtIDCliente.Text, "idcliente", "Cliente", "baja"))
                MessageBox.Show("REgistro Eliminado correctamente");
           else
                MessageBox.Show("Error al borrar el registro");
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }
    }
}
