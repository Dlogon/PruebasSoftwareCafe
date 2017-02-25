namespace LaMorisca
{
    partial class FormMovimientoInventario
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.txtCausa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtExistenciaO = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtProducto = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAgregarprod = new System.Windows.Forms.Button();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDetalles = new System.Windows.Forms.TextBox();
            this.txtIDPROD = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtaGrid = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.txtExistenciaD = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtnomDestino = new System.Windows.Forms.TextBox();
            this.txtOrigen = new System.Windows.Forms.ComboBox();
            this.txtDestino = new System.Windows.Forms.ComboBox();
            this.txtNomOrigen = new System.Windows.Forms.TextBox();
            this.txtIdEmpleado = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtaGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(595, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio";
            // 
            // txtFolio
            // 
            this.txtFolio.Enabled = false;
            this.txtFolio.Location = new System.Drawing.Point(638, 6);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.Size = new System.Drawing.Size(100, 20);
            this.txtFolio.TabIndex = 1;
            // 
            // txtCausa
            // 
            this.txtCausa.Location = new System.Drawing.Point(126, 64);
            this.txtCausa.Multiline = true;
            this.txtCausa.Name = "txtCausa";
            this.txtCausa.Size = new System.Drawing.Size(321, 54);
            this.txtCausa.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Causa";
            // 
            // txtFecha
            // 
            this.txtFecha.Enabled = false;
            this.txtFecha.Location = new System.Drawing.Point(638, 32);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(100, 20);
            this.txtFecha.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(595, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fecha";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Sucursal Origen";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Sucursal destino";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Empleado que autoriza";
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.Location = new System.Drawing.Point(239, 125);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(208, 20);
            this.txtEmpleado.TabIndex = 11;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(270, 425);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 13;
            this.btnAgregar.Text = "&Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(351, 425);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "&Regresar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtExistenciaO
            // 
            this.txtExistenciaO.Enabled = false;
            this.txtExistenciaO.Location = new System.Drawing.Point(432, 151);
            this.txtExistenciaO.Name = "txtExistenciaO";
            this.txtExistenciaO.Size = new System.Drawing.Size(100, 20);
            this.txtExistenciaO.TabIndex = 58;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Enabled = false;
            this.label12.Location = new System.Drawing.Point(337, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 13);
            this.label12.TabIndex = 57;
            this.label12.Text = "Existencia Origen";
            // 
            // txtProducto
            // 
            this.txtProducto.Enabled = false;
            this.txtProducto.Location = new System.Drawing.Point(234, 151);
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.Size = new System.Drawing.Size(100, 20);
            this.txtProducto.TabIndex = 54;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(178, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 53;
            this.label9.Text = "Producto";
            // 
            // btnAgregarprod
            // 
            this.btnAgregarprod.Location = new System.Drawing.Point(72, 200);
            this.btnAgregarprod.Name = "btnAgregarprod";
            this.btnAgregarprod.Size = new System.Drawing.Size(100, 23);
            this.btnAgregarprod.TabIndex = 52;
            this.btnAgregarprod.Text = "Agregar Producto";
            this.btnAgregarprod.UseVisualStyleBackColor = true;
            this.btnAgregarprod.Click += new System.EventHandler(this.btnAgregarprod_Click);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(72, 174);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(100, 20);
            this.txtCantidad.TabIndex = 51;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 50;
            this.label7.Text = "Cantidad";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(178, 181);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Detalles:";
            // 
            // txtDetalles
            // 
            this.txtDetalles.Enabled = false;
            this.txtDetalles.Location = new System.Drawing.Point(234, 181);
            this.txtDetalles.Multiline = true;
            this.txtDetalles.Name = "txtDetalles";
            this.txtDetalles.Size = new System.Drawing.Size(298, 26);
            this.txtDetalles.TabIndex = 48;
            // 
            // txtIDPROD
            // 
            this.txtIDPROD.Location = new System.Drawing.Point(72, 151);
            this.txtIDPROD.Name = "txtIDPROD";
            this.txtIDPROD.Size = new System.Drawing.Size(100, 20);
            this.txtIDPROD.TabIndex = 47;
            this.txtIDPROD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIDPROD_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 46;
            this.label11.Text = "Id producto";
            // 
            // dtaGrid
            // 
            this.dtaGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtaGrid.Location = new System.Drawing.Point(10, 229);
            this.dtaGrid.Name = "dtaGrid";
            this.dtaGrid.Size = new System.Drawing.Size(760, 190);
            this.dtaGrid.TabIndex = 45;
            this.dtaGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtaGrid_CellContentClick);
            this.dtaGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtaGrid_CellValueChanged);
            this.dtaGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dtaGrid_EditingControlShowing);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 213);
            this.label13.Name = "label13";
            this.label13.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label13.Size = new System.Drawing.Size(45, 13);
            this.label13.TabIndex = 44;
            this.label13.Text = "Detalles";
            // 
            // txtExistenciaD
            // 
            this.txtExistenciaD.Enabled = false;
            this.txtExistenciaD.Location = new System.Drawing.Point(638, 151);
            this.txtExistenciaD.Name = "txtExistenciaD";
            this.txtExistenciaD.Size = new System.Drawing.Size(100, 20);
            this.txtExistenciaD.TabIndex = 60;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Enabled = false;
            this.label10.Location = new System.Drawing.Point(538, 154);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 13);
            this.label10.TabIndex = 59;
            this.label10.Text = "Existencia Destino";
            // 
            // txtnomDestino
            // 
            this.txtnomDestino.Enabled = false;
            this.txtnomDestino.Location = new System.Drawing.Point(239, 31);
            this.txtnomDestino.Name = "txtnomDestino";
            this.txtnomDestino.Size = new System.Drawing.Size(208, 20);
            this.txtnomDestino.TabIndex = 9;
            // 
            // txtOrigen
            // 
            this.txtOrigen.FormattingEnabled = true;
            this.txtOrigen.Location = new System.Drawing.Point(126, 6);
            this.txtOrigen.Name = "txtOrigen";
            this.txtOrigen.Size = new System.Drawing.Size(107, 21);
            this.txtOrigen.TabIndex = 61;
            this.txtOrigen.SelectedIndexChanged += new System.EventHandler(this.txtOrigen_SelectedIndexChanged);
            // 
            // txtDestino
            // 
            this.txtDestino.FormattingEnabled = true;
            this.txtDestino.Location = new System.Drawing.Point(126, 31);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(107, 21);
            this.txtDestino.TabIndex = 62;
            this.txtDestino.SelectedIndexChanged += new System.EventHandler(this.txtDestino_SelectedIndexChanged);
            // 
            // txtNomOrigen
            // 
            this.txtNomOrigen.Enabled = false;
            this.txtNomOrigen.Location = new System.Drawing.Point(239, 6);
            this.txtNomOrigen.Name = "txtNomOrigen";
            this.txtNomOrigen.Size = new System.Drawing.Size(208, 20);
            this.txtNomOrigen.TabIndex = 63;
            // 
            // txtIdEmpleado
            // 
            this.txtIdEmpleado.FormattingEnabled = true;
            this.txtIdEmpleado.Location = new System.Drawing.Point(126, 124);
            this.txtIdEmpleado.Name = "txtIdEmpleado";
            this.txtIdEmpleado.Size = new System.Drawing.Size(107, 21);
            this.txtIdEmpleado.TabIndex = 64;
            this.txtIdEmpleado.SelectedIndexChanged += new System.EventHandler(this.txtIdEmpleado_SelectedIndexChanged);
            // 
            // FormMovimientoInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 450);
            this.ControlBox = false;
            this.Controls.Add(this.txtIdEmpleado);
            this.Controls.Add(this.txtNomOrigen);
            this.Controls.Add(this.txtDestino);
            this.Controls.Add(this.txtOrigen);
            this.Controls.Add(this.txtExistenciaD);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtExistenciaO);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtProducto);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnAgregarprod);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDetalles);
            this.Controls.Add(this.txtIDPROD);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dtaGrid);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtEmpleado);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtnomDestino);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCausa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFolio);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMovimientoInventario";
            this.Text = "Movimiento de inventario";
            this.Load += new System.EventHandler(this.FormMovimientoInventario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtaGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.TextBox txtCausa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmpleado;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtExistenciaO;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtProducto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnAgregarprod;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDetalles;
        private System.Windows.Forms.TextBox txtIDPROD;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dtaGrid;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtExistenciaD;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtnomDestino;
        private System.Windows.Forms.ComboBox txtOrigen;
        private System.Windows.Forms.ComboBox txtDestino;
        private System.Windows.Forms.TextBox txtNomOrigen;
        private System.Windows.Forms.ComboBox txtIdEmpleado;
    }
}