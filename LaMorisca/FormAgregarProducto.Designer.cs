namespace LaMorisca
{
    partial class FormAgregarProducto
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

        internal class Numericsign : System.Windows.Forms.NumericUpDown
        {
            protected override void UpdateEditText()
            {
                base.UpdateEditText();

                ChangingText = true;
                Text += "$";
            }

        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIProducto = new System.Windows.Forms.TextBox();
            this.txtProve = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDetalles = new System.Windows.Forms.TextBox();
            this.numPreciolista = new LaMorisca.FormAgregarProducto.Numericsign();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.numPrecioPublic = new LaMorisca.FormAgregarProducto.Numericsign();
            this.label6 = new System.Windows.Forms.Label();
            this.txtProveedor = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numPreciolista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioPublic)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Id producto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "proveedor (ID)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombre";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Precio Lista";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Detalles";
            // 
            // txtIProducto
            // 
            this.txtIProducto.Location = new System.Drawing.Point(167, 12);
            this.txtIProducto.Name = "txtIProducto";
            this.txtIProducto.Size = new System.Drawing.Size(121, 20);
            this.txtIProducto.TabIndex = 5;
            // 
            // txtProve
            // 
            this.txtProve.Enabled = false;
            this.txtProve.Location = new System.Drawing.Point(325, 35);
            this.txtProve.Name = "txtProve";
            this.txtProve.Size = new System.Drawing.Size(172, 20);
            this.txtProve.TabIndex = 6;
            this.txtProve.Leave += new System.EventHandler(this.txtIdProveedor_Leave);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(167, 59);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(330, 20);
            this.txtNombre.TabIndex = 7;
            // 
            // txtDetalles
            // 
            this.txtDetalles.Location = new System.Drawing.Point(167, 132);
            this.txtDetalles.Multiline = true;
            this.txtDetalles.Name = "txtDetalles";
            this.txtDetalles.Size = new System.Drawing.Size(330, 141);
            this.txtDetalles.TabIndex = 8;
            // 
            // numPreciolista
            // 
            this.numPreciolista.DecimalPlaces = 2;
            this.numPreciolista.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPreciolista.Location = new System.Drawing.Point(167, 85);
            this.numPreciolista.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numPreciolista.Name = "numPreciolista";
            this.numPreciolista.Size = new System.Drawing.Size(98, 20);
            this.numPreciolista.TabIndex = 9;
            this.numPreciolista.Tag = "$";
            this.numPreciolista.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPreciolista.ThousandsSeparator = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(371, 279);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "&Regresar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(290, 279);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 11;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.button2_Click);
            // 
            // numPrecioPublic
            // 
            this.numPrecioPublic.DecimalPlaces = 2;
            this.numPrecioPublic.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPrecioPublic.Location = new System.Drawing.Point(167, 106);
            this.numPrecioPublic.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numPrecioPublic.Name = "numPrecioPublic";
            this.numPrecioPublic.Size = new System.Drawing.Size(98, 20);
            this.numPrecioPublic.TabIndex = 13;
            this.numPrecioPublic.Tag = "$";
            this.numPrecioPublic.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPrecioPublic.ThousandsSeparator = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Precio publico";
            // 
            // txtProveedor
            // 
            this.txtProveedor.FormattingEnabled = true;
            this.txtProveedor.Location = new System.Drawing.Point(167, 35);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Size = new System.Drawing.Size(152, 21);
            this.txtProveedor.TabIndex = 14;
            this.txtProveedor.SelectedIndexChanged += new System.EventHandler(this.txtProveedor_SelectedIndexChanged);
            // 
            // FormAgregarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 359);
            this.ControlBox = false;
            this.Controls.Add(this.txtProveedor);
            this.Controls.Add(this.numPrecioPublic);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.numPreciolista);
            this.Controls.Add(this.txtDetalles);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtProve);
            this.Controls.Add(this.txtIProducto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormAgregarProducto";
            this.Text = "Agregar producto";
            this.Load += new System.EventHandler(this.FormAgregarProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPreciolista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioPublic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIProducto;
        private System.Windows.Forms.TextBox txtProve;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDetalles;
        private System.Windows.Forms.NumericUpDown numPrecio;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private Numericsign numPreciolista;
        private Numericsign numPrecioPublic;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox txtProveedor;
    }
}