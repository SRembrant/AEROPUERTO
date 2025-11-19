namespace Aeropuerto
{
    partial class Uc_ConfirmarCancelacion
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
            this.pnlConfirmarCancelacion = new Guna.UI2.WinForms.Guna2Panel();
            this.lblDireccionCorreco_mns = new System.Windows.Forms.Label();
            this.btnCancelar_confCancela = new Guna.UI2.WinForms.Guna2Button();
            this.btnConfirmarCancelacion = new Guna.UI2.WinForms.Guna2Button();
            this.pnlConfirmarCancelacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlConfirmarCancelacion
            // 
            this.pnlConfirmarCancelacion.BorderRadius = 20;
            this.pnlConfirmarCancelacion.Controls.Add(this.lblDireccionCorreco_mns);
            this.pnlConfirmarCancelacion.Controls.Add(this.btnCancelar_confCancela);
            this.pnlConfirmarCancelacion.Controls.Add(this.btnConfirmarCancelacion);
            this.pnlConfirmarCancelacion.FillColor = System.Drawing.Color.White;
            this.pnlConfirmarCancelacion.Location = new System.Drawing.Point(55, 66);
            this.pnlConfirmarCancelacion.Name = "pnlConfirmarCancelacion";
            this.pnlConfirmarCancelacion.Size = new System.Drawing.Size(475, 272);
            this.pnlConfirmarCancelacion.TabIndex = 2;
            // 
            // lblDireccionCorreco_mns
            // 
            this.lblDireccionCorreco_mns.AutoSize = true;
            this.lblDireccionCorreco_mns.BackColor = System.Drawing.Color.Transparent;
            this.lblDireccionCorreco_mns.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblDireccionCorreco_mns.Location = new System.Drawing.Point(107, 99);
            this.lblDireccionCorreco_mns.Name = "lblDireccionCorreco_mns";
            this.lblDireccionCorreco_mns.Size = new System.Drawing.Size(249, 25);
            this.lblDireccionCorreco_mns.TabIndex = 10;
            this.lblDireccionCorreco_mns.Text = "¿Desea cancelar su vuelo?";
            // 
            // btnCancelar_confCancela
            // 
            this.btnCancelar_confCancela.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar_confCancela.BorderRadius = 20;
            this.btnCancelar_confCancela.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar_confCancela.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancelar_confCancela.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancelar_confCancela.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancelar_confCancela.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancelar_confCancela.FillColor = System.Drawing.Color.Silver;
            this.btnCancelar_confCancela.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar_confCancela.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar_confCancela.Location = new System.Drawing.Point(43, 158);
            this.btnCancelar_confCancela.Name = "btnCancelar_confCancela";
            this.btnCancelar_confCancela.Size = new System.Drawing.Size(195, 41);
            this.btnCancelar_confCancela.TabIndex = 1;
            this.btnCancelar_confCancela.Text = "Cancelar";
            this.btnCancelar_confCancela.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnConfirmarCancelacion
            // 
            this.btnConfirmarCancelacion.BorderRadius = 20;
            this.btnConfirmarCancelacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmarCancelacion.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnConfirmarCancelacion.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnConfirmarCancelacion.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnConfirmarCancelacion.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnConfirmarCancelacion.FillColor = System.Drawing.Color.RoyalBlue;
            this.btnConfirmarCancelacion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarCancelacion.ForeColor = System.Drawing.Color.White;
            this.btnConfirmarCancelacion.Location = new System.Drawing.Point(254, 158);
            this.btnConfirmarCancelacion.Name = "btnConfirmarCancelacion";
            this.btnConfirmarCancelacion.Size = new System.Drawing.Size(190, 41);
            this.btnConfirmarCancelacion.TabIndex = 0;
            this.btnConfirmarCancelacion.Text = "Confirmar";
            this.btnConfirmarCancelacion.Click += new System.EventHandler(this.btnConfirmarCancelacion_Click);
            // 
            // Uc_ConfirmarCancelacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlConfirmarCancelacion);
            this.Name = "Uc_ConfirmarCancelacion";
            this.Size = new System.Drawing.Size(588, 652);
            this.pnlConfirmarCancelacion.ResumeLayout(false);
            this.pnlConfirmarCancelacion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlConfirmarCancelacion;
        private System.Windows.Forms.Label lblDireccionCorreco_mns;
        private Guna.UI2.WinForms.Guna2Button btnCancelar_confCancela;
        private Guna.UI2.WinForms.Guna2Button btnConfirmarCancelacion;
    }
}
