namespace Aeropuerto.ControlUsuario
{
    partial class Uc_ReagendarPasaje
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
            this.btnReagendarPasaje = new Guna.UI2.WinForms.Guna2Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtmFechaViaje_ReagendarVuelo = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.mns_estado = new System.Windows.Forms.Label();
            this.lb_Estado_ReagendarPasaje = new System.Windows.Forms.Label();
            this.lb_Origen_ReagPasaje = new System.Windows.Forms.Label();
            this.lb_Destino_ReagendarPasaje = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_CantidadPasajeros_ReagendarPasajes = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReagendarPasaje
            // 
            this.btnReagendarPasaje.BorderRadius = 15;
            this.btnReagendarPasaje.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReagendarPasaje.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnReagendarPasaje.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnReagendarPasaje.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnReagendarPasaje.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnReagendarPasaje.FillColor = System.Drawing.Color.RoyalBlue;
            this.btnReagendarPasaje.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnReagendarPasaje.ForeColor = System.Drawing.Color.White;
            this.btnReagendarPasaje.Location = new System.Drawing.Point(259, 348);
            this.btnReagendarPasaje.Name = "btnReagendarPasaje";
            this.btnReagendarPasaje.Size = new System.Drawing.Size(189, 32);
            this.btnReagendarPasaje.TabIndex = 94;
            this.btnReagendarPasaje.Text = "Aplicar cambio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F);
            this.label3.Location = new System.Drawing.Point(122, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 13);
            this.label3.TabIndex = 91;
            this.label3.Text = "Seleccione la nueva fecha en la que desea viajar";
            // 
            // dtmFechaViaje_ReagendarVuelo
            // 
            this.dtmFechaViaje_ReagendarVuelo.BackColor = System.Drawing.Color.Transparent;
            this.dtmFechaViaje_ReagendarVuelo.BorderRadius = 10;
            this.dtmFechaViaje_ReagendarVuelo.Checked = true;
            this.dtmFechaViaje_ReagendarVuelo.FillColor = System.Drawing.Color.White;
            this.dtmFechaViaje_ReagendarVuelo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtmFechaViaje_ReagendarVuelo.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtmFechaViaje_ReagendarVuelo.Location = new System.Drawing.Point(125, 292);
            this.dtmFechaViaje_ReagendarVuelo.MaxDate = new System.DateTime(2025, 10, 20, 0, 0, 0, 0);
            this.dtmFechaViaje_ReagendarVuelo.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtmFechaViaje_ReagendarVuelo.Name = "dtmFechaViaje_ReagendarVuelo";
            this.dtmFechaViaje_ReagendarVuelo.Size = new System.Drawing.Size(222, 23);
            this.dtmFechaViaje_ReagendarVuelo.TabIndex = 90;
            this.dtmFechaViaje_ReagendarVuelo.Value = new System.DateTime(2025, 10, 20, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F);
            this.label2.Location = new System.Drawing.Point(421, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "Destino";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F);
            this.label1.Location = new System.Drawing.Point(122, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 85;
            this.label1.Text = "Origen";
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.AutoSize = false;
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Segoe UI", 15.25F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(264, 19);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(222, 30);
            this.guna2HtmlLabel4.TabIndex = 80;
            this.guna2HtmlLabel4.Text = "Reagendar Vuelo";
            // 
            // mns_estado
            // 
            this.mns_estado.AutoSize = true;
            this.mns_estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F);
            this.mns_estado.Location = new System.Drawing.Point(122, 68);
            this.mns_estado.Name = "mns_estado";
            this.mns_estado.Size = new System.Drawing.Size(43, 13);
            this.mns_estado.TabIndex = 97;
            this.mns_estado.Text = "Estado:";
            // 
            // lb_Estado_ReagendarPasaje
            // 
            this.lb_Estado_ReagendarPasaje.AutoSize = true;
            this.lb_Estado_ReagendarPasaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F);
            this.lb_Estado_ReagendarPasaje.Location = new System.Drawing.Point(186, 68);
            this.lb_Estado_ReagendarPasaje.Name = "lb_Estado_ReagendarPasaje";
            this.lb_Estado_ReagendarPasaje.Size = new System.Drawing.Size(63, 13);
            this.lb_Estado_ReagendarPasaje.TabIndex = 98;
            this.lb_Estado_ReagendarPasaje.Text = "Ida y Vuelta";
            // 
            // lb_Origen_ReagPasaje
            // 
            this.lb_Origen_ReagPasaje.AutoSize = true;
            this.lb_Origen_ReagPasaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F);
            this.lb_Origen_ReagPasaje.Location = new System.Drawing.Point(122, 189);
            this.lb_Origen_ReagPasaje.Name = "lb_Origen_ReagPasaje";
            this.lb_Origen_ReagPasaje.Size = new System.Drawing.Size(38, 13);
            this.lb_Origen_ReagPasaje.TabIndex = 99;
            this.lb_Origen_ReagPasaje.Text = "Origen";
            // 
            // lb_Destino_ReagendarPasaje
            // 
            this.lb_Destino_ReagendarPasaje.AutoSize = true;
            this.lb_Destino_ReagendarPasaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F);
            this.lb_Destino_ReagendarPasaje.Location = new System.Drawing.Point(421, 189);
            this.lb_Destino_ReagendarPasaje.Name = "lb_Destino_ReagendarPasaje";
            this.lb_Destino_ReagendarPasaje.Size = new System.Drawing.Size(43, 13);
            this.lb_Destino_ReagendarPasaje.TabIndex = 100;
            this.lb_Destino_ReagendarPasaje.Text = "Destino";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F);
            this.label4.Location = new System.Drawing.Point(122, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 92;
            this.label4.Text = "Cantidad de pasajeros";
            // 
            // lb_CantidadPasajeros_ReagendarPasajes
            // 
            this.lb_CantidadPasajeros_ReagendarPasajes.AutoSize = true;
            this.lb_CantidadPasajeros_ReagendarPasajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F);
            this.lb_CantidadPasajeros_ReagendarPasajes.Location = new System.Drawing.Point(122, 235);
            this.lb_CantidadPasajeros_ReagendarPasajes.Name = "lb_CantidadPasajeros_ReagendarPasajes";
            this.lb_CantidadPasajeros_ReagendarPasajes.Size = new System.Drawing.Size(112, 13);
            this.lb_CantidadPasajeros_ReagendarPasajes.TabIndex = 101;
            this.lb_CantidadPasajeros_ReagendarPasajes.Text = "Cantidad de pasajeros";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Aeropuerto.Properties.Resources.flecha_2;
            this.pictureBox3.Location = new System.Drawing.Point(278, 102);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(109, 57);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 88;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Aeropuerto.Properties.Resources.avion_b;
            this.pictureBox1.Location = new System.Drawing.Point(424, 109);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 87;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Aeropuerto.Properties.Resources.avion_b;
            this.pictureBox2.Location = new System.Drawing.Point(125, 109);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 83;
            this.pictureBox2.TabStop = false;
            // 
            // Uc_ReagendarPasaje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lb_CantidadPasajeros_ReagendarPasajes);
            this.Controls.Add(this.lb_Destino_ReagendarPasaje);
            this.Controls.Add(this.lb_Origen_ReagPasaje);
            this.Controls.Add(this.lb_Estado_ReagendarPasaje);
            this.Controls.Add(this.mns_estado);
            this.Controls.Add(this.btnReagendarPasaje);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtmFechaViaje_ReagendarVuelo);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.guna2HtmlLabel4);
            this.Name = "Uc_ReagendarPasaje";
            this.Size = new System.Drawing.Size(701, 646);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnReagendarPasaje;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtmFechaViaje_ReagendarVuelo;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private System.Windows.Forms.Label mns_estado;
        private System.Windows.Forms.Label lb_Estado_ReagendarPasaje;
        private System.Windows.Forms.Label lb_Origen_ReagPasaje;
        private System.Windows.Forms.Label lb_Destino_ReagendarPasaje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_CantidadPasajeros_ReagendarPasajes;
    }
}
