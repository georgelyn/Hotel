﻿namespace Hotel
{
    partial class Cliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cliente));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevoCliente = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnMostrarClientes = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNuevaReservacion = new System.Windows.Forms.Button();
            this.btnVerReservaciones = new System.Windows.Forms.Button();
            this.lblReservaciones = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRegistrarVehiculo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnVerVehiculos = new System.Windows.Forms.Button();
            this.lblVehiculos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNotas = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.lblCedula = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtEdad = new System.Windows.Forms.TextBox();
            this.lblEdad = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTelefono2 = new System.Windows.Forms.TextBox();
            this.txtTelefono1 = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightBlue;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevoCliente,
            this.btnModificar,
            this.btnMostrarClientes,
            this.btnEliminar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 10);
            this.toolStrip1.Size = new System.Drawing.Size(970, 41);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevoCliente
            // 
            this.btnNuevoCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoCliente.Image")));
            this.btnNuevoCliente.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevoCliente.Name = "btnNuevoCliente";
            this.btnNuevoCliente.Size = new System.Drawing.Size(120, 24);
            this.btnNuevoCliente.Text = "Nuevo cliente";
            this.btnNuevoCliente.Click += new System.EventHandler(this.btnNuevoCliente_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(93, 24);
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnMostrarClientes
            // 
            this.btnMostrarClientes.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnMostrarClientes.Image = ((System.Drawing.Image)(resources.GetObject("btnMostrarClientes.Image")));
            this.btnMostrarClientes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMostrarClientes.Name = "btnMostrarClientes";
            this.btnMostrarClientes.Size = new System.Drawing.Size(134, 24);
            this.btnMostrarClientes.Text = "Mostrar clientes";
            this.btnMostrarClientes.Click += new System.EventHandler(this.btnMostrarClientes_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(83, 24);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(970, 528);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnNuevaReservacion);
            this.panel2.Controls.Add(this.btnVerReservaciones);
            this.panel2.Controls.Add(this.lblReservaciones);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.btnRegistrarVehiculo);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Controls.Add(this.btnGuardar);
            this.panel2.Controls.Add(this.btnVerVehiculos);
            this.panel2.Controls.Add(this.lblVehiculos);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtNotas);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtCedula);
            this.panel2.Controls.Add(this.lblCedula);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtApellido);
            this.panel2.Controls.Add(this.lblApellido);
            this.panel2.Controls.Add(this.txtEdad);
            this.panel2.Controls.Add(this.lblEdad);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtTelefono2);
            this.panel2.Controls.Add(this.txtTelefono1);
            this.panel2.Controls.Add(this.txtNombre);
            this.panel2.Controls.Add(this.lblTelefono);
            this.panel2.Controls.Add(this.lblNombre);
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(13, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(945, 510);
            this.panel2.TabIndex = 0;
            this.panel2.Visible = false;
            // 
            // btnNuevaReservacion
            // 
            this.btnNuevaReservacion.Location = new System.Drawing.Point(355, 368);
            this.btnNuevaReservacion.Name = "btnNuevaReservacion";
            this.btnNuevaReservacion.Size = new System.Drawing.Size(221, 28);
            this.btnNuevaReservacion.TabIndex = 147;
            this.btnNuevaReservacion.Text = "Nueva reservación";
            this.btnNuevaReservacion.UseVisualStyleBackColor = true;
            // 
            // btnVerReservaciones
            // 
            this.btnVerReservaciones.Location = new System.Drawing.Point(293, 368);
            this.btnVerReservaciones.Name = "btnVerReservaciones";
            this.btnVerReservaciones.Size = new System.Drawing.Size(56, 28);
            this.btnVerReservaciones.TabIndex = 146;
            this.btnVerReservaciones.Text = "Ver";
            this.btnVerReservaciones.UseVisualStyleBackColor = true;
            this.btnVerReservaciones.Visible = false;
            // 
            // lblReservaciones
            // 
            this.lblReservaciones.AutoSize = true;
            this.lblReservaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReservaciones.Location = new System.Drawing.Point(250, 372);
            this.lblReservaciones.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReservaciones.Name = "lblReservaciones";
            this.lblReservaciones.Size = new System.Drawing.Size(20, 22);
            this.lblReservaciones.TabIndex = 145;
            this.lblReservaciones.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 372);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(171, 20);
            this.label5.TabIndex = 144;
            this.label5.Text = "Reservaciones activas:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightBlue;
            this.panel3.Location = new System.Drawing.Point(14, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 399);
            this.panel3.TabIndex = 140;
            // 
            // btnRegistrarVehiculo
            // 
            this.btnRegistrarVehiculo.Location = new System.Drawing.Point(355, 402);
            this.btnRegistrarVehiculo.Name = "btnRegistrarVehiculo";
            this.btnRegistrarVehiculo.Size = new System.Drawing.Size(221, 28);
            this.btnRegistrarVehiculo.TabIndex = 143;
            this.btnRegistrarVehiculo.Text = "Registrar nuevo vehículo";
            this.btnRegistrarVehiculo.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(465, 143);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 20);
            this.label4.TabIndex = 142;
            this.label4.Text = "*";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightBlue;
            this.panel4.Location = new System.Drawing.Point(920, 43);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 399);
            this.panel4.TabIndex = 141;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(827, 465);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(91, 32);
            this.btnCancelar.TabIndex = 139;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(730, 465);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(91, 32);
            this.btnGuardar.TabIndex = 138;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnVerVehiculos
            // 
            this.btnVerVehiculos.Location = new System.Drawing.Point(293, 402);
            this.btnVerVehiculos.Name = "btnVerVehiculos";
            this.btnVerVehiculos.Size = new System.Drawing.Size(56, 28);
            this.btnVerVehiculos.TabIndex = 137;
            this.btnVerVehiculos.Text = "Ver";
            this.btnVerVehiculos.UseVisualStyleBackColor = true;
            this.btnVerVehiculos.Visible = false;
            // 
            // lblVehiculos
            // 
            this.lblVehiculos.AutoSize = true;
            this.lblVehiculos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehiculos.Location = new System.Drawing.Point(250, 406);
            this.lblVehiculos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVehiculos.Name = "lblVehiculos";
            this.lblVehiculos.Size = new System.Drawing.Size(20, 22);
            this.lblVehiculos.TabIndex = 136;
            this.lblVehiculos.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 406);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 20);
            this.label3.TabIndex = 135;
            this.label3.Text = "Vehículos registrados:";
            // 
            // txtNotas
            // 
            this.txtNotas.Location = new System.Drawing.Point(280, 226);
            this.txtNotas.Name = "txtNotas";
            this.txtNotas.Size = new System.Drawing.Size(455, 67);
            this.txtNotas.TabIndex = 133;
            this.txtNotas.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 236);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 134;
            this.label2.Text = "Notas:";
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(280, 140);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(180, 26);
            this.txtCedula.TabIndex = 132;
            // 
            // lblCedula
            // 
            this.lblCedula.AutoSize = true;
            this.lblCedula.Location = new System.Drawing.Point(174, 143);
            this.lblCedula.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCedula.Name = "lblCedula";
            this.lblCedula.Size = new System.Drawing.Size(63, 20);
            this.lblCedula.TabIndex = 131;
            this.lblCedula.Text = "Cédula:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(712, 101);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 20);
            this.label7.TabIndex = 130;
            this.label7.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(712, 59);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 20);
            this.label6.TabIndex = 129;
            this.label6.Text = "*";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(280, 98);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(425, 26);
            this.txtApellido.TabIndex = 128;
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Location = new System.Drawing.Point(174, 101);
            this.lblApellido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(87, 20);
            this.lblApellido.TabIndex = 127;
            this.lblApellido.Text = "Apellido(s):";
            // 
            // txtEdad
            // 
            this.txtEdad.Location = new System.Drawing.Point(554, 140);
            this.txtEdad.Name = "txtEdad";
            this.txtEdad.Size = new System.Drawing.Size(77, 26);
            this.txtEdad.TabIndex = 119;
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.Location = new System.Drawing.Point(496, 143);
            this.lblEdad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(51, 20);
            this.lblEdad.TabIndex = 126;
            this.lblEdad.Text = "Edad:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(449, 186);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 20);
            this.label1.TabIndex = 125;
            this.label1.Text = "/";
            // 
            // txtTelefono2
            // 
            this.txtTelefono2.Location = new System.Drawing.Point(469, 183);
            this.txtTelefono2.Name = "txtTelefono2";
            this.txtTelefono2.Size = new System.Drawing.Size(162, 26);
            this.txtTelefono2.TabIndex = 121;
            // 
            // txtTelefono1
            // 
            this.txtTelefono1.Location = new System.Drawing.Point(280, 183);
            this.txtTelefono1.Name = "txtTelefono1";
            this.txtTelefono1.Size = new System.Drawing.Size(162, 26);
            this.txtTelefono1.TabIndex = 120;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(280, 56);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(425, 26);
            this.txtNombre.TabIndex = 118;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(174, 186);
            this.lblTelefono.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(93, 20);
            this.lblTelefono.TabIndex = 124;
            this.lblTelefono.Text = "Teléfono(s):";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(174, 59);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(87, 20);
            this.lblNombre.TabIndex = 123;
            this.lblNombre.Text = "Nombre(s):";
            // 
            // Cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 555);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de clientes";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnMostrarClientes;
        private System.Windows.Forms.ToolStripButton btnNuevoCliente;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.Label lblCedula;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtEdad;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTelefono2;
        private System.Windows.Forms.TextBox txtTelefono1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.RichTextBox txtNotas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnVerVehiculos;
        private System.Windows.Forms.Label lblVehiculos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnRegistrarVehiculo;
        private System.Windows.Forms.Button btnNuevaReservacion;
        private System.Windows.Forms.Button btnVerReservaciones;
        private System.Windows.Forms.Label lblReservaciones;
        private System.Windows.Forms.Label label5;
    }
}