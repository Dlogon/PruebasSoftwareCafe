using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Mono;
using Herramientas;
using Connection;
using System.Text.RegularExpressions;

namespace LaMorisca
{
    public partial class FormAgregarSucursal : Form
    {
        QueryBuilder builder;
        public FormAgregarSucursal()
        {
            InitializeComponent();
            builder = new QueryBuilder(Program.conexion);
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
                string lastSuc=builder.getField("SUCURSAL", "idsucursal", "WHERE idsucursal LIKE '" + selected + "%' order by idsucursal desc");
               
                if (lastSuc!=null)
                {
                    string idsuc = Regex.Match(lastSuc, @"\d+").Value;
                    int lastid = Convert.ToInt32(idsuc);
                    lastid++;
                    string len = selected + Convert.ToString(lastid);
                    txtIdGen.Text = len;
                }
                else
                {
                    string len = selected + '1';
                    txtIdGen.Text = len;
                }

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
                    builder.insertFields("SUCURSAL", new string[]
                        {
                            "'" + txtIdGen.Text + "'",
                            "'" + txtDireccion.Text + "'",
                            "'" + txtCiudad.Text + "'",
                            "'" + txtEstado.Text + "'",
                            "'" + txtTelefono.Text + "'"
                        }
                        );
                    IDataReader ejecutor = builder.returnReader(" existencia ", null, "distinct producto");
                    while(ejecutor.Read())
                    {
                        builder.insertFields("existencia", new string[]
                            {
                                "0",
                                "'" + txtIdGen.Text + "'",
                                ejecutor.GetString(ejecutor.GetOrdinal("producto"))
                            }
                            );
                    }
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
