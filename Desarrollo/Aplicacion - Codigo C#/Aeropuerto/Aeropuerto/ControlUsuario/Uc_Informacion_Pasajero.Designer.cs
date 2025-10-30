namespace Aeropuerto
{
    partial class Uc_Informacion_Pasajero
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.mns_BuscarVuelos = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.flowLayoutPanelPasajeros = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cbxMetodoPago = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnRealizarCompra = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblPrecio_InformPasajero = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel6 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.chk_TerminosPoliticas = new System.Windows.Forms.CheckBox();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mns_BuscarVuelos
            // 
            this.mns_BuscarVuelos.AutoSize = false;
            this.mns_BuscarVuelos.BackColor = System.Drawing.Color.Transparent;
            this.mns_BuscarVuelos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.mns_BuscarVuelos.ForeColor = System.Drawing.Color.Black;
            this.mns_BuscarVuelos.Location = new System.Drawing.Point(16, 7);
            this.mns_BuscarVuelos.Margin = new System.Windows.Forms.Padding(4);
            this.mns_BuscarVuelos.Name = "mns_BuscarVuelos";
            this.mns_BuscarVuelos.Size = new System.Drawing.Size(293, 37);
            this.mns_BuscarVuelos.TabIndex = 64;
            this.mns_BuscarVuelos.Text = "Información de pasajero";
            // 
            // flowLayoutPanelPasajeros
            // 
            this.flowLayoutPanelPasajeros.AutoScroll = true;
            this.flowLayoutPanelPasajeros.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelPasajeros.Location = new System.Drawing.Point(16, 48);
            this.flowLayoutPanelPasajeros.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelPasajeros.Name = "flowLayoutPanelPasajeros";
            this.flowLayoutPanelPasajeros.Size = new System.Drawing.Size(728, 582);
            this.flowLayoutPanelPasajeros.TabIndex = 65;
            this.flowLayoutPanelPasajeros.WrapContents = false;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(378, 658);
            this.guna2HtmlLabel2.Margin = new System.Windows.Forms.Padding(4);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(106, 21);
            this.guna2HtmlLabel2.TabIndex = 66;
            this.guna2HtmlLabel2.Text = "Metodo de pago";
            // 
            // cbxMetodoPago
            // 
            this.cbxMetodoPago.BackColor = System.Drawing.Color.Transparent;
            this.cbxMetodoPago.BorderRadius = 10;
            this.cbxMetodoPago.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMetodoPago.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbxMetodoPago.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbxMetodoPago.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbxMetodoPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbxMetodoPago.ItemHeight = 18;
            this.cbxMetodoPago.Items.AddRange(new object[] {
            "PSE",
            "Tarjeta Crédito",
            "Tarjeta Débito"});
            this.cbxMetodoPago.Location = new System.Drawing.Point(492, 658);
            this.cbxMetodoPago.Margin = new System.Windows.Forms.Padding(4);
            this.cbxMetodoPago.Name = "cbxMetodoPago";
            this.cbxMetodoPago.Size = new System.Drawing.Size(191, 24);
            this.cbxMetodoPago.TabIndex = 75;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.btnRealizarCompra);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel5);
            this.guna2Panel1.Controls.Add(this.lblPrecio_InformPasajero);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel1.FillColor = System.Drawing.Color.Black;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 697);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(784, 105);
            this.guna2Panel1.TabIndex = 66;
            // 
            // btnRealizarCompra
            // 
            this.btnRealizarCompra.BackColor = System.Drawing.Color.Transparent;
            this.btnRealizarCompra.BorderRadius = 15;
            this.btnRealizarCompra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRealizarCompra.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRealizarCompra.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRealizarCompra.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRealizarCompra.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRealizarCompra.FillColor = System.Drawing.Color.RoyalBlue;
            this.btnRealizarCompra.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarCompra.ForeColor = System.Drawing.Color.White;
            this.btnRealizarCompra.Location = new System.Drawing.Point(459, 23);
            this.btnRealizarCompra.Margin = new System.Windows.Forms.Padding(4);
            this.btnRealizarCompra.Name = "btnRealizarCompra";
            this.btnRealizarCompra.Size = new System.Drawing.Size(285, 50);
            this.btnRealizarCompra.TabIndex = 7;
            this.btnRealizarCompra.Text = "Realizar compra";
            this.btnRealizarCompra.UseTransparentBackground = true;
            this.btnRealizarCompra.Click += new System.EventHandler(this.btnRealizarCompra_Click);
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel5.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(55, 17);
            this.guna2HtmlLabel5.Margin = new System.Windows.Forms.Padding(4);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(105, 27);
            this.guna2HtmlLabel5.TabIndex = 6;
            this.guna2HtmlLabel5.Text = "Precio total";
            // 
            // lblPrecio_InformPasajero
            // 
            this.lblPrecio_InformPasajero.BackColor = System.Drawing.Color.Transparent;
            this.lblPrecio_InformPasajero.Font = new System.Drawing.Font("Segoe UI", 13.25F, System.Drawing.FontStyle.Bold);
            this.lblPrecio_InformPasajero.ForeColor = System.Drawing.Color.White;
            this.lblPrecio_InformPasajero.Location = new System.Drawing.Point(31, 41);
            this.lblPrecio_InformPasajero.Margin = new System.Windows.Forms.Padding(4);
            this.lblPrecio_InformPasajero.Name = "lblPrecio_InformPasajero";
            this.lblPrecio_InformPasajero.Size = new System.Drawing.Size(157, 32);
            this.lblPrecio_InformPasajero.TabIndex = 4;
            this.lblPrecio_InformPasajero.Text = "3.700.000 COP";
            // 
            // guna2HtmlLabel6
            // 
            this.guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel6.Location = new System.Drawing.Point(55, 658);
            this.guna2HtmlLabel6.Margin = new System.Windows.Forms.Padding(4);
            this.guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            this.guna2HtmlLabel6.Size = new System.Drawing.Size(3, 2);
            this.guna2HtmlLabel6.TabIndex = 77;
            this.guna2HtmlLabel6.Text = null;
            // 
            // chk_TerminosPoliticas
            // 
            this.chk_TerminosPoliticas.AutoSize = true;
            this.chk_TerminosPoliticas.Location = new System.Drawing.Point(16, 658);
            this.chk_TerminosPoliticas.Margin = new System.Windows.Forms.Padding(4);
            this.chk_TerminosPoliticas.Name = "chk_TerminosPoliticas";
            this.chk_TerminosPoliticas.Size = new System.Drawing.Size(281, 20);
            this.chk_TerminosPoliticas.TabIndex = 3;
            this.chk_TerminosPoliticas.Text = "He leído y acepto la política de privacidad";
            this.chk_TerminosPoliticas.UseVisualStyleBackColor = true;
            // 
            // Uc_Informacion_Pasajero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2HtmlLabel6);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.cbxMetodoPago);
            this.Controls.Add(this.flowLayoutPanelPasajeros);
            this.Controls.Add(this.chk_TerminosPoliticas);
            this.Controls.Add(this.mns_BuscarVuelos);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Uc_Informacion_Pasajero";
            this.Size = new System.Drawing.Size(784, 802);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel mns_BuscarVuelos;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPasajeros;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2ComboBox cbxMetodoPago;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPrecio_InformPasajero;
        private Guna.UI2.WinForms.Guna2Button btnRealizarCompra;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel6;
        private System.Windows.Forms.CheckBox chk_TerminosPoliticas;
    }
}
