namespace Hotel
{
    partial class Vehiculo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboVehiculo = new System.Windows.Forms.ComboBox();
            this.checkCamion = new System.Windows.Forms.CheckBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.lblMarca = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.lblModelo = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.comboCedula = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNotas = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vehículos asociados con cédula:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Elija uno de la lista:";
            // 
            // comboVehiculo
            // 
            this.comboVehiculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboVehiculo.FormattingEnabled = true;
            this.comboVehiculo.Location = new System.Drawing.Point(243, 78);
            this.comboVehiculo.Name = "comboVehiculo";
            this.comboVehiculo.Size = new System.Drawing.Size(266, 28);
            this.comboVehiculo.TabIndex = 0;
            this.comboVehiculo.SelectedIndexChanged += new System.EventHandler(this.comboVehiculo_SelectedIndexChanged);
            // 
            // checkCamion
            // 
            this.checkCamion.AutoSize = true;
            this.checkCamion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkCamion.Location = new System.Drawing.Point(525, 150);
            this.checkCamion.Name = "checkCamion";
            this.checkCamion.Size = new System.Drawing.Size(82, 24);
            this.checkCamion.TabIndex = 49;
            this.checkCamion.Text = "Camión";
            this.checkCamion.UseVisualStyleBackColor = true;
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(368, 149);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(124, 26);
            this.txtPlaca.TabIndex = 1;
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.Location = new System.Drawing.Point(73, 151);
            this.lblMarca.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(57, 20);
            this.lblMarca.TabIndex = 55;
            this.lblMarca.Text = "Marca:";
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(141, 201);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(351, 26);
            this.txtModelo.TabIndex = 2;
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(141, 148);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(156, 26);
            this.txtMarca.TabIndex = 0;
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModelo.Location = new System.Drawing.Point(73, 204);
            this.lblModelo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(65, 20);
            this.lblModelo.TabIndex = 56;
            this.lblModelo.Text = "Modelo:";
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaca.Location = new System.Drawing.Point(309, 151);
            this.lblPlaca.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(52, 20);
            this.lblPlaca.TabIndex = 57;
            this.lblPlaca.Text = "Placa:";
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(462, 6);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(95, 35);
            this.btnModificar.TabIndex = 58;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Visible = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(563, 6);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 35);
            this.btnCancelar.TabIndex = 59;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightBlue;
            this.panel1.Controls.Add(this.btnEliminar);
            this.panel1.Controls.Add(this.btnModificar);
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 357);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(666, 51);
            this.panel1.TabIndex = 60;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Red;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEliminar.Location = new System.Drawing.Point(16, 6);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(87, 34);
            this.btnEliminar.TabIndex = 60;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Visible = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // comboCedula
            // 
            this.comboCedula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCedula.FormattingEnabled = true;
            this.comboCedula.Location = new System.Drawing.Point(314, 34);
            this.comboCedula.Name = "comboCedula";
            this.comboCedula.Size = new System.Drawing.Size(195, 28);
            this.comboCedula.TabIndex = 61;
            this.comboCedula.SelectedIndexChanged += new System.EventHandler(this.comboCedula_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(73, 253);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 62;
            this.label2.Text = "Notas:";
            // 
            // txtNotas
            // 
            this.txtNotas.Location = new System.Drawing.Point(141, 250);
            this.txtNotas.Name = "txtNotas";
            this.txtNotas.Size = new System.Drawing.Size(351, 73);
            this.txtNotas.TabIndex = 63;
            this.txtNotas.Text = "";
            // 
            // Vehiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 408);
            this.Controls.Add(this.txtNotas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboCedula);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkCamion);
            this.Controls.Add(this.txtPlaca);
            this.Controls.Add(this.lblMarca);
            this.Controls.Add(this.comboVehiculo);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMarca);
            this.Controls.Add(this.lblModelo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPlaca);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Vehiculo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vehiculo";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboVehiculo;
        private System.Windows.Forms.CheckBox checkCamion;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtNotas;
        internal System.Windows.Forms.ComboBox comboCedula;
    }
}