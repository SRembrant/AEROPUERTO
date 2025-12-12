namespace Aeropuerto.ControlUsuario
{
    partial class Uc_GenerarFactura_Reagendo
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
            this.pnlConfirmarReagendamiento = new Guna.UI2.WinForms.Guna2Panel();
            this.btnGenerarFactura = new Guna.UI2.WinForms.Guna2Button();
            this.lblDireccionCorreco_mns = new System.Windows.Forms.Label();
            this.Regresar_misVuelos = new System.Windows.Forms.Label();
            this.pnlConfirmarReagendamiento.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlConfirmarReagendamiento
            // 
            this.pnlConfirmarReagendamiento.BorderRadius = 20;
            this.pnlConfirmarReagendamiento.Controls.Add(this.Regresar_misVuelos);
            this.pnlConfirmarReagendamiento.Controls.Add(this.btnGenerarFactura);
            this.pnlConfirmarReagendamiento.Controls.Add(this.lblDireccionCorreco_mns);
            this.pnlConfirmarReagendamiento.FillColor = System.Drawing.Color.White;
            this.pnlConfirmarReagendamiento.Location = new System.Drawing.Point(65, 217);
            this.pnlConfirmarReagendamiento.Name = "pnlConfirmarReagendamiento";
            this.pnlConfirmarReagendamiento.Size = new System.Drawing.Size(475, 215);
            this.pnlConfirmarReagendamiento.TabIndex = 3;
            // 
            // btnGenerarFactura
            // 
            this.btnGenerarFactura.BorderRadius = 20;
            this.btnGenerarFactura.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarFactura.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGenerarFactura.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGenerarFactura.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGenerarFactura.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGenerarFactura.FillColor = System.Drawing.Color.RoyalBlue;
            this.btnGenerarFactura.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarFactura.ForeColor = System.Drawing.Color.White;
            this.btnGenerarFactura.Location = new System.Drawing.Point(132, 111);
            this.btnGenerarFactura.Name = "btnGenerarFactura";
            this.btnGenerarFactura.Size = new System.Drawing.Size(212, 46);
            this.btnGenerarFactura.TabIndex = 12;
            this.btnGenerarFactura.Text = "Generar factura";
            this.btnGenerarFactura.Click += new System.EventHandler(this.btnGenerarFactura_Click);
            // 
            // lblDireccionCorreco_mns
            // 
            this.lblDireccionCorreco_mns.AutoSize = true;
            this.lblDireccionCorreco_mns.BackColor = System.Drawing.Color.Transparent;
            this.lblDireccionCorreco_mns.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblDireccionCorreco_mns.Location = new System.Drawing.Point(62, 67);
            this.lblDireccionCorreco_mns.Name = "lblDireccionCorreco_mns";
            this.lblDireccionCorreco_mns.Size = new System.Drawing.Size(357, 25);
            this.lblDireccionCorreco_mns.TabIndex = 10;
            this.lblDireccionCorreco_mns.Text = "¡Su pago se ha realizado exitosamente!";
            // 
            // Regresar_misVuelos
            // 
            this.Regresar_misVuelos.AutoSize = true;
            this.Regresar_misVuelos.BackColor = System.Drawing.SystemColors.HighlightText;
            this.Regresar_misVuelos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Regresar_misVuelos.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Regresar_misVuelos.Location = new System.Drawing.Point(175, 160);
            this.Regresar_misVuelos.Name = "Regresar_misVuelos";
            this.Regresar_misVuelos.Size = new System.Drawing.Size(131, 15);
            this.Regresar_misVuelos.TabIndex = 14;
            this.Regresar_misVuelos.Text = "Regresar a Mis Vuelos";
            this.Regresar_misVuelos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Regresar_misVuelos.Click += new System.EventHandler(this.Regresar_misVuelos_Click);
            // 
            // Uc_GenerarFactura_Reagendo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlConfirmarReagendamiento);
            this.Name = "Uc_GenerarFactura_Reagendo";
            this.Size = new System.Drawing.Size(634, 644);
            this.pnlConfirmarReagendamiento.ResumeLayout(false);
            this.pnlConfirmarReagendamiento.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlConfirmarReagendamiento;
        private Guna.UI2.WinForms.Guna2Button btnGenerarFactura;
        private System.Windows.Forms.Label lblDireccionCorreco_mns;
        private System.Windows.Forms.Label Regresar_misVuelos;
    }
}
