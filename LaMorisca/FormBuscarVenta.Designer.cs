namespace LaMorisca
{
    partial class FormBuscarVenta
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
            this.txtfechaR = new System.Windows.Forms.TextBox();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtfolio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFechaEn = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.Label();
            this.txtIva = new System.Windows.Forms.Label();
            this.txtsubtotal = new System.Windows.Forms.Label();
            this.lblTot = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.dtaview = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.txtNombreEmpleado = new System.Windows.Forms.TextBox();
            this.btnentregada = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtaview)).BeginInit();
            this.SuspendLayout();
            // 
            // txtfechaR
            // 
            this.txtfechaR.Enabled = false;
            this.txtfechaR.Location = new System.Drawing.Point(153, 29);
            this.txtfechaR.Name = "txtfechaR";
            this.txtfechaR.Size = new System.Drawing.Size(100, 20);
            this.txtfechaR.TabIndex = 31;
            // 
            // btnRegresar
            // 
            this.btnRegresar.Location = new System.Drawing.Point(257, 370);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(75, 23);
            this.btnRegresar.TabIndex = 30;
            this.btnRegresar.Text = "&Regresar";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(259, 4);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 29;
            this.btnAgregar.Text = "&Buscar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.Enabled = false;
            this.txtCliente.Location = new System.Drawing.Point(153, 81);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(100, 20);
            this.txtCliente.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Cliente";
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.Enabled = false;
            this.txtEmpleado.Location = new System.Drawing.Point(399, 51);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(100, 20);
            this.txtEmpleado.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(258, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Empleado que realiza";
            // 
            // txtfolio
            // 
            this.txtfolio.Location = new System.Drawing.Point(153, 6);
            this.txtfolio.Name = "txtfolio";
            this.txtfolio.Size = new System.Drawing.Size(100, 20);
            this.txtfolio.TabIndex = 23;
            this.txtfolio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtfolio_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Fecha de Realizacion";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Folio";
            // 
            // txtStatus
            // 
            this.txtStatus.Enabled = false;
            this.txtStatus.Location = new System.Drawing.Point(399, 29);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(100, 20);
            this.txtStatus.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(258, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "STATUS";
            // 
            // txtFechaEn
            // 
            this.txtFechaEn.Enabled = false;
            this.txtFechaEn.Location = new System.Drawing.Point(153, 55);
            this.txtFechaEn.Name = "txtFechaEn";
            this.txtFechaEn.Size = new System.Drawing.Size(100, 20);
            this.txtFechaEn.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Fecha de Entrega";
            // 
            // txtTotal
            // 
            this.txtTotal.AutoSize = true;
            this.txtTotal.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(350, 339);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(17, 19);
            this.txtTotal.TabIndex = 46;
            this.txtTotal.Text = "0";
            // 
            // txtIva
            // 
            this.txtIva.AutoSize = true;
            this.txtIva.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIva.Location = new System.Drawing.Point(350, 320);
            this.txtIva.Name = "txtIva";
            this.txtIva.Size = new System.Drawing.Size(17, 19);
            this.txtIva.TabIndex = 45;
            this.txtIva.Text = "0";
            // 
            // txtsubtotal
            // 
            this.txtsubtotal.AutoSize = true;
            this.txtsubtotal.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsubtotal.Location = new System.Drawing.Point(350, 301);
            this.txtsubtotal.Name = "txtsubtotal";
            this.txtsubtotal.Size = new System.Drawing.Size(17, 19);
            this.txtsubtotal.TabIndex = 44;
            this.txtsubtotal.Text = "0";
            // 
            // lblTot
            // 
            this.lblTot.AutoSize = true;
            this.lblTot.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTot.Location = new System.Drawing.Point(253, 339);
            this.lblTot.Name = "lblTot";
            this.lblTot.Size = new System.Drawing.Size(41, 19);
            this.lblTot.TabIndex = 43;
            this.lblTot.Text = "Total";
            // 
            // lblIva
            // 
            this.lblIva.AutoSize = true;
            this.lblIva.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(254, 320);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(35, 19);
            this.lblIva.TabIndex = 42;
            this.lblIva.Text = "I.V.A.";
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSub.Location = new System.Drawing.Point(254, 301);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(63, 19);
            this.lblSub.TabIndex = 41;
            this.lblSub.Text = "Subtotal";
            // 
            // dtaview
            // 
            this.dtaview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtaview.Location = new System.Drawing.Point(15, 131);
            this.dtaview.Name = "dtaview";
            this.dtaview.Size = new System.Drawing.Size(700, 43);
            this.dtaview.TabIndex = 40;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 115);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 38;
            this.label8.Text = "Detalles";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Location = new System.Drawing.Point(505, 81);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(82, 44);
            this.btnImprimir.TabIndex = 47;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.Enabled = false;
            this.txtNombreCliente.Location = new System.Drawing.Point(261, 81);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Size = new System.Drawing.Size(238, 20);
            this.txtNombreCliente.TabIndex = 48;
            // 
            // txtNombreEmpleado
            // 
            this.txtNombreEmpleado.Enabled = false;
            this.txtNombreEmpleado.Location = new System.Drawing.Point(505, 51);
            this.txtNombreEmpleado.Name = "txtNombreEmpleado";
            this.txtNombreEmpleado.Size = new System.Drawing.Size(210, 20);
            this.txtNombreEmpleado.TabIndex = 49;
            // 
            // btnentregada
            // 
            this.btnentregada.Location = new System.Drawing.Point(633, 81);
            this.btnentregada.Name = "btnentregada";
            this.btnentregada.Size = new System.Drawing.Size(82, 44);
            this.btnentregada.TabIndex = 50;
            this.btnentregada.Text = "&Marcar como entregada";
            this.btnentregada.UseVisualStyleBackColor = true;
            this.btnentregada.Click += new System.EventHandler(this.btnentregada_Click);
            // 
            // FormBuscarVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 426);
            this.ControlBox = false;
            this.Controls.Add(this.btnentregada);
            this.Controls.Add(this.txtNombreEmpleado);
            this.Controls.Add(this.txtNombreCliente);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtIva);
            this.Controls.Add(this.txtsubtotal);
            this.Controls.Add(this.lblTot);
            this.Controls.Add(this.lblIva);
            this.Controls.Add(this.lblSub);
            this.Controls.Add(this.dtaview);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtFechaEn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtfechaR);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEmpleado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtfolio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormBuscarVenta";
            this.Text = "Buscar Venta";
            ((System.ComponentModel.ISupportInitialize)(this.dtaview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtfechaR;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmpleado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtfolio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFechaEn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label txtTotal;
        private System.Windows.Forms.Label txtIva;
        private System.Windows.Forms.Label txtsubtotal;
        private System.Windows.Forms.Label lblTot;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.DataGridView dtaview;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.TextBox txtNombreEmpleado;
        private System.Windows.Forms.Button btnentregada;
    }
}