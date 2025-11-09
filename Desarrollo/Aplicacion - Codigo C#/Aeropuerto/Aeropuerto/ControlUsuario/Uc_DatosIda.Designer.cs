namespace Aeropuerto.ControlUsuario
{
    partial class Uc_DatosIda
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelIda = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pbComprar = new System.Windows.Forms.PictureBox();
            this.lblTituloVuelo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbFecha_VDisponibles = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.lbPrecio_VuelosDisponibles = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbDestino_VDisponibles = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbOrigen_VDisponibles = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbDestino_Avr_VDisponibles = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbOrigen_Avr_VDisponibles = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelIda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbComprar)).BeginInit();
            this.guna2Panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.panelIda);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(14, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(666, 168);
            this.flowLayoutPanel1.TabIndex = 14;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // panelIda
            // 
            this.panelIda.BackColor = System.Drawing.Color.White;
            this.panelIda.Controls.Add(this.guna2HtmlLabel2);
            this.panelIda.Controls.Add(this.pbComprar);
            this.panelIda.Controls.Add(this.lblTituloVuelo);
            this.panelIda.Controls.Add(this.lbFecha_VDisponibles);
            this.panelIda.Controls.Add(this.guna2Panel5);
            this.panelIda.Controls.Add(this.lbDestino_VDisponibles);
            this.panelIda.Controls.Add(this.lbOrigen_VDisponibles);
            this.panelIda.Controls.Add(this.lbDestino_Avr_VDisponibles);
            this.panelIda.Controls.Add(this.lbOrigen_Avr_VDisponibles);
            this.panelIda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelIda.Location = new System.Drawing.Point(3, 3);
            this.panelIda.Name = "panelIda";
            this.panelIda.Size = new System.Drawing.Size(656, 152);
            this.panelIda.TabIndex = 0;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(20, 39);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(26, 23);
            this.guna2HtmlLabel2.TabIndex = 13;
            this.guna2HtmlLabel2.Text = "Ida";
            // 
            // pbComprar
            // 
            this.pbComprar.Image = global::Aeropuerto.Properties.Resources.Captura_de_pantalla_2025_10_27_153759;
            this.pbComprar.Location = new System.Drawing.Point(90, 39);
            this.pbComprar.Name = "pbComprar";
            this.pbComprar.Size = new System.Drawing.Size(334, 39);
            this.pbComprar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbComprar.TabIndex = 11;
            this.pbComprar.TabStop = false;
            this.pbComprar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // lblTituloVuelo
            // 
            this.lblTituloVuelo.BackColor = System.Drawing.Color.Transparent;
            this.lblTituloVuelo.Font = new System.Drawing.Font("Segoe UI", 13.25F, System.Drawing.FontStyle.Bold);
            this.lblTituloVuelo.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblTituloVuelo.Location = new System.Drawing.Point(8, 6);
            this.lblTituloVuelo.Name = "lblTituloVuelo";
            this.lblTituloVuelo.Size = new System.Drawing.Size(67, 26);
            this.lblTituloVuelo.TabIndex = 15;
            this.lblTituloVuelo.Text = "Vuelo 1";
            // 
            // lbFecha_VDisponibles
            // 
            this.lbFecha_VDisponibles.BackColor = System.Drawing.Color.Transparent;
            this.lbFecha_VDisponibles.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lbFecha_VDisponibles.ForeColor = System.Drawing.Color.Gray;
            this.lbFecha_VDisponibles.Location = new System.Drawing.Point(183, 95);
            this.lbFecha_VDisponibles.Name = "lbFecha_VDisponibles";
            this.lbFecha_VDisponibles.Size = new System.Drawing.Size(54, 17);
            this.lbFecha_VDisponibles.TabIndex = 14;
            this.lbFecha_VDisponibles.Text = "Fecha Ida ";
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.Controls.Add(this.lbPrecio_VuelosDisponibles);
            this.guna2Panel5.Location = new System.Drawing.Point(497, 0);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(159, 152);
            this.guna2Panel5.TabIndex = 10;
            // 
            // lbPrecio_VuelosDisponibles
            // 
            this.lbPrecio_VuelosDisponibles.BackColor = System.Drawing.Color.Transparent;
            this.lbPrecio_VuelosDisponibles.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lbPrecio_VuelosDisponibles.ForeColor = System.Drawing.Color.Black;
            this.lbPrecio_VuelosDisponibles.Location = new System.Drawing.Point(36, 66);
            this.lbPrecio_VuelosDisponibles.Name = "lbPrecio_VuelosDisponibles";
            this.lbPrecio_VuelosDisponibles.Size = new System.Drawing.Size(106, 23);
            this.lbPrecio_VuelosDisponibles.TabIndex = 10;
            this.lbPrecio_VuelosDisponibles.Text = "1.250.000 COP";
            // 
            // lbDestino_VDisponibles
            // 
            this.lbDestino_VDisponibles.AutoSize = false;
            this.lbDestino_VDisponibles.BackColor = System.Drawing.Color.Transparent;
            this.lbDestino_VDisponibles.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lbDestino_VDisponibles.ForeColor = System.Drawing.Color.Gray;
            this.lbDestino_VDisponibles.Location = new System.Drawing.Point(376, 105);
            this.lbDestino_VDisponibles.Name = "lbDestino_VDisponibles";
            this.lbDestino_VDisponibles.Size = new System.Drawing.Size(28, 17);
            this.lbDestino_VDisponibles.TabIndex = 8;
            this.lbDestino_VDisponibles.Text = "Paris";
            // 
            // lbOrigen_VDisponibles
            // 
            this.lbOrigen_VDisponibles.AutoSize = false;
            this.lbOrigen_VDisponibles.BackColor = System.Drawing.Color.Transparent;
            this.lbOrigen_VDisponibles.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lbOrigen_VDisponibles.ForeColor = System.Drawing.Color.Gray;
            this.lbOrigen_VDisponibles.Location = new System.Drawing.Point(94, 105);
            this.lbOrigen_VDisponibles.Name = "lbOrigen_VDisponibles";
            this.lbOrigen_VDisponibles.Size = new System.Drawing.Size(41, 17);
            this.lbOrigen_VDisponibles.TabIndex = 7;
            this.lbOrigen_VDisponibles.Text = "Madrid";
            // 
            // lbDestino_Avr_VDisponibles
            // 
            this.lbDestino_Avr_VDisponibles.BackColor = System.Drawing.Color.Transparent;
            this.lbDestino_Avr_VDisponibles.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbDestino_Avr_VDisponibles.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbDestino_Avr_VDisponibles.Location = new System.Drawing.Point(425, 84);
            this.lbDestino_Avr_VDisponibles.Name = "lbDestino_Avr_VDisponibles";
            this.lbDestino_Avr_VDisponibles.Size = new System.Drawing.Size(57, 39);
            this.lbDestino_Avr_VDisponibles.TabIndex = 4;
            this.lbDestino_Avr_VDisponibles.Text = "PAR";
            // 
            // lbOrigen_Avr_VDisponibles
            // 
            this.lbOrigen_Avr_VDisponibles.BackColor = System.Drawing.Color.Transparent;
            this.lbOrigen_Avr_VDisponibles.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbOrigen_Avr_VDisponibles.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbOrigen_Avr_VDisponibles.Location = new System.Drawing.Point(20, 84);
            this.lbOrigen_Avr_VDisponibles.Name = "lbOrigen_Avr_VDisponibles";
            this.lbOrigen_Avr_VDisponibles.Size = new System.Drawing.Size(68, 39);
            this.lbOrigen_Avr_VDisponibles.TabIndex = 3;
            this.lbOrigen_Avr_VDisponibles.Text = "MAD";
            // 
            // Uc_DatosIda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Uc_DatosIda";
            this.Size = new System.Drawing.Size(698, 188);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelIda.ResumeLayout(false);
            this.panelIda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbComprar)).EndInit();
            this.guna2Panel5.ResumeLayout(false);
            this.guna2Panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Panel panelIda;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbFecha_VDisponibles;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private System.Windows.Forms.PictureBox pbComprar;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbPrecio_VuelosDisponibles;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbDestino_VDisponibles;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbOrigen_VDisponibles;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbDestino_Avr_VDisponibles;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbOrigen_Avr_VDisponibles;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTituloVuelo;
    }
}
