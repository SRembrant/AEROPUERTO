namespace Aeropuerto
{
    partial class Uc_MisVuelos
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
            this.flp_MisVuelos = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.SuspendLayout();
            // 
            // flp_MisVuelos
            // 
            this.flp_MisVuelos.AutoScroll = true;
            this.flp_MisVuelos.BackColor = System.Drawing.Color.White;
            this.flp_MisVuelos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_MisVuelos.Location = new System.Drawing.Point(23, 64);
            this.flp_MisVuelos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flp_MisVuelos.Name = "flp_MisVuelos";
            this.flp_MisVuelos.Size = new System.Drawing.Size(881, 716);
            this.flp_MisVuelos.TabIndex = 13;
            this.flp_MisVuelos.WrapContents = false;
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel4.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(23, 16);
            this.guna2HtmlLabel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(99, 27);
            this.guna2HtmlLabel4.TabIndex = 12;
            this.guna2HtmlLabel4.Text = "Mis Vuelos";
            // 
            // Uc_MisVuelos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flp_MisVuelos);
            this.Controls.Add(this.guna2HtmlLabel4);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Uc_MisVuelos";
            this.Size = new System.Drawing.Size(935, 795);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flp_MisVuelos;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
    }
}
