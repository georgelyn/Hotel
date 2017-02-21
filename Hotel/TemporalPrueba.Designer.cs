namespace Hotel
{
    partial class TemporalPrueba
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtEntrada = new System.Windows.Forms.DateTimePicker();
            this.dtSalida = new System.Windows.Forms.DateTimePicker();
            this.lblHab = new System.Windows.Forms.Label();
            this.comboHabitacion = new System.Windows.Forms.ComboBox();
            this.lblFechaIngrso = new System.Windows.Forms.Label();
            this.lblFechaSalida = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dtEntrada);
            this.panel2.Controls.Add(this.dtSalida);
            this.panel2.Controls.Add(this.lblHab);
            this.panel2.Controls.Add(this.comboHabitacion);
            this.panel2.Controls.Add(this.lblFechaIngrso);
            this.panel2.Controls.Add(this.lblFechaSalida);
            this.panel2.Location = new System.Drawing.Point(60, 167);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(859, 93);
            this.panel2.TabIndex = 92;
            // 
            // dtEntrada
            // 
            this.dtEntrada.CustomFormat = "dd/MMM/yyyy h:mm tt";
            this.dtEntrada.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEntrada.Location = new System.Drawing.Point(606, 10);
            this.dtEntrada.Margin = new System.Windows.Forms.Padding(4);
            this.dtEntrada.MinDate = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            this.dtEntrada.Name = "dtEntrada";
            this.dtEntrada.Size = new System.Drawing.Size(215, 20);
            this.dtEntrada.TabIndex = 16;
            // 
            // dtSalida
            // 
            this.dtSalida.CustomFormat = "dd/MMM/yyyy h:mm tt";
            this.dtSalida.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtSalida.Location = new System.Drawing.Point(606, 53);
            this.dtSalida.Margin = new System.Windows.Forms.Padding(4);
            this.dtSalida.MinDate = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            this.dtSalida.Name = "dtSalida";
            this.dtSalida.Size = new System.Drawing.Size(215, 20);
            this.dtSalida.TabIndex = 17;
            this.dtSalida.Value = new System.DateTime(2017, 2, 16, 14, 0, 0, 0);
            // 
            // lblHab
            // 
            this.lblHab.AutoSize = true;
            this.lblHab.Location = new System.Drawing.Point(11, 29);
            this.lblHab.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblHab.Name = "lblHab";
            this.lblHab.Size = new System.Drawing.Size(84, 13);
            this.lblHab.TabIndex = 0;
            this.lblHab.Text = "Nro. Habitación:";
            // 
            // comboHabitacion
            // 
            this.comboHabitacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboHabitacion.FormattingEnabled = true;
            this.comboHabitacion.Location = new System.Drawing.Point(154, 26);
            this.comboHabitacion.Margin = new System.Windows.Forms.Padding(4);
            this.comboHabitacion.Name = "comboHabitacion";
            this.comboHabitacion.Size = new System.Drawing.Size(74, 21);
            this.comboHabitacion.TabIndex = 12;
            // 
            // lblFechaIngrso
            // 
            this.lblFechaIngrso.AutoSize = true;
            this.lblFechaIngrso.Location = new System.Drawing.Point(478, 16);
            this.lblFechaIngrso.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFechaIngrso.Name = "lblFechaIngrso";
            this.lblFechaIngrso.Size = new System.Drawing.Size(79, 13);
            this.lblFechaIngrso.TabIndex = 14;
            this.lblFechaIngrso.Text = "Fecha entrada:";
            // 
            // lblFechaSalida
            // 
            this.lblFechaSalida.AutoSize = true;
            this.lblFechaSalida.Location = new System.Drawing.Point(491, 57);
            this.lblFechaSalida.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFechaSalida.Name = "lblFechaSalida";
            this.lblFechaSalida.Size = new System.Drawing.Size(70, 13);
            this.lblFechaSalida.TabIndex = 15;
            this.lblFechaSalida.Text = "Fecha salida:";
            // 
            // TemporalPrueba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 612);
            this.Controls.Add(this.panel2);
            this.Name = "TemporalPrueba";
            this.Text = "TemporalPrueba";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtEntrada;
        private System.Windows.Forms.DateTimePicker dtSalida;
        private System.Windows.Forms.Label lblHab;
        private System.Windows.Forms.ComboBox comboHabitacion;
        private System.Windows.Forms.Label lblFechaIngrso;
        private System.Windows.Forms.Label lblFechaSalida;
    }
}