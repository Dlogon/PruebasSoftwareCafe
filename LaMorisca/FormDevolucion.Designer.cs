namespace LaMorisca
{
    partial class FormDevolucion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtfolio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCausa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtempleado = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCancela = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFolioVenta = new System.Windows.Forms.TextBox();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.txtIdEmpleado = new System.Windows.Forms.ComboBox();
            this.dtaview = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtfecha = new System.Windows.Forms.TextBox();
            this.txtsucursal = new System.Windows.Forms.ComboBox();
            this.sucDireccion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtaview)).BeginInit();
            this.SuspendLayout();
            // 
            // txtfolio
            // 
            this.txtfolio.Enabled = false;
            this.txtfolio.Location = new System.Drawing.Point(470, 12);
            this.txtfolio.Name = "txtfolio";
            this.txtfolio.Size = new System.Drawing.Size(100, 20);
            this.txtfolio.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(435, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Folio";
            // 
            // txtCausa
            // 
            this.txtCausa.Location = new System.Drawing.Point(92, 90);
            this.txtCausa.Multiline = true;
            this.txtCausa.Name = "txtCausa";
            this.txtCausa.Size = new System.Drawing.Size(329, 97);
            this.txtCausa.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Causas";
            // 
            // txtempleado
            // 
            this.txtempleado.Enabled = false;
            this.txtempleado.Location = new System.Drawing.Point(198, 192);
            this.txtempleado.Name = "txtempleado";
            this.txtempleado.Size = new System.Drawing.Size(223, 20);
            this.txtempleado.TabIndex = 11;
            this.txtempleado.Leave += new System.EventHandler(this.txtempleado_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Autoriza";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Cliente";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Detalle";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(198, 396);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 17;
            this.btnAgregar.Text = "&Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCancela
            // 
            this.btnCancela.Location = new System.Drawing.Point(279, 396);
            this.btnCancela.Name = "btnCancela";
            this.btnCancela.Size = new System.Drawing.Size(75, 23);
            this.btnCancela.TabIndex = 18;
            this.btnCancela.Text = "&Regresar";
            this.btnCancela.UseVisualStyleBackColor = true;
            this.btnCancela.Click += new System.EventHandler(this.btnCancela_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Folio de venta";
            // 
            // txtFolioVenta
            // 
            this.txtFolioVenta.Location = new System.Drawing.Point(92, 12);
            this.txtFolioVenta.Name = "txtFolioVenta";
            this.txtFolioVenta.Size = new System.Drawing.Size(100, 20);
            this.txtFolioVenta.TabIndex = 19;
            this.txtFolioVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFolioVenta_KeyPress);
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.Enabled = false;
            this.txtNombreCliente.Location = new System.Drawing.Point(198, 38);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Size = new System.Drawing.Size(223, 20);
            this.txtNombreCliente.TabIndex = 21;
            // 
            // txtIdEmpleado
            // 
            this.txtIdEmpleado.FormattingEnabled = true;
            this.txtIdEmpleado.Location = new System.Drawing.Point(92, 191);
            this.txtIdEmpleado.Name = "txtIdEmpleado";
            this.txtIdEmpleado.Size = new System.Drawing.Size(100, 21);
            this.txtIdEmpleado.TabIndex = 22;
            this.txtIdEmpleado.SelectedIndexChanged += new System.EventHandler(this.txtIdEmpleado_SelectedIndexChanged);
            // 
            // dtaview
            // 
            this.dtaview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtaview.Location = new System.Drawing.Point(18, 218);
            this.dtaview.Name = "dtaview";
            this.dtaview.Size = new System.Drawing.Size(552, 150);
            this.dtaview.TabIndex = 41;
            this.dtaview.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtaview_CellValueChanged);
            this.dtaview.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dtaview_EditingControlShowing);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(198, 12);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(103, 23);
            this.btnBuscar.TabIndex = 42;
            this.btnBuscar.Text = "Buscar en ventas";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(427, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Fecha";
            // 
            // txtfecha
            // 
            this.txtfecha.Enabled = false;
            this.txtfecha.Location = new System.Drawing.Point(470, 38);
            this.txtfecha.Name = "txtfecha";
            this.txtfecha.Size = new System.Drawing.Size(100, 20);
            this.txtfecha.TabIndex = 16;
            // 
            // txtsucursal
            // 
            this.txtsucursal.FormattingEnabled = true;
            this.txtsucursal.Location = new System.Drawing.Point(92, 64);
            this.txtsucursal.Name = "txtsucursal";
            this.txtsucursal.Size = new System.Drawing.Size(100, 21);
            this.txtsucursal.TabIndex = 43;
            this.txtsucursal.SelectedIndexChanged += new System.EventHandler(this.txtsucursal_SelectedIndexChanged);
            // 
            // sucDireccion
            // 
            this.sucDireccion.Enabled = false;
            this.sucDireccion.Location = new System.Drawing.Point(198, 64);
            this.sucDireccion.Name = "sucDireccion";
            this.sucDireccion.Size = new System.Drawing.Size(223, 20);
            this.sucDireccion.TabIndex = 44;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 45;
            this.label8.Text = "Sucursal";
            // 
            // txtCliente
            // 
            this.txtCliente.FormattingEnabled = true;
            this.txtCliente.Location = new System.Drawing.Point(92, 38);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(100, 21);
            this.txtCliente.TabIndex = 46;
            this.txtCliente.SelectedIndexChanged += new System.EventHandler(this.txtCliente_SelectedIndexChanged);
            // 
            // FormDevolucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 431);
            this.ControlBox = false;
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.sucDireccion);
            this.Controls.Add(this.txtsucursal);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dtaview);
            this.Controls.Add(this.txtIdEmpleado);
            this.Controls.Add(this.txtNombreCliente);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtFolioVenta);
            this.Controls.Add(this.btnCancela);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtfecha);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtempleado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCausa);
            this.Controls.Add(this.txtfolio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormDevolucion";
            this.Text = "Devolucion";
            this.Load += new System.EventHandler(this.FormDevolucion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtaview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtfolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCausa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtempleado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCancela;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFolioVenta;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.ComboBox txtIdEmpleado;
        private System.Windows.Forms.DataGridView dtaview;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtfecha;
        private System.Windows.Forms.ComboBox txtsucursal;
        private System.Windows.Forms.TextBox sucDireccion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox txtCliente;
    }
}