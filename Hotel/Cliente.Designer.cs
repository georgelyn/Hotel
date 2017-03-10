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
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblBuscar = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.txtBuscar = new System.Windows.Forms.ToolStripTextBox();
            this.btnBuscar = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevaReservacion = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelClienteDesde = new System.Windows.Forms.Panel();
            this.lblClienteDesde = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnVerReservacion = new System.Windows.Forms.Button();
            this.dtClienteDesde = new System.Windows.Forms.DateTimePicker();
            this.panelListboxHabitaciones = new System.Windows.Forms.Panel();
            this.listboxReservaciones = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnVerReservaciones = new System.Windows.Forms.Button();
            this.lblReservaciones = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
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
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelClienteDesde.SuspendLayout();
            this.panelListboxHabitaciones.SuspendLayout();
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
            this.toolStripComboBox1,
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.lblBuscar,
            this.toolStripButton1,
            this.btnEliminar,
            this.txtBuscar,
            this.btnBuscar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 5, 5);
            this.toolStrip1.Size = new System.Drawing.Size(903, 34);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevoCliente
            // 
            this.btnNuevoCliente.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevoCliente.Name = "btnNuevoCliente";
            this.btnNuevoCliente.Size = new System.Drawing.Size(104, 24);
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
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Todos los clientes",
            "Clientes actuales",
            "Habitaciones ocupadas"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(200, 27);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(61, 24);
            this.toolStripLabel1.Text = "Mostrar:";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // lblBuscar
            // 
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(53, 24);
            this.lblBuscar.Text = "Buscar:";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(67, 24);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(180, 27);
            this.txtBuscar.ToolTipText = "Buscar cliente por nombre o cédula";
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            // 
            // btnBuscar
            // 
            this.btnBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBuscar.Image = global::Hotel.Properties.Resources.search16;
            this.btnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(23, 24);
            this.btnBuscar.Text = "toolStripButton1";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(903, 528);
            this.panel1.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightBlue;
            this.panel5.Controls.Add(this.btnCancelar);
            this.panel5.Controls.Add(this.btnGuardar);
            this.panel5.Controls.Add(this.btnNuevaReservacion);
            this.panel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 475);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(909, 50);
            this.panel5.TabIndex = 153;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(800, 7);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(91, 36);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(700, 7);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(91, 36);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnNuevaReservacion
            // 
            this.btnNuevaReservacion.BackColor = System.Drawing.Color.Teal;
            this.btnNuevaReservacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaReservacion.ForeColor = System.Drawing.Color.White;
            this.btnNuevaReservacion.Location = new System.Drawing.Point(17, 7);
            this.btnNuevaReservacion.Name = "btnNuevaReservacion";
            this.btnNuevaReservacion.Size = new System.Drawing.Size(163, 36);
            this.btnNuevaReservacion.TabIndex = 8;
            this.btnNuevaReservacion.Text = "Nueva reservación";
            this.btnNuevaReservacion.UseVisualStyleBackColor = false;
            this.btnNuevaReservacion.Click += new System.EventHandler(this.btnNuevaReservacion_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelClienteDesde);
            this.panel2.Controls.Add(this.btnVerReservacion);
            this.panel2.Controls.Add(this.dtClienteDesde);
            this.panel2.Controls.Add(this.panelListboxHabitaciones);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.btnVerReservaciones);
            this.panel2.Controls.Add(this.lblReservaciones);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.panel4);
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
            this.panel2.Location = new System.Drawing.Point(3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 521);
            this.panel2.TabIndex = 0;
            this.panel2.Visible = false;
            // 
            // panelClienteDesde
            // 
            this.panelClienteDesde.Controls.Add(this.lblClienteDesde);
            this.panelClienteDesde.Controls.Add(this.label8);
            this.panelClienteDesde.Location = new System.Drawing.Point(602, 16);
            this.panelClienteDesde.Name = "panelClienteDesde";
            this.panelClienteDesde.Size = new System.Drawing.Size(240, 34);
            this.panelClienteDesde.TabIndex = 158;
            this.panelClienteDesde.Visible = false;
            // 
            // lblClienteDesde
            // 
            this.lblClienteDesde.AutoSize = true;
            this.lblClienteDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClienteDesde.Location = new System.Drawing.Point(127, 10);
            this.lblClienteDesde.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClienteDesde.Name = "lblClienteDesde";
            this.lblClienteDesde.Size = new System.Drawing.Size(104, 16);
            this.lblClienteDesde.TabIndex = 157;
            this.lblClienteDesde.Text = "dd/MMM/yyyy";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(11, 10);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 16);
            this.label8.TabIndex = 153;
            this.label8.Text = "Cliente desde:";
            // 
            // btnVerReservacion
            // 
            this.btnVerReservacion.Image = global::Hotel.Properties.Resources.eye_icon;
            this.btnVerReservacion.Location = new System.Drawing.Point(838, 376);
            this.btnVerReservacion.Name = "btnVerReservacion";
            this.btnVerReservacion.Size = new System.Drawing.Size(34, 28);
            this.btnVerReservacion.TabIndex = 156;
            this.btnVerReservacion.UseVisualStyleBackColor = true;
            this.btnVerReservacion.Visible = false;
            this.btnVerReservacion.Click += new System.EventHandler(this.btnVerReservacion_Click);
            // 
            // dtClienteDesde
            // 
            this.dtClienteDesde.CustomFormat = "dd/MMM/yyyy";
            this.dtClienteDesde.Enabled = false;
            this.dtClienteDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtClienteDesde.Location = new System.Drawing.Point(78, 24);
            this.dtClienteDesde.Margin = new System.Windows.Forms.Padding(4);
            this.dtClienteDesde.MinDate = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            this.dtClienteDesde.Name = "dtClienteDesde";
            this.dtClienteDesde.Size = new System.Drawing.Size(131, 26);
            this.dtClienteDesde.TabIndex = 155;
            this.dtClienteDesde.Visible = false;
            // 
            // panelListboxHabitaciones
            // 
            this.panelListboxHabitaciones.BackColor = System.Drawing.Color.Teal;
            this.panelListboxHabitaciones.Controls.Add(this.listboxReservaciones);
            this.panelListboxHabitaciones.Location = new System.Drawing.Point(723, 348);
            this.panelListboxHabitaciones.Name = "panelListboxHabitaciones";
            this.panelListboxHabitaciones.Size = new System.Drawing.Size(109, 84);
            this.panelListboxHabitaciones.TabIndex = 152;
            this.panelListboxHabitaciones.Visible = false;
            // 
            // listboxReservaciones
            // 
            this.listboxReservaciones.FormattingEnabled = true;
            this.listboxReservaciones.ItemHeight = 20;
            this.listboxReservaciones.Location = new System.Drawing.Point(8, 0);
            this.listboxReservaciones.Name = "listboxReservaciones";
            this.listboxReservaciones.Size = new System.Drawing.Size(92, 84);
            this.listboxReservaciones.TabIndex = 146;
            this.listboxReservaciones.Visible = false;
            this.listboxReservaciones.SelectedIndexChanged += new System.EventHandler(this.listboxReservaciones_SelectedIndexChanged);
            this.listboxReservaciones.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listboxReservaciones_MouseDoubleClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(353, 410);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(364, 20);
            this.label10.TabIndex = 151;
            this.label10.Text = "|---------------------------------------------------------------------|";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(728, 330);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 15);
            this.label11.TabIndex = 150;
            this.label11.Text = "Habitaciones:";
            this.label11.Visible = false;
            // 
            // btnVerReservaciones
            // 
            this.btnVerReservaciones.Image = global::Hotel.Properties.Resources.eye_icon;
            this.btnVerReservaciones.Location = new System.Drawing.Point(299, 406);
            this.btnVerReservaciones.Name = "btnVerReservaciones";
            this.btnVerReservaciones.Size = new System.Drawing.Size(47, 28);
            this.btnVerReservaciones.TabIndex = 7;
            this.btnVerReservaciones.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVerReservaciones.UseVisualStyleBackColor = true;
            this.btnVerReservaciones.Visible = false;
            this.btnVerReservaciones.Click += new System.EventHandler(this.btnVerReservaciones_Click);
            // 
            // lblReservaciones
            // 
            this.lblReservaciones.AutoSize = true;
            this.lblReservaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReservaciones.Location = new System.Drawing.Point(258, 410);
            this.lblReservaciones.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReservaciones.Name = "lblReservaciones";
            this.lblReservaciones.Size = new System.Drawing.Size(20, 22);
            this.lblReservaciones.TabIndex = 145;
            this.lblReservaciones.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(74, 410);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(171, 20);
            this.label5.TabIndex = 144;
            this.label5.Text = "Reservaciones activas:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightBlue;
            this.panel3.Location = new System.Drawing.Point(14, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 418);
            this.panel3.TabIndex = 140;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(461, 165);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 20);
            this.label4.TabIndex = 142;
            this.label4.Text = "*";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightBlue;
            this.panel4.Location = new System.Drawing.Point(878, 24);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 418);
            this.panel4.TabIndex = 141;
            // 
            // btnVerVehiculos
            // 
            this.btnVerVehiculos.Image = global::Hotel.Properties.Resources.eye_icon;
            this.btnVerVehiculos.Location = new System.Drawing.Point(299, 372);
            this.btnVerVehiculos.Name = "btnVerVehiculos";
            this.btnVerVehiculos.Size = new System.Drawing.Size(47, 28);
            this.btnVerVehiculos.TabIndex = 9;
            this.btnVerVehiculos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVerVehiculos.UseVisualStyleBackColor = true;
            this.btnVerVehiculos.Visible = false;
            this.btnVerVehiculos.Click += new System.EventHandler(this.btnVerVehiculos_Click);
            // 
            // lblVehiculos
            // 
            this.lblVehiculos.AutoSize = true;
            this.lblVehiculos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehiculos.Location = new System.Drawing.Point(258, 376);
            this.lblVehiculos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVehiculos.Name = "lblVehiculos";
            this.lblVehiculos.Size = new System.Drawing.Size(20, 22);
            this.lblVehiculos.TabIndex = 136;
            this.lblVehiculos.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 376);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 20);
            this.label3.TabIndex = 135;
            this.label3.Text = "Vehículos registrados:";
            // 
            // txtNotas
            // 
            this.txtNotas.Location = new System.Drawing.Point(276, 248);
            this.txtNotas.Name = "txtNotas";
            this.txtNotas.Size = new System.Drawing.Size(455, 67);
            this.txtNotas.TabIndex = 6;
            this.txtNotas.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 258);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 134;
            this.label2.Text = "Notas:";
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(276, 162);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(180, 26);
            this.txtCedula.TabIndex = 2;
            this.txtCedula.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCedula_KeyPress);
            // 
            // lblCedula
            // 
            this.lblCedula.AutoSize = true;
            this.lblCedula.Location = new System.Drawing.Point(170, 165);
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
            this.label7.Location = new System.Drawing.Point(708, 123);
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
            this.label6.Location = new System.Drawing.Point(708, 81);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 20);
            this.label6.TabIndex = 129;
            this.label6.Text = "*";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(276, 120);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(425, 26);
            this.txtApellido.TabIndex = 1;
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Location = new System.Drawing.Point(170, 123);
            this.lblApellido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(87, 20);
            this.lblApellido.TabIndex = 127;
            this.lblApellido.Text = "Apellido(s):";
            // 
            // txtEdad
            // 
            this.txtEdad.Location = new System.Drawing.Point(550, 162);
            this.txtEdad.Name = "txtEdad";
            this.txtEdad.Size = new System.Drawing.Size(77, 26);
            this.txtEdad.TabIndex = 3;
            this.txtEdad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCedula_KeyPress);
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.Location = new System.Drawing.Point(492, 165);
            this.lblEdad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(51, 20);
            this.lblEdad.TabIndex = 126;
            this.lblEdad.Text = "Edad:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(445, 208);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 20);
            this.label1.TabIndex = 125;
            this.label1.Text = "/";
            // 
            // txtTelefono2
            // 
            this.txtTelefono2.Location = new System.Drawing.Point(465, 205);
            this.txtTelefono2.Name = "txtTelefono2";
            this.txtTelefono2.Size = new System.Drawing.Size(162, 26);
            this.txtTelefono2.TabIndex = 5;
            this.txtTelefono2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCedula_KeyPress);
            // 
            // txtTelefono1
            // 
            this.txtTelefono1.Location = new System.Drawing.Point(276, 205);
            this.txtTelefono1.Name = "txtTelefono1";
            this.txtTelefono1.Size = new System.Drawing.Size(162, 26);
            this.txtTelefono1.TabIndex = 4;
            this.txtTelefono1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCedula_KeyPress);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(276, 78);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(425, 26);
            this.txtNombre.TabIndex = 0;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(170, 208);
            this.lblTelefono.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(93, 20);
            this.lblTelefono.TabIndex = 124;
            this.lblTelefono.Text = "Teléfono(s):";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(170, 81);
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
            this.ClientSize = new System.Drawing.Size(903, 555);
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
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelClienteDesde.ResumeLayout(false);
            this.panelClienteDesde.PerformLayout();
            this.panelListboxHabitaciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
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
        private System.Windows.Forms.Button btnNuevaReservacion;
        private System.Windows.Forms.Button btnVerReservaciones;
        private System.Windows.Forms.Label lblReservaciones;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listboxReservaciones;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripLabel lblBuscar;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox txtBuscar;
        private System.Windows.Forms.ToolStripButton btnBuscar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panelListboxHabitaciones;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtClienteDesde;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.Button btnVerReservacion;
        private System.Windows.Forms.Label lblClienteDesde;
        private System.Windows.Forms.Panel panelClienteDesde;
    }
}