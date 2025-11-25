namespace Aeropuerto
{
    partial class Uc_SobrecostoReagendarVuelo
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
            this.VerPoliticas_Reagendamiento = new System.Windows.Forms.Label();
            this.lblDireccionCorreco_mns = new System.Windows.Forms.Label();
            this.btnCancelar_ReagendarVuelo = new Guna.UI2.WinForms.Guna2Button();
            this.btnConfirmar_ReagendarVuelo = new Guna.UI2.WinForms.Guna2Button();
            this.pnlConfirmarCancelacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlConfirmarCancelacion
            // 
            this.pnlConfirmarCancelacion.BorderRadius = 20;
            this.pnlConfirmarCancelacion.Controls.Add(this.VerPoliticas_Reagendamiento);
            this.pnlConfirmarCancelacion.Controls.Add(this.lblDireccionCorreco_mns);
            this.pnlConfirmarCancelacion.Controls.Add(this.btnCancelar_ReagendarVuelo);
            this.pnlConfirmarCancelacion.Controls.Add(this.btnConfirmar_ReagendarVuelo);
            this.pnlConfirmarCancelacion.FillColor = System.Drawing.Color.White;
            this.pnlConfirmarCancelacion.Location = new System.Drawing.Point(114, 124);
            this.pnlConfirmarCancelacion.Name = "pnlConfirmarCancelacion";
            this.pnlConfirmarCancelacion.Size = new System.Drawing.Size(475, 272);
            this.pnlConfirmarCancelacion.TabIndex = 3;
            // 
            // VerPoliticas_Reagendamiento
            // 
            this.VerPoliticas_Reagendamiento.AutoSize = true;
            this.VerPoliticas_Reagendamiento.BackColor = System.Drawing.SystemColors.HighlightText;
            this.VerPoliticas_Reagendamiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerPoliticas_Reagendamiento.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.VerPoliticas_Reagendamiento.Location = new System.Drawing.Point(150, 222);
            this.VerPoliticas_Reagendamiento.Name = "VerPoliticas_Reagendamiento";
            this.VerPoliticas_Reagendamiento.Size = new System.Drawing.Size(190, 15);
            this.VerPoliticas_Reagendamiento.TabIndex = 11;
            this.VerPoliticas_Reagendamiento.Text = "Ver Políticas de Reagendamiento";
            this.VerPoliticas_Reagendamiento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VerPoliticas_Reagendamiento.Click += new System.EventHandler(this.VerPoliticas_Reagendamiento_Click);
            // 
            // lblDireccionCorreco_mns
            // 
            this.lblDireccionCorreco_mns.AutoSize = true;
            this.lblDireccionCorreco_mns.BackColor = System.Drawing.Color.Transparent;
            this.lblDireccionCorreco_mns.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblDireccionCorreco_mns.Location = new System.Drawing.Point(65, 74);
            this.lblDireccionCorreco_mns.Name = "lblDireccionCorreco_mns";
            this.lblDireccionCorreco_mns.Size = new System.Drawing.Size(358, 50);
            this.lblDireccionCorreco_mns.TabIndex = 10;
            this.lblDireccionCorreco_mns.Text = "El reagendamiento del vuelo aplicará un\n sobrecosto sobre el total a pagar";
            this.lblDireccionCorreco_mns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelar_ReagendarVuelo
            // 
            this.btnCancelar_ReagendarVuelo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar_ReagendarVuelo.BorderRadius = 20;
            this.btnCancelar_ReagendarVuelo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar_ReagendarVuelo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancelar_ReagendarVuelo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancelar_ReagendarVuelo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancelar_ReagendarVuelo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancelar_ReagendarVuelo.FillColor = System.Drawing.Color.Silver;
            this.btnCancelar_ReagendarVuelo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar_ReagendarVuelo.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar_ReagendarVuelo.Location = new System.Drawing.Point(43, 158);
            this.btnCancelar_ReagendarVuelo.Name = "btnCancelar_ReagendarVuelo";
            this.btnCancelar_ReagendarVuelo.Size = new System.Drawing.Size(195, 41);
            this.btnCancelar_ReagendarVuelo.TabIndex = 1;
            this.btnCancelar_ReagendarVuelo.Text = "Cancelar";
            this.btnCancelar_ReagendarVuelo.Click += new System.EventHandler(this.btnCancelar_ReagendarVuelo_Click);
            // 
            // btnConfirmar_ReagendarVuelo
            // 
            this.btnConfirmar_ReagendarVuelo.BorderRadius = 20;
            this.btnConfirmar_ReagendarVuelo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar_ReagendarVuelo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnConfirmar_ReagendarVuelo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnConfirmar_ReagendarVuelo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnConfirmar_ReagendarVuelo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnConfirmar_ReagendarVuelo.FillColor = System.Drawing.Color.RoyalBlue;
            this.btnConfirmar_ReagendarVuelo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar_ReagendarVuelo.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar_ReagendarVuelo.Location = new System.Drawing.Point(254, 158);
            this.btnConfirmar_ReagendarVuelo.Name = "btnConfirmar_ReagendarVuelo";
            this.btnConfirmar_ReagendarVuelo.Size = new System.Drawing.Size(190, 41);
            this.btnConfirmar_ReagendarVuelo.TabIndex = 0;
            this.btnConfirmar_ReagendarVuelo.Text = "Confirmar";
            this.btnConfirmar_ReagendarVuelo.Click += new System.EventHandler(this.btnConfirmar_ReagendarVuelo_Click);
            // 
            // Uc_SobrecostoReagendarVuelo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlConfirmarCancelacion);
            this.Name = "Uc_SobrecostoReagendarVuelo";
            this.Size = new System.Drawing.Size(701, 646);
            this.pnlConfirmarCancelacion.ResumeLayout(false);
            this.pnlConfirmarCancelacion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlConfirmarCancelacion;
        private System.Windows.Forms.Label lblDireccionCorreco_mns;
        private Guna.UI2.WinForms.Guna2Button btnCancelar_ReagendarVuelo;
        private Guna.UI2.WinForms.Guna2Button btnConfirmar_ReagendarVuelo;
        private System.Windows.Forms.Label VerPoliticas_Reagendamiento;
    }
}
