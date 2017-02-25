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
    public partial class FrmPrincipal : Form
    {
        static public string tipo { get; set; }
        

        public FrmPrincipal()
        {
            tipo = "Gerente";
            InitializeComponent();
        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void empleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            new FormAgregarEmpleado().Show(this);
            this.Hide();
            
        }

        private void proveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAgregarProveedor().Show(this);
            this.Hide();
        }

        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAgregarProducto().Show(this);
            this.Hide();
        }

        private void sucurssalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAgregarSucursal().Show(this);
            this.Hide();
        }
        
        private void pedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Agregar_Pedido().Show(this);
            this.Hide();

        }

        private void ventaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Agregar_venta().Show();
            this.Hide();
        }

        private void devolucionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormDevolucion().Show();
            this.Hide();
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormMovimientoInventario().Show();
            this.Hide();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBuscarProducto().Show();
            this.Hide();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBuscarCliente().Show();
            Hide();
        }

        private void devolu8cionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBuscarDevolucion().Show();
            Hide();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBuscarProveedor().Show();
            Hide();
        }

        private void sucursalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBuscarSucursal().Show();
            Hide();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBuscarEmpleado().Show();
            Hide();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAgregarCliente().Show();
            Hide();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBuscarVenta().Show();
            Hide();
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBuscarPedido().Show();
            Hide();
        }

        private void inventarioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new FormBuscarMovimiento().Show();
            Hide();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            txtttipo.Text = tipo;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            MessageBox.Show("NADA");
        }
    }
}
