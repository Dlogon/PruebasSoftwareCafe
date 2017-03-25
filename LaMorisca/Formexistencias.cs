using Connection;
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

namespace LaMorisca
{
    public partial class Formexistencias : Form
    {
        QueryBuilder builder;
        public string producto { get; set; }
        public Formexistencias(string pro)
        {
            InitializeComponent();
            producto = pro;
        }

        private void Formexistencias_Load(object sender, EventArgs e)
        {
            bool encontro = false;
            dtaview.DataSource = null;
            dtaview.ColumnCount = 0;
            DataTable datos = new DataTable();
            dtaview.DataSource = datos.DefaultView;

            try
            {

                NpgsqlDataAdapter cons = new NpgsqlDataAdapter(
                    "SELECT  sucursal, sucursal.direccion as direccion, producto as IDproducto, producto.nombre as nombre,  cantidad   " +
                    " FROM existencia INNER JOIN producto on producto.idproducto=existencia.producto INNER JOIN sucursal on sucursal.idsucursal= existencia.sucursal" +
                    " where producto='" + producto + "'", Program.conexion)
                ;
                cons.Fill(datos);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "");
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
