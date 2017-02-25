using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Mono;
using Herramientas;

namespace LaMorisca
{
    public partial class FormAgregarSucursal : Form
    {
        public FormAgregarSucursal()
        {
            InitializeComponent();
        }

        internal bool checkemptys()
        {
            if (txtTelefono.Text == "" || txtCiudad.Text == "" || txtDireccion.Text == "" || txtEstado.Text == "" || cmbTipo.Text == "")
                return true;
            return false;
        }
        internal void setEmpty()
        {
            txtTelefono.Text = "";
            txtCiudad.Text = "";
            txtDireccion.Text = "";
            txtEstado.Text = "";
            cmbTipo.Text = "";
            txtIdGen.Text = "";
            cmbTipo.Text = "";
        }
        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected ="S"+ (char) cmbTipo.Text.ToCharArray().GetValue(0);
            string sigue;
            try
            {
                IDbConnection conexion = new SqlConnection
                        (Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT idsucursal FROM SUCURSAL WHERE idsucursal LIKE '"+ selected +"%' order by idsucursal desc";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    string len= string.Empty;
                    int i = 0;
                    sigue = Convert.ToString(reader.GetString(reader.GetOrdinal("idsucursal")));
                    int next = Convert.ToInt32(sigue.Substring(2));
                    next++;
                    
                    len += selected;
                    while (i < ((next.ToString().Length - 6)*-1))
                    {
                        len += '0';
                        i++;
                    }
                    len += next.ToString();
                    txtIdGen.Text = len;
                    conexion.Close();
                    reader.Close();
                }
                else
                {
                    string len = string.Empty;
                    int i = 0;
                    len += selected;
                    while (i < 5)
                    {
                        len += '0';
                        i++;
                    }
                    len += "1";
                    txtIdGen.Text = len;
                }

            }
            catch(NpgsqlException ex)
            {
                MessageBox.Show(ex.Message+ex.Where+ex.Detail+"XXX");
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
            }
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int aux;
            if (Tools.checkBoxEmptys(this.Controls))
                Program.retornarError("Faltan DATOS", "ERROR");
            else if (!(int.TryParse(txtTelefono.Text, out aux)))
                Program.retornarError("El telefono es de formato incorrecto", "ERROR");
            else
            {
                try
                {
                    IDbConnection conexion = new SqlConnection
                        (Program.conexion);
                    conexion.Open();
                    IDbCommand comando = conexion.CreateCommand();
                    comando.CommandText =
                        "INSERT INTO SUCURSAL VALUES('" + txtIdGen.Text + "'," +
                        "'" + txtDireccion.Text + "', " +
                        "'" + txtCiudad.Text + "', " +
                        "'" + txtEstado.Text + "', " +
                        "'" + txtTelefono.Text + "'); ";
                    IDataReader ejecutor = comando.ExecuteReader();
                    comando.CommandText = "select distinct producto from existencia";
                    ejecutor = comando.ExecuteReader();
                    while(ejecutor.Read())
                    {
                        comando.CommandText = "insert into existencia values(0, '" + txtIdGen.Text + "', " + ejecutor.GetString(ejecutor.GetOrdinal("producto")) + ");";
                        comando.ExecuteNonQuery();
                    }
                    conexion.Close();

                    MessageBox.Show("Sucursal Guardada correctamente ");
                    Tools.setBoxemptys(Controls);

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

        private void FormAgregarSucursal_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }
    }
}
