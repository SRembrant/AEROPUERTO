namespace Aeropuerto.ControlUsuario
{
    partial class Uc_GenerarFactura
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
            this.lbGenerarFactura = new System.Windows.Forms.Label();
            this.lblDireccionCorreco_mns = new System.Windows.Forms.Label();
            this.pnlConfirmarReagendamiento.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlConfirmarReagendamiento
            // 
            this.pnlConfirmarReagendamiento.BorderRadius = 20;
            this.pnlConfirmarReagendamiento.Controls.Add(this.lbGenerarFactura);
            this.pnlConfirmarReagendamiento.Controls.Add(this.lblDireccionCorreco_mns);
            this.pnlConfirmarReagendamiento.FillColor = System.Drawing.Color.White;
            this.pnlConfirmarReagendamiento.Location = new System.Drawing.Point(65, 217);
            this.pnlConfirmarReagendamiento.Name = "pnlConfirmarReagendamiento";
            this.pnlConfirmarReagendamiento.Size = new System.Drawing.Size(475, 215);
            this.pnlConfirmarReagendamiento.TabIndex = 2;
            // 
            // lbGenerarFactura
            // 
            this.lbGenerarFactura.AutoSize = true;
            this.lbGenerarFactura.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGenerarFactura.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbGenerarFactura.Location = new System.Drawing.Point(169, 110);
            this.lbGenerarFactura.Name = "lbGenerarFactura";
            this.lbGenerarFactura.Size = new System.Drawing.Size(149, 28);
            this.lbGenerarFactura.TabIndex = 11;
            this.lbGenerarFactura.Text = "Generar Factura";
            this.lbGenerarFactura.Click += new System.EventHandler(this.lbGenerarFactura_Click);
            // 
            // lblDireccionCorreco_mns
            // 
            this.lblDireccionCorreco_mns.AutoSize = true;
            this.lblDireccionCorreco_mns.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblDireccionCorreco_mns.Location = new System.Drawing.Point(62, 67);
            this.lblDireccionCorreco_mns.Name = "lblDireccionCorreco_mns";
            this.lblDireccionCorreco_mns.Size = new System.Drawing.Size(357, 25);
            this.lblDireccionCorreco_mns.TabIndex = 10;
            this.lblDireccionCorreco_mns.Text = "¡Su pago se ha realizado exitosamente!";
            // 
            // Uc_GenerarFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlConfirmarReagendamiento);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Uc_GenerarFactura";
            this.Size = new System.Drawing.Size(634, 644);
            this.pnlConfirmarReagendamiento.ResumeLayout(false);
            this.pnlConfirmarReagendamiento.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlConfirmarReagendamiento;
        private System.Windows.Forms.Label lblDireccionCorreco_mns;
        private System.Windows.Forms.Label lbGenerarFactura;
    }
}
