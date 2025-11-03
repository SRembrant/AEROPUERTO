namespace Aeropuerto
{
    partial class Uc_AcVuelosDisponibles
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
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.flowVuelosDisponibles = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel5.ForeColor = System.Drawing.Color.RoyalBlue;
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(966, 33);
            this.guna2HtmlLabel5.Margin = new System.Windows.Forms.Padding(4);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(50, 27);
            this.guna2HtmlLabel5.TabIndex = 14;
            this.guna2HtmlLabel5.Text = "Inicio";
            this.guna2HtmlLabel5.Click += new System.EventHandler(this.px_RegresarInicio_VerVuelosDisponibles_Click);
            // 
            // flowVuelosDisponibles
            // 
            this.flowVuelosDisponibles.AutoScroll = true;
            this.flowVuelosDisponibles.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowVuelosDisponibles.Location = new System.Drawing.Point(33, 68);
            this.flowVuelosDisponibles.Margin = new System.Windows.Forms.Padding(4);
            this.flowVuelosDisponibles.Name = "flowVuelosDisponibles";
            this.flowVuelosDisponibles.Size = new System.Drawing.Size(983, 716);
            this.flowVuelosDisponibles.TabIndex = 13;
            this.flowVuelosDisponibles.WrapContents = false;
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel4.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(23, 18);
            this.guna2HtmlLabel4.Margin = new System.Windows.Forms.Padding(4);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(169, 27);
            this.guna2HtmlLabel4.TabIndex = 12;
            this.guna2HtmlLabel4.Text = "Vuelos disponibles";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Aeropuerto.Properties.Resources.flechaiz;
            this.pictureBox2.Location = new System.Drawing.Point(912, 48);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 12);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 29;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.px_RegresarInicio_VerVuelosDisponibles_Click);
            // 
            // Uc_AcVuelosDisponibles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.guna2HtmlLabel5);
            this.Controls.Add(this.flowVuelosDisponibles);
            this.Controls.Add(this.guna2HtmlLabel4);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Uc_AcVuelosDisponibles";
            this.Size = new System.Drawing.Size(1068, 802);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private System.Windows.Forms.FlowLayoutPanel flowVuelosDisponibles;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
