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
        private QueryBuilder builder;
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

            try
            {
                string tipo = builder.getField("tiposucursal", "idtiposucursal", "WHERE idtiposucursal =" + (cmbTipo.SelectedIndex +1)+ "");
                string prefix = builder.getField("tiposucursal", "prefijo", "WHERE idtiposucursal = " + (cmbTipo.SelectedIndex +1)+ "");
                string lastSuc=builder.getField("SUCURSAL S ", " idsucursal ", "INNER JOIN tiposucursal "+
                    " t on S.idtiposucursal = t.idtiposucursal where  t.idtiposucursal = "+tipo +
                    " order by idsucursal desc ");
               
                if (lastSuc!="")
                {
                    string idsuc = Regex.Match(lastSuc, @"\d+").Value;
                    int lastid = Convert.ToInt32(idsuc);
                    lastid++;
                    string len = prefix + Convert.ToString(lastid);
                    txtIdGen.Text = len;
                }
                else
                {
                    string len = prefix + '1';
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
                            "'" + txtTelefono.Text + "'",
                            Convert.ToString(cmbTipo.SelectedIndex +1)

                        }
                        );
                    IDataReader ejecutor = builder.returnReader(" existencia ", null, "distinct producto");
                    if(ejecutor!=null)
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

        private void FormAgregarSucursal_Load(object sender, EventArgs e)
        {
            IDataReader reader = builder.returnReader("tiposucursal", null, "descripcion");
            if(reader!=null)
            while(reader.Read())
            {
                cmbTipo.Items.Add(reader.GetString(reader.GetOrdinal("descripcion")));
            }
        }
    }
}
