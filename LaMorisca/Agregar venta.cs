using Herramientas;
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
    public partial class Agregar_venta : Form
    {
        DataTable datos = new DataTable();
        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
        static string sucur;
        public Agregar_venta()
        {
            InitializeComponent();
            sucur = "";
        }

        private void Agregar_venta_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnentregada, "Presione este boton si la venta es entregada en mostrador");
            txtfecha.Text = DateTime.Now.ToShortDateString();
            int sigue;
            try
            {
                IDbConnection conexion = new SqlConnection(Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT folio FROM VENTA order by folio desc   1";
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read())
                {
                    string len = string.Empty;
                    //int i = 0;
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

            Tools.FillCombos(txtSucursal, Program.conexion, "idsucursal", "sucursal");
            Tools.FillCombos(txtIdCliente, Program.conexion, "idcliente", "cliente");
            Tools.FillCombos(txtIdEmpleado, Program.conexion, "idempleado", "empleado");

        }

        private void txtProveedor_Leave(object sender, EventArgs e)
        {
            if (empleadoValida.Valid(txtIdEmpleado.Text, Program.conexion)==null)
                  Program.retornarError("El empleado no existe", "Empleado no existe");
        }

        private void txtCliente_Leave(object sender, EventArgs e)
        {
            if (clienteValida.Valid(txtIdCliente.Text, Program.conexion)==null)
                 Program.retornarError("El cliente no existe", "Error");
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Tools.checkBoxEmptys(this.Controls))
                MessageBox.Show("faltan campos");
            else if (clienteValida.Valid(txtIdCliente.Text, Program.conexion)==null)
                Program.retornarError("El cliente no existe", "Error");
            else if (empleadoValida.Valid(txtIdEmpleado.Text, Program.conexion)==null)
                Program.retornarError("El empleado no existe", "Empleado no existe");
            else
            {
                try
                {
                    IDbConnection conexion = new SqlConnection
                        (Program.conexion);
                    conexion.Open();
                    IDbCommand comando = conexion.CreateCommand();
                    comando.CommandText =
                        "INSERT INTO VENTA VALUES(" + txtfolio.Text + "," +
                        "'No entregada',"+
                        "'" +txtfecha.Text+ "',NULL ,"+
                    "'" + txtIdEmpleado.Text + "',"+
                    "'"+ txtIdCliente.Text +"');";
                    IDataReader ejecutor = comando.ExecuteReader();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
        }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                SetTrabName();
            }
        }
        internal void SetClientName()
        {
            if (clienteValida.Valid(txtIdCliente.Text, Program.conexion)==null)
                Program.retornarError("El cliente no existe", "Cliente no encontrado");
            else
            {
                try
                {
                    IDbConnection conexion = new SqlConnection(Program.conexion);
                    conexion.Open();
                    IDbCommand dbcmd = conexion.CreateCommand();
                    dbcmd.CommandText = "SELECT nombre from cliente where idcliente='" + txtIdCliente.Text + "';";

                    IDataReader read = dbcmd.ExecuteReader();
                    if(read.Read())
                    {
                       txtNombreCLiente.Text = read.GetString(read.GetOrdinal("nombre"));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        internal void SetTrabName()
        {
            if (empleadoValida.Valid(txtIdEmpleado.Text, Program.conexion)==null)
                Program.retornarError("El empleado no existe", "empleado no encontrado");
            else
            {
                try
                {
                    IDbConnection conexion = new SqlConnection(Program.conexion);
                    conexion.Open();
                    IDbCommand dbcmd = conexion.CreateCommand();
                    dbcmd.CommandText = "SELECT nombre from empleado where idempleado='" + txtIdEmpleado.Text + "';";
                    conexion.Close();
                    IDataReader read = dbcmd.ExecuteReader();
                    if (read.Read())
                    {
                       txtNombreEmpleado.Text = read.GetString(read.GetOrdinal("nombre"));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtIdCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SetClientName();
            }
        }

        private void txtIDPROD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!ProductoValida.Valid(txtIDPROD.Text, Program.conexion))
                    MessageBox.Show("no existe");
                else if (txtNombreCLiente.Text == "" || clienteValida.Valid(txtIdCliente.Text, Program.conexion)==null)
                    Program.retornarError("El cliente no existe, o debe ingresar uno correcto", "cliente invalida");
                else if (txtNombreEmpleado.Text == "" || empleadoValida.Valid(txtIdEmpleado.Text, Program.conexion)==null)
                    Program.retornarError("El empleado no existe, o debe ingresar uno correcto", "empleado invalido");
                else
                {
                    try
                    {
                        IDbConnection conexion = new SqlConnection
                                (Program.conexion);
                        conexion.Open();
                        IDbCommand dbcmd = conexion.CreateCommand();
                        
                        NpgsqlDataAdapter cons = new NpgsqlDataAdapter(
                        "SELECT producto as IDproducto, producto.nombre, producto.detalle, cantidad, pedidodetalle.precio " +
                        " FROM pedidodetalle INNER JOIN producto on producto.idproducto=pedidodetalle.producto " +
                        " where folio=" + txtfolio.Text + "", Program.conexion)
                    ;
                        dbcmd.CommandText = "SELECT detalle, nombre, preciopub, existencia.cantidad FROM PRODUCTO"+
                            " INNER JOIN existencia on producto.idproducto=existencia.producto AND existencia.sucursal='"+ txtSucursal.Text+"'"
                            +"WHERE idproducto ='" + txtIDPROD.Text + "';";
                        IDataReader reader = dbcmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtDetalles.Text = reader.GetString(reader.GetOrdinal("detalle"));
                            txtProducto.Text = reader.GetString(reader.GetOrdinal("nombre"));
                            txtPrecio.Text = Convert.ToString(reader.GetDecimal(reader.GetOrdinal("preciopub")));
                            txtExistencia.Text = Convert.ToString(reader.GetInt32(reader.GetOrdinal("cantidad")));
                            conexion.Close();
                        }
                        else
                        {
                            MessageBox.Show("El producto existe, pero no se han echo pedidos");
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
        private void calculaSubtotal()
        {
            decimal subtotal = 0;
            for (int k = 0; k < dtaGrid.RowCount-1; k++)
            {
               // MessageBox.Show((dtaGrid.RowCount - 1).ToString(),"");
                string sub = Convert.ToString(dtaGrid.Rows[k].Cells[4].Value);
                subtotal += Convert.ToInt32(dtaGrid.Rows[k].Cells[3].Value) * Convert.ToDecimal(sub.Substring(0,sub.Length-1));
            }

            txtsubtotal.Text = subtotal.ToString()+"$";
            txtIva.Text = Convert.ToString(Convert.ToDouble(txtsubtotal.Text.Substring(0,txtsubtotal.Text.Length-1)) * .16)+"$";
            txtTotal.Text = Convert.ToString(Convert.ToDouble(txtsubtotal.Text.Substring(0, txtsubtotal.Text.Length - 1)) * 1.16) + "$"; 
        }

        private void txtSucursal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (SucursalValida.Valid(txtSucursal.Text, Program.conexion)==null)
                    Program.retornarError("La sucursal no existe", "Sucursal no encontrada");
                else
                {
                    try
                    {
                        IDbConnection conexion = new SqlConnection(Program.conexion);
                        conexion.Open();
                        IDbCommand dbcmd = conexion.CreateCommand();
                        dbcmd.CommandText = "SELECT direccion from SUCURSAL where idsucursal='" + txtSucursal.Text + "';";

                        IDataReader read = dbcmd.ExecuteReader();
                        if (read.Read())
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

        internal int getExistences(string product)
        {
            try
            {
                IDbConnection conexion = new SqlConnection(Program.conexion);
                conexion.Open();
                IDbCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = "SELECT cantidad from existencia where sucursal='" + txtSucursal.Text + "' AND producto='"+product+"';";

                IDataReader read = dbcmd.ExecuteReader();
                conexion.Close();
                if(read.Read())
                   return read.GetInt32(read.GetOrdinal("cantidad"));
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            return -1;

        }
        private void btnAgregarprod_Click(object sender, EventArgs e)
        {
            if (!ProductoValida.Valid(txtIDPROD.Text, Program.conexion) || txtCantidad.Text == "" || txtPrecio.Text == "")
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
                            dtaGrid.Rows[cuenta].Cells[4].Value = txtPrecio.Text + "$";
                            dtaGrid.Rows[cuenta].Cells[5].Value = Convert.ToString(Convert.ToInt32(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text))+"$";
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
                            dtaGrid.Rows[i].Cells[4].Value = txtPrecio.Text + "$";
                            dtaGrid.Rows[i].Cells[5].Value = Convert.ToString(Convert.ToInt32(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text))+"$";
                            i++;
                            dtaGrid.Rows[dtaGrid.RowCount-1].ReadOnly = true;
                        }
                    }
                    
                }
            }
            calculaSubtotal();
            txtCantidad.Clear();
            txtDetalles.Clear();
            txtPrecio.Clear();
            txtIDPROD.Clear();
            txtProducto.Clear();

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

                        int.TryParse(senderGrid.CurrentCell.Value.ToString(), out aux);
                        if (aux <= 0 || aux > getExistences(senderGrid.CurrentRow.Cells[0].Value.ToString()))
                        {
                            MessageBox.Show("La existencia no puede ser menor o igual que 0, o sobrepasaste la existencia");
                            senderGrid.CurrentCell.Value = 1;
                            aux = 1;
                        }
                        string pre = senderGrid.CurrentRow.Cells[4].Value.ToString();
                        dtaGrid.Rows[senderGrid.CurrentCell.RowIndex].Cells[5].Value =
                            Convert.ToString(
                                Convert.ToDecimal(pre.Substring(0, pre.Length - 1)) * aux) + "$";
                        calculaSubtotal();
                    }
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message, "");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.regresar(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (empleadoValida.Valid(txtIdEmpleado.Text, Program.conexion)==null)
                Program.retornarError("El proveedor no existe", "Proveedor no encontrado");
            else if(clienteValida.Valid(txtIdCliente.Text, Program.conexion)==null)
                Program.retornarError("El Cliente no existe", "Cliente no encontrado");
            else if(SucursalValida.Valid(txtSucursal.Text, Program.conexion)==null)
                Program.retornarError("La sucursal no es valida", "sucursal invalida");
            else if (i == 0)
                Program.retornarError("No hay productos en la venta", "Venta sin elementos");
            else
            {
                try
                {
                    string igual = "NULL";
                    string status = "No entregada";
                    if (btnentregada.Checked)
                    {
                        status = "Entregada";
                        igual = "'"+txtfecha.Text+"'";
                    }
                    SqlConnection conexion = new SqlConnection
                        (Program.conexion);
                    conexion.Open();
                    IDbCommand comando = conexion.CreateCommand();
                    comando.CommandText =
                        "INSERT INTO VENTA VALUES(" + txtfolio.Text + "," +
                        "'"+status+"'," +
                        "'" + txtfecha.Text + "', "+igual+" ," +
                    "'" + txtIdEmpleado.Text + "'," +
                    "'" + txtIdCliente.Text + "');";
                    comando.ExecuteReader();
                    if (!btnentregada.Checked)
                    {
                        comando.CommandText =
                          "Update cliente set saldo = saldo +" + txtTotal.Text.Substring(0, txtTotal.Text.Length - 1)+ "where idcliente='"+txtIdCliente.Text+"'";
                    comando.ExecuteReader();
                    }
                    int contador = 0, existnew;

                    while (i != contador)
                    {
                        SqlCommand cmdUp;
                        string idproduct = dtaGrid.Rows[contador].Cells[0].Value.ToString();
                        existnew = Convert.ToInt32(dtaGrid.Rows[contador].Cells[3].Value);
                        string queryUp = "update existencia set cantidad=cantidad - " + existnew +
                            " where producto = '" + idproduct + "' AND SUCURSAL = '" + txtSucursal.Text + "';";
                        cmdUp = new SqlCommand(queryUp, conexion);
                        cmdUp.ExecuteNonQuery();
                        string precio = dtaGrid.Rows[contador].Cells[4].Value.ToString();
                        queryUp = "INSERT INTO detalleventa VALUES('" +
                            idproduct + "', " +
                            "" + txtfolio.Text + ", " +
                            "" + precio.Substring(0,precio.Length-1) + ", " +
                                dtaGrid.Rows[contador].Cells[3].Value + ");";
                        cmdUp = new SqlCommand(queryUp, conexion);
                        cmdUp.ExecuteNonQuery();
                        contador++;
                    }
                    MessageBox.Show("Venta Guardado correctamente...");
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

        private void txtIdEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string nom = empleadoValida.Valid(txtIdEmpleado.Text, Program.conexion);
                if (nom != null)
                {
                    txtNombreEmpleado.Text = nom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtIdCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string nom = clienteValida.Valid(txtIdCliente.Text, Program.conexion);
                if (nom != null)
                {
                    txtNombreCLiente.Text = nom;
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
            if (sucur != ""&&dtaGrid.RowCount>1)
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

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Tools.WriteOnlyDigits(sender, e);
        }

        private void dtaGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= Tools.WriteOnlyDigits;
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 3)
            {
                e.Control.KeyPress += Tools.WriteOnlyDigits;
            }
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
                        calculaSubtotal();
                    }
                }
            }

        }
    }
}
