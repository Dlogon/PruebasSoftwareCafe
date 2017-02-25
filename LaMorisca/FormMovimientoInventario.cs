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
    public partial class FormMovimientoInventario : Form
    {
        DataTable datos = new DataTable();
        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
        public static string sucurO, sucurD;
        public FormMovimientoInventario()
        {
            InitializeComponent();
            sucurO = sucurD = "";
        }

        private void FormMovimientoInventario_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtFolio.Text = Convert.ToString(Tools.getFolio("movimientoinventario", Program.conexion));
            Tools.FillCombos(txtOrigen, Program.conexion, "idsucursal", "sucursal");
            Tools.FillCombos(txtDestino, Program.conexion, "idsucursal", "sucursal");
            Tools.FillCombos(txtIdEmpleado, Program.conexion, "idempleado", "empleado");

            datos.Columns.Add("Id");
            datos.Columns.Add("Nombre");
            datos.Columns.Add("DESCRIPCIÓN");
            datos.Columns.Add("CANTIDAD");
            dtaGrid.DataSource = datos.DefaultView;
            dtaGrid.Columns.Add(btn);
            btn.HeaderText = "Eliminar";
            btn.Text = "Eliminar fila";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;
            dtaGrid.Columns[0].ReadOnly = true;
            dtaGrid.Columns[1].ReadOnly = true;
            dtaGrid.Columns[2].ReadOnly = true;
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (SucursalValida.Valid(txtOrigen.Text, Program.conexion)==null)
                Program.retornarError("La sucursal de origen no existe", "Error");
            else if (SucursalValida.Valid(txtDestino.Text, Program.conexion)==null)
                Program.retornarError("la sucursal de destino no existe", "Error");
            else if(txtOrigen.Text.Equals(txtDestino.Text))
                Program.retornarError("la sucursal de destino no puede ser igual que la de origen", "Error");
            else if(empleadoValida.Valid(txtIdEmpleado.Text, Program.conexion)==null)
                Program.retornarError("El empleado no existe", "Error");
            else
            {
                try
                {
                    SqlConnection conexion = new SqlConnection
                        (Program.conexion);
                    conexion.Open();
                    IDbCommand comando = conexion.CreateCommand();
                    comando.CommandText =
                        "INSERT INTO movimientoinventario VALUES(" + txtFolio.Text + "," +
                        "'"+txtCausa.Text+"'," +
                        "'" +txtFecha.Text + "'," +
                    "'" + txtOrigen.Text + "'," +
                    "'" + txtDestino.Text + "'," +
                    "'" + txtIdEmpleado.Text + "');";
                    comando.ExecuteReader();

                    int contador = 0, existnew;

                    while (i != contador)
                    {
                        SqlCommand cmdUp;
                        string idproduct = dtaGrid.Rows[contador].Cells[0].Value.ToString();
                        existnew = Convert.ToInt32(dtaGrid.Rows[contador].Cells[3].Value);
                        string queryUp = "update existencia set cantidad=cantidad - " + existnew +
                            " where producto = '" + idproduct + "' AND SUCURSAL = '" + txtOrigen.Text + "';";
                        cmdUp = new SqlCommand(queryUp, conexion);
                        cmdUp.ExecuteNonQuery();
                        queryUp = "update existencia set cantidad=cantidad + " + existnew +
                            " where producto = '" + idproduct + "' AND SUCURSAL = '" + txtDestino.Text + "';";
                        cmdUp = new SqlCommand(queryUp, conexion);
                        cmdUp.ExecuteNonQuery();
                        queryUp = "INSERT INTO detallemov VALUES(" +
                            txtFolio.Text + ", " +
                            "'" + idproduct + "', " +
                            +existnew+  ");";
                        cmdUp = new SqlCommand(queryUp, conexion);
                        cmdUp.ExecuteNonQuery();
                        contador++;
                    }
                    MessageBox.Show("Movimiento guardado correctamente");
                    Tools.setBoxemptys(Controls);
                    conexion.Close();
                    dtaGrid.DataSource = null;

                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
        }

        private void txtDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtOrigen.Text.Equals(txtDestino.Text))
            {
                Program.retornarError("La sucursal de origen y destino no puede ser la misma", "ERROR");
                txtDestino.Text = sucurD;
            }
            else
            {
                SetSucurdir(sender, txtDestino, txtnomDestino);
                sucurD = txtDestino.Text;
            }
        }

        private void SetSucurdir(object sender, ComboBox id, TextBox text)
        {
            var senderbox = (ComboBox)sender;
            if (sucurO != ""&& sucurD!="" && dtaGrid.RowCount > 1)
            {
                if (MessageBox.Show("Si cambia de sucursal, la existencias se modifican, por lo que se reiniciara la venta, desea continuar?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    while (dtaGrid.RowCount != 1)
                        dtaGrid.Rows.RemoveAt(0);

                }
                else
                {
                    txtOrigen.Text = sucurO;
                    txtDestino.Text = sucurD;
                }
            }
            try
            {
                string nom = SucursalValida.Valid(id.Text, Program.conexion);
                if (nom != null)
                {
                    text.Text = nom;
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
                   txtEmpleado.Text = nom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtIDPROD_KeyPress(object sender, KeyPressEventArgs e)
        {
            IDbConnection conexion = new SqlConnection
                               (Program.conexion);
            if (e.KeyChar == 13)
            {
                if (!ProductoValida.Valid(txtIDPROD.Text, Program.conexion))
                {
                    MessageBox.Show("no existe");
                    txtCantidad.Text = "";
                    txtProducto.Text = "";
                    txtExistenciaD.Text = "";
                    txtExistenciaO.Text = "";
                    txtDetalles.Text = "";
                }
                else if (
                    txtOrigen.Text == "" ||
                    txtDestino.Text == "" ||
                    SucursalValida.Valid(txtOrigen.Text, Program.conexion) == null ||
                    SucursalValida.Valid(txtDestino.Text, Program.conexion) == null
                    )
                    Program.retornarError("La(s) sucursal(es) no existe(n), o debe ingresarlas de forma correcta", "Sucursal invalida");
                else
                {
                    try
                    {

                        conexion.Open();
                        IDbCommand dbcmd = conexion.CreateCommand();
                        dbcmd.CommandText = "SELECT detalle, nombre,  existencia.cantidad FROM PRODUCTO" +
                            " INNER JOIN existencia on producto.idproducto=existencia.producto AND existencia.sucursal='" + txtOrigen.Text + "'"
                            + "WHERE idproducto ='" + txtIDPROD.Text + "';";
                        IDataReader reader = dbcmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtDetalles.Text = reader.GetString(reader.GetOrdinal("detalle"));
                            txtProducto.Text = reader.GetString(reader.GetOrdinal("nombre"));
                            txtExistenciaO.Text = Convert.ToString(reader.GetInt32(reader.GetOrdinal("cantidad")));
                            conexion.Close();
                        }
                        else
                        {
                            MessageBox.Show("ERROR");
                        }
                        conexion.Open();
                        dbcmd.CommandText = "SELECT cantidad from existencia where producto='" + txtIDPROD.Text + "' AND sucursal='" + txtDestino.Text + "';";
                        reader = dbcmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtExistenciaD.Text = Convert.ToString(reader.GetInt32(reader.GetOrdinal("CANTIDAD")));
                            conexion.Close();
                        }
                        else
                        {
                            MessageBox.Show("ERROR");
                        }
                    }
                    catch (Exception msg)
                    {
                        MessageBox.Show(msg.Message);

                    }
                    finally
                    {
                        conexion.Close();
                    }
                }

            }
        }

        internal int getExistences(string product)
        {
            try
            {
                IDbConnection conexion = new SqlConnection(Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT cantidad from existencia where sucursal='" + txtOrigen.Text + "' AND producto='" + product + "';";

                IDataReader read = dbcmd.ExecuteReader();
                while (read.Read())
                    return read.GetInt32(read.GetOrdinal("cantidad"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            return -1;

        }
        int cuenta;
        bool band;
        int n, i = 0;
        internal void BuscarEnGrid()
        {
            cuenta = 0;
            band = false;
            do
            {
                string h = Convert.ToString(dtaGrid.Rows[cuenta].Cells[0].Value);
                if (h == txtIDPROD.Text)
                {
                    band = true;
                    break;

                }
                cuenta++;
            }
            while (cuenta < i);
        }

        private void dtaGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (MessageBox.Show("DESEA Eliminar EL ARTÍCULO de  LA VENTA", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (e.RowIndex == dtaGrid.RowCount - 1)
                        Program.retornarError("No se puede borrar esta fila", "Error");
                    else
                    {
                        dtaGrid.Rows.RemoveAt(e.RowIndex);
                        i--;
                    }
                }
            }
        }

        private void dtaGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            int aux;
            if ((senderGrid.RowCount != 1))
            {
                if (senderGrid.CurrentCell.ColumnIndex == 3)
                {

                    if (int.TryParse(senderGrid.CurrentCell.Value.ToString(), out aux))
                    {
                        if (aux <= 0 || aux > getExistences(senderGrid.CurrentRow.Cells[0].Value.ToString()))
                        {
                            MessageBox.Show("La existencia no puede ser menor o igual que 0, o sobrepasaste la existencia");
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

        private void dtaGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= Tools.WriteOnlyDigits;
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 3)
            {
                e.Control.KeyPress += Tools.WriteOnlyDigits;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }

        private void btnAgregarprod_Click(object sender, EventArgs e)
        {
            if (!ProductoValida.Valid(txtIDPROD.Text, Program.conexion) || txtCantidad.Text == "")
                Program.retornarError("el producto no existe, o no introdujiste una cantidad valida", "ERROR");
            else if (MessageBox.Show("DESEAS AGREGAR EL ARTÍCULO A LA VENTA", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                BuscarEnGrid();
                if (getExistences(txtIDPROD.Text) == 0)
                    Program.retornarError("La existencia de este producto es 0, no se puede vender", "ERROR");
                else
                {
                    if (band)
                    {
                        if (Convert.ToInt32(dtaGrid.Rows[cuenta].Cells[3].Value) + Convert.ToInt32(txtCantidad.Text) > getExistences(dtaGrid.Rows[cuenta].Cells[0].Value.ToString()))
                            MessageBox.Show("Sobrepasas la existencia");
                        else
                        {
                            dtaGrid.Rows[cuenta].Cells[0].Value = txtIDPROD.Text;
                            dtaGrid.Rows[cuenta].Cells[1].Value = txtProducto.Text;
                            dtaGrid.Rows[cuenta].Cells[2].Value = txtDetalles.Text;
                            dtaGrid.Rows[cuenta].Cells[3].Value = Convert.ToInt32(txtCantidad.Text) + Convert.ToInt32(dtaGrid.Rows[cuenta].Cells[3].Value);
                        }

                    }
                    else
                    {
                        if (Convert.ToInt32(txtCantidad.Text) > getExistences(txtIDPROD.Text))
                            MessageBox.Show("Sobrepasas la existencia");
                        else
                        {
                            datos.Rows.Add();
                            dtaGrid.Rows[i].Cells[0].Value = txtIDPROD.Text;
                            dtaGrid.Rows[i].Cells[1].Value = txtProducto.Text;
                            dtaGrid.Rows[i].Cells[2].Value = txtDetalles.Text;
                            dtaGrid.Rows[i].Cells[3].Value = txtCantidad.Text;
                            dtaGrid.Rows[dtaGrid.RowCount - 1].ReadOnly = true;
                            i++;
                        }
                    }
                   
                }
            }
            txtCantidad.Clear();
            txtDetalles.Clear();
            txtIDPROD.Clear();
            txtProducto.Clear();
        }

        private void txtOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtOrigen.Text.Equals(txtDestino.Text))
            {
                Program.retornarError("La sucursal de origen y destino no puede ser la misma","ERROR");
                txtOrigen.Text = sucurO;
            }
            else
            {
                SetSucurdir(sender, txtOrigen, txtNomOrigen);
                sucurO = txtOrigen.Text;
            }
        }
    }
}
