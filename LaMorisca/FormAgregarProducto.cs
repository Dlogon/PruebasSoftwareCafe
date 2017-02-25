﻿using Herramientas;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace LaMorisca
{
    public partial class FormAgregarProducto : Form
    {
        public FormAgregarProducto()
        {
            InitializeComponent();
        }

        private void FormAgregarProducto_Load(object sender, EventArgs e)
        {
            Tools.FillCombos(txtProveedor, Program.conexion, "idproveedor", "proveedor");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtIProducto.Text == "")
            {
                Program.retornarError("falta el campo ID producto", "Error");
            }
            else if (proveedorValida.Valid(txtProveedor.Text, Program.conexion)==null)
                Program.retornarError("El proveedor no existe", "Provedor no valido");
            else
            {
                try
                {
                    IDbConnection conexion = new SqlConnection(Program.conexion);
                    conexion.Open();
                    IDbCommand comando = conexion.CreateCommand();
                    comando.CommandText =
                        "INSERT INTO PRODUCTO VALUES('" + txtIProducto.Text + "'," +
                        "" +numPrecioPublic.Value + ", " +
                        "'" + txtDetalles.Text + "', " +
                        "'" + txtNombre.Text + "', " +
                        "'" + txtProveedor.Text +"',"+
                        "" + numPreciolista.Value + "); ";

                    comando.ExecuteReader();
                    conexion.Close();
                    MessageBox.Show("Registro Guardado correctamente...");
                    Tools.setBoxemptys(Controls);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void txtIdProveedor_Leave(object sender, EventArgs e)
        {
            if (proveedorValida.Valid(txtProve.Text, Program.conexion)==null)
                Program.retornarError("El proveedor no existe", "Provedor no valido");
        }

        private void txtProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string nom = proveedorValida.Valid(txtProveedor.Text, Program.conexion);
                if (nom != null)
                {
                    txtProve.Text = nom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}