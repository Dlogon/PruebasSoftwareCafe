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
    public partial class FormDevolucion : Form
    {
        public FormDevolucion()
        {
            InitializeComponent();
        }

        private void FormDevolucion_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Solo se puede hacer devolucion de ventas ya entregadas, y unicamente se permite hacer 1 devolucion por venta", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            txtfecha.Text = DateTime.Now.ToShortDateString();
            int sigue;
            try
            {
                IDbConnection conexion = new SqlConnection(Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT folio FROM DEVOLUCION order by folio desc";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    string len = string.Empty;
                    int i = 0;
                    sigue = Convert.ToInt32(reader.GetInt32(reader.GetOrdinal("folio")));
                    sigue++;
                    txtfolio.Text = sigue.ToString();
                    conexion.Close();
                }
                else
                {
                    txtfolio.Text = "1";
                }

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
            }
            Tools.FillCombos(txtIdEmpleado, Program.conexion, "idempleado", "empleado");
            Tools.FillCombos(txtsucursal, Program.conexion, "idsucursal", "sucursal");
            Tools.FillCombos(txtCliente, Program.conexion, "idcliente", "cliente");
        }

        private void txtempleado_Leave(object sender, EventArgs e)
        {
            if (Herramientas.empleadoValida.Valid(txtempleado.Text, Program.conexion)==null)
                Program.retornarError("Ese empleado no existe", "Error");
        }

        private void txtCliente_Leave(object sender, EventArgs e)
        {
            if (Herramientas.clienteValida.Valid(txtCliente.Text, Program.conexion)==null)
                Program.retornarError("Ese cliente no existe", "Error");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (clienteValida.Valid(txtCliente.Text, Program.conexion)==null)
                Program.retornarError("El cliente no existe", "Error");
            else if (empleadoValida.Valid(txtIdEmpleado.Text, Program.conexion)==null)
                Program.retornarError("El empleado no existe", "Empleado no existe");
            else
            {
                bool selected=false;
                foreach (DataGridViewRow row in dtaview.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[4].Value) == true)
                    {
                        selected = true;
                        break;
                    }
                }
                if (selected)
                {
                    try
                    {
                        SqlConnection conexion = new SqlConnection
                            (Program.conexion);
                        conexion.Open();
                        IDbCommand comando = conexion.CreateCommand();
                        comando.CommandText =
                            "INSERT INTO DEVOLUCION VALUES(" + txtfolio.Text + ", " +
                            "'" + txtfecha.Text + "' ," +
                            "'" + txtCausa.Text + "'," +
                        "'" + txtIdEmpleado.Text + "'," +
                        "'" + txtCliente.Text + "'," + txtFolioVenta.Text + " );";
                        IDataReader ejecutor = comando.ExecuteReader();
                        comando.CommandText = "update venta set status='Con devolucion' where folio="+txtFolioVenta.Text+";";
                        comando.ExecuteNonQuery();
                       
                        foreach (DataGridViewRow row in dtaview.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells[4].Value) == true)
                            {
                                SqlCommand cmdUp;
                                string idproduct = row.Cells[0].Value.ToString();
                                int existnew = Convert.ToInt32(row.Cells[3].Value);
                                string queryUp = "update existencia set cantidad=cantidad + " + existnew +
                                     " where producto = '" + idproduct + "' AND SUCURSAL = '" + txtsucursal.Text + "';";
                                cmdUp = new SqlCommand(queryUp, conexion);
                                cmdUp.ExecuteNonQuery();
                                queryUp = "INSERT INTO detalledevolucion VALUES(" +
                                    txtfolio.Text + ", " +
                                    "'" + idproduct + "', " +
                                    +existnew + ");";
                                cmdUp = new SqlCommand(queryUp, conexion);
                                cmdUp.ExecuteNonQuery();

                            }
                        }
                        MessageBox.Show("Registro Guardado correctamente...");
                        Tools.setBoxemptys(Controls);
                        conexion.Close();
                        dtaview.DataSource = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        
                    }
                }
                else
                {
                    MessageBox.Show("todas las casillas estan des-seleccionadas, debe seleccionar al menos 1");
                }
            }
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool encontro = false;
            dtaview.DataSource = null;
            dtaview.ColumnCount = 0;
            DataTable datos = new DataTable();
            dtaview.DataSource = datos.DefaultView;
            try
            {
                IDbConnection conexion = new SqlConnection
                            (Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT * FROM Venta  "
                    + "  INNER JOIN " +
                    "cliente on cliente.idcliente=venta.cliente where folio=" + txtFolioVenta.Text + ";";

                IDataReader read = dbcmd.ExecuteReader();
                while (read.Read())
                {
                    encontro = true;
                    if (read.GetString(read.GetOrdinal("status")).Equals("No entregada") || read.GetString(read.GetOrdinal("status")).Equals("Con devolucion"))
                        MessageBox.Show("No se puede hacer devolucion a esta venta, no se ha entregado o ya se han echo devoluciones anteriormente",
                            "Status inadecuado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        txtCliente.Text = read.GetString(read.GetOrdinal("cliente"));
                        txtNombreCliente.Text = read.GetString(read.GetOrdinal("nombre"));
                        NpgsqlDataAdapter cons = new NpgsqlDataAdapter(
                            "SELECT producto as IDproducto, producto.nombre, producto.detalle, cantidad, detalleventa.precio " +
                            " FROM detalleventa INNER JOIN producto on producto.idproducto=detalleventa.producto " +
                            " where folio=" + txtFolioVenta.Text + "", Program.conexion);
                        cons.Fill(datos);
                        dtaview.Columns.RemoveAt(2);
                        dtaview.Columns.RemoveAt(3);
                        dtaview.Columns[0].ReadOnly = true;
                        dtaview.Columns[1].ReadOnly = true;
                        dtaview.Columns[2].ReadOnly = true;
                        dtaview.Columns.Add("Cantidad a devolver", "Cantidad a devolver");
                        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                        dtaview.Columns.Add(chk);
                        chk.HeaderText = "Devolver";
                        chk.Name = "dev";
                        dtaview.Rows[dtaview.RowCount - 1].Cells[3].ReadOnly = true;
                        dtaview.Rows[dtaview.RowCount - 1].Cells[4].ReadOnly = true;
                        encontro = true;
                    }
                }
                if (!encontro)
                {
                    MessageBox.Show("No se encuentra el registro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtaview_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            int aux;
            if ((senderGrid.RowCount != 1))
            {
                if (senderGrid.CurrentCell.ColumnIndex == 3)
                {

                    if (int.TryParse(senderGrid.CurrentCell.Value.ToString(), out aux))
                    {
                        if (aux <= 0||aux> Convert.ToInt32(senderGrid.CurrentRow.Cells[2].Value))
                        {
                            MessageBox.Show("La existencia no puede ser menor o igual que 0, o mayor a la de la venta");
                            senderGrid.CurrentCell.Value = 1;
                            aux = 1;
                        }

                    }
                    else
                    {
                        MessageBox.Show("No introdujiste un numero");
                        senderGrid.CurrentCell.Value = 1;
                        aux = 1;

                    }
                    
                }
            }
        }

        private void txtCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string nom = clienteValida.Valid(txtCliente.Text, Program.conexion);
                if (nom != null)
                {
                   txtNombreCliente.Text = nom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtsucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string nom = SucursalValida.Valid(txtsucursal.Text, Program.conexion);
                if (nom != null)
                {
                    sucDireccion.Text = nom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtIdEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string nom = empleadoValida.Valid(txtIdEmpleado.Text, Program.conexion);
                if (nom != null)
                {
                   txtempleado.Text = nom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFolioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }

        private void dtaview_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= Tools.WriteOnlyDigits;
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 3)
            {
                e.Control.KeyPress += Tools.WriteOnlyDigits;
            }
        }
    }
}
