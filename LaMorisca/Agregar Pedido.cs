using Herramientas;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaMorisca
{
    public partial class Agregar_Pedido : Form
    {
        DataTable datos = new DataTable();
        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
        static string sucur;
        public Agregar_Pedido()
        {
            InitializeComponent();
        }
        
        private void Agregar_Pedido_Load(object sender, EventArgs e)
        {
            txtfecha.Text = DateTime.Now.ToShortDateString();
            int sigue;
            try
            {
                IDbConnection conexion = new SqlConnection(Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT folio FROM PEDIDO order by folio desc   1";
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

            
            datos.Columns.Add("Id");
            datos.Columns.Add("Nombre");
            datos.Columns.Add("DESCRIPCIÓN");
            datos.Columns.Add("CANTIDAD");
            datos.Columns.Add("PRECIO");
            datos.Columns.Add("IMPORTE");
            dtaGrid.DataSource = datos.DefaultView;
            dtaGrid.Columns.Add(btn);
            btn.HeaderText = "Eliminar";
            btn.Text = "Eliminar fila";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;
            dtaGrid.Columns[0].ReadOnly = true;
            dtaGrid.Columns[1].ReadOnly = true;
            dtaGrid.Columns[2].ReadOnly = true;
            dtaGrid.Columns[4].ReadOnly = true;
            dtaGrid.Columns[5].ReadOnly = true;

            Tools.FillCombos(txtProveedor, Program.conexion, "idproveedor", "proveedor");
            Tools.FillCombos(txtSucursal, Program.conexion, "idsucursal", "sucursal");
        }

        internal void insertExistences(string idprod)
        {
            try
            {
                IDbConnection conexion = new SqlConnection(Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT idsucursal from sucursal;";

                IDataReader read = dbcmd.ExecuteReader();
                while (read.Read())
                {
                    dbcmd = conexion.CreateCommand();
                    dbcmd.CommandText = "Insert into existencia VALUES(0 ,'" + read.GetString(read.GetOrdinal("idsucursal")) + "','" + idprod + "');";
                    dbcmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection
                        (Program.conexion);
            if (proveedorValida.Valid(txtProveedor.Text, Program.conexion)==null)
                Program.retornarError("El proveedor no existe", "Proveedor no encontrado");
            else if(i==0)
                Program.retornarError("No hay productos en el pedido", "Pedido sin elementos");
            else
            {
                try
                {
                    
                    conexion.Open();
                    IDbCommand comando = conexion.CreateCommand();
                    comando.CommandText =
                        "INSERT INTO PEDIDO VALUES(" + txtfolio.Text + "," +
                        "'" + txtfecha.Text + "', " +
                    "'" + txtProveedor.Text + "' ," +
                    "'" + txtSucursal.Text + "');";
                    comando.ExecuteReader();
                    
                    int contador = 0, existnew;

                    while (i != contador)
                    {
                        SqlCommand cmdUp;
                        string idproduct = dtaGrid.Rows[contador].Cells[0].Value.ToString();
                        if(!Tools.FirstExistenceUpdate(txtSucursal.Text, idproduct, Program.conexion))
                            insertExistences(idproduct);
                        Program.retornarError(idproduct, "");
                        existnew =Convert.ToInt32(dtaGrid.Rows[contador].Cells[3].Value);
                        string queryUp ="update existencia set cantidad=cantidad + " + existnew +
                            " where producto = '" + idproduct + "' AND SUCURSAL = '"+ txtSucursal.Text+ "';";
                        
                        cmdUp = new SqlCommand(queryUp, conexion);
                        cmdUp.ExecuteNonQuery();
                        string precio = dtaGrid.Rows[contador].Cells[4].Value.ToString();
                        queryUp = "INSERT INTO PEDIDODETALLE VALUES(" +
                            txtfolio.Text + ", " +
                            "'" + idproduct + "', " +
                            "" + dtaGrid.Rows[contador].Cells[3].Value + ", " +
                                precio.Substring(0, precio.Length - 1) + ");";
                        cmdUp = new SqlCommand(queryUp, conexion);
                        cmdUp.ExecuteNonQuery();
                        contador++;
                    }
                    MessageBox.Show("Pedido Guardado correctamente...");
                    Tools.setBoxemptys(Controls);
                    conexion.Close();
                    dtaGrid.DataSource = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }

        }

        private void txtProveedor_Leave(object sender, EventArgs e)
        {
            SetProvName();
        }

        internal void SetProvName()
        {
            if (proveedorValida.Valid(txtProveedor.Text, Program.conexion)==null)
                Program.retornarError("El proveedor no existe", "Proveedor no encontrado");
            else
            {
                try
                {
                    IDbConnection conexion = new SqlConnection(Program.conexion);
                    conexion.Open();
                    IDbCommand dbcmd = conexion.CreateCommand();
                    dbcmd.CommandText = "SELECT nombre from proveedor where idproveedor='" + txtProveedor.Text + "';";

                    IDataReader read = dbcmd.ExecuteReader();
                    while (read.Read())
                    {
                        txtNombreProveedor.Text = read.GetString(read.GetOrdinal("nombre"));
                    }
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

        private void txtIDPROD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                if (!ProductoValida.Valid(txtIDPROD.Text, Program.conexion))
                    MessageBox.Show("no existe");
                else if (txtSucursal.Text == "" || SucursalValida.Valid(txtSucursal.Text, Program.conexion)==null)
                    Program.retornarError("La sucursal no existe o debe ingresar una correcta", "Sucursal invalida");
                else
                {
                    try
                    {
                        IDbConnection conexion = new SqlConnection
                                (Program.conexion);
                        conexion.Open();
                        IDbCommand dbcmd = conexion.CreateCommand();
                        dbcmd.CommandText = "SELECT * FROM PRODUCTO WHERE idproducto ='" + txtIDPROD.Text + "';";
                        IDataReader reader = dbcmd.ExecuteReader();
                        if (reader.Read())
                        {
                            if (reader.GetString(reader.GetOrdinal("proveedor")) != txtProveedor.Text)
                                MessageBox.Show("Este producto no pertenece a el proveedor" + txtProveedor.Text);
                            else
                            {
                                txtDetalles.Text = reader.GetString(reader.GetOrdinal("detalle"));
                                txtProducto.Text = reader.GetString(reader.GetOrdinal("nombre"));
                                txtPrecio.Text = Convert.ToString(reader.GetDecimal(reader.GetOrdinal("preciolista")));
                            }
                        }
                        else
                        {
                            MessageBox.Show("El producto se a añadido, pero aún no hay pedidos del mismo, pida el producto a su proveedor para generar existencias");
                        }

                    }
                    catch (Exception msg)
                    {
                        MessageBox.Show(msg.Message);
                        
                    }
                }
                    
            }
        }

        int cuenta;
        bool band;
        int n, i=0;
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

        internal void calculaSubtotal()
        {
            decimal subtotal=0;
            for(int k = 0; k< dtaGrid.RowCount-1; k++)
            {
                string sub = Convert.ToString(dtaGrid.Rows[k].Cells[4].Value);
                subtotal += Convert.ToInt32(dtaGrid.Rows[k].Cells[3].Value) * Convert.ToDecimal(sub.Substring(0, sub.Length - 1));
            }

            txtsubtotal.Text = subtotal.ToString() + "$";
            txtIva.Text = Convert.ToString(Convert.ToDouble(txtsubtotal.Text.Substring(0, txtsubtotal.Text.Length - 1)) * .16) + "$";
            txtTotal.Text = Convert.ToString(Convert.ToDouble(txtsubtotal.Text.Substring(0, txtsubtotal.Text.Length - 1)) * 1.16) + "$";
        }

        private void dtaGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if(MessageBox.Show("DESEA Eliminar EL ARTÍCULO de  LA VENTA", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (e.RowIndex == dtaGrid.RowCount - 1)
                        Program.retornarError("No se puede borrar esta fila", "Error");
                    else
                    {
                        dtaGrid.Rows.RemoveAt(e.RowIndex);
                        i--;
                        calculaSubtotal();
                    }
                }
            }
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                SetProvName();
            }
        }

        private void txtSucursal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                if (SucursalValida.Valid(txtSucursal.Text, Program.conexion)==null)
                    Program.retornarError("La sucursal no existe", "Proveedor no encontrado");
                else
                {
                    try
                    {
                        IDbConnection conexion = new SqlConnection(Program.conexion);
                        conexion.Open();
                        IDbCommand dbcmd = conexion.CreateCommand();
                        dbcmd.CommandText = "SELECT direccion from SUCURSAL where idsucursal='" + txtSucursal.Text+ "';";

                        IDataReader read = dbcmd.ExecuteReader();
                        while (read.Read())
                        {
                           txtDirSucursal.Text = read.GetString(read.GetOrdinal("direccion"));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void dtaGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool x=false;
            var senderGrid = (DataGridView)sender;
            if (!(Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
                x = true;
            }
            else
                senderGrid.CurrentCell.Value = "";
            if (x)
            {
                

                int aux;
                if (e.KeyChar == 13)
                {
                    if (int.TryParse(senderGrid.CurrentCell.Value.ToString(), out aux))
                    {
                        if (aux <= 0)
                        {
                            MessageBox.Show("La existencia no puede ser menor o igual que 0");
                            senderGrid.CurrentCell.Value = 1;
                        }

                    }
                    else
                    {
                        MessageBox.Show("No introdujiste un numero");
                        senderGrid.CurrentCell.Value = 1;

                    }
                    calculaSubtotal();
                }
            }
        }

        private void dtaGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;

                int aux;
                if ((senderGrid.RowCount != 1))
                {
                    if (senderGrid.CurrentCell.ColumnIndex == 3)
                    {

                        if (int.TryParse(senderGrid.CurrentCell.Value.ToString(), out aux))
                        {
                            if (aux <= 0)
                            {
                                MessageBox.Show("La existencia no puede ser menor o igual que 0");
                                senderGrid.CurrentCell.Value = 1;
                                aux = 1;
                            }
                            else
                                calculaSubtotal();

                        }
                        else
                        {
                            MessageBox.Show("No introdujiste un numero");
                            senderGrid.CurrentCell.Value = 1;
                            aux = 1;

                        }
                        string pre = senderGrid.CurrentRow.Cells[4].Value.ToString();



                        dtaGrid.Rows[senderGrid.CurrentCell.RowIndex].Cells[5].Value =
                            Convert.ToString(
                                Convert.ToDouble(pre.Substring(0, pre.Length - 1)) * aux) + "$";
                        calculaSubtotal();
                    }
                }
            }
            catch(Exception ex)
            {
                
            }
        }

        private void txtProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string nom = proveedorValida.Valid(txtProveedor.Text, Program.conexion);
                if (nom != null)
                {
                   txtNombreProveedor.Text = nom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            var senderbox = (ComboBox)sender;
            if (sucur != "" && dtaGrid.RowCount > 1)
            {
                if (MessageBox.Show("Si cambia de sucursal, la existencias se modifican, por lo que se reiniciara la venta, desea continuar?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    while (dtaGrid.RowCount != 1)
                        dtaGrid.Rows.RemoveAt(0);

                }
                else
                    senderbox.Text = sucur;
            }
            try
            {
                string nom = SucursalValida.Valid(txtSucursal.Text, Program.conexion);
                if (nom != null)
                {
                    txtDirSucursal.Text = nom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sucur = senderbox.Text;
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
            if (!ProductoValida.Valid(txtIDPROD.Text, Program.conexion) || txtCantidad.Text == "" || txtPrecio.Text == "")
                Program.retornarError("el producto no existe, o no introdujiste una cantidad valida", "ERROR");
            else if (MessageBox.Show("DESEAS AGREGAR EL ARTÍCULO A LA VENTA", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                BuscarEnGrid();
                if (band)
                {
                    dtaGrid.Rows[cuenta].Cells[0].Value = txtIDPROD.Text;
                    dtaGrid.Rows[cuenta].Cells[1].Value = txtProducto.Text;
                    dtaGrid.Rows[cuenta].Cells[2].Value = txtDetalles.Text;
                    dtaGrid.Rows[cuenta].Cells[3].Value = Convert.ToInt32(txtCantidad.Text) + Convert.ToInt32(dtaGrid.Rows[cuenta].Cells[3].Value);
                    dtaGrid.Rows[cuenta].Cells[4].Value = txtPrecio.Text+"$";
                    dtaGrid.Rows[cuenta].Cells[5].Value = Convert.ToString(Convert.ToInt32(txtCantidad.Text)*Convert.ToDouble(txtPrecio.Text))+"$";
                    
                }
                else
                {
                    datos.Rows.Add();
                    dtaGrid.Rows[i].Cells[0].Value = txtIDPROD.Text;
                    dtaGrid.Rows[i].Cells[1].Value = txtProducto.Text;
                    dtaGrid.Rows[i].Cells[2].Value = txtDetalles.Text;
                    dtaGrid.Rows[i].Cells[3].Value = txtCantidad.Text;
                    dtaGrid.Rows[i].Cells[4].Value = txtPrecio.Text+"$";
                    dtaGrid.Rows[i].Cells[5].Value = Convert.ToString(Convert.ToInt32(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text))+"$";
                    i++;
                    dtaGrid.Rows[dtaGrid.RowCount - 1].ReadOnly = true;
                }
                calculaSubtotal();
            }
            txtCantidad.Clear();
            txtPrecio.Clear();
            txtDetalles.Clear();
            txtProducto.Clear();
        }
    }
}

