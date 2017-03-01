namespace Hotel
{
    partial class Reservacion
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.panelCedula = new System.Windows.Forms.Panel();
            this.btnCheckCedula = new System.Windows.Forms.Button();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.lblCedula = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.linkLblCambiarNumHab = new System.Windows.Forms.LinkLabel();
            this.dtEntrada = new System.Windows.Forms.DateTimePicker();
            this.dtSalida = new System.Windows.Forms.DateTimePicker();
            this.lblHab = new System.Windows.Forms.Label();
            this.comboHabitacion = new System.Windows.Forms.ComboBox();
            this.lblFechaIngrso = new System.Windows.Forms.Label();
            this.lblFechaSalida = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listboxHabitaciones = new System.Windows.Forms.ListBox();
            this.txtNotas = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkCamion = new System.Windows.Forms.CheckBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.lblMarca = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.lblModelo = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.txtEdad = new System.Windows.Forms.TextBox();
            this.lblEdad = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTelefono2 = new System.Windows.Forms.TextBox();
            this.txtTelefono1 = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblHabitacionActual = new System.Windows.Forms.Label();
            this.lblHabitacionNumero = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            this.panelCedula.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.btnEliminar);
            this.panel1.Controls.Add(this.btnModificar);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 538);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(912, 55);
            this.panel1.TabIndex = 54;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(611, 10);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(87, 34);
            this.btnEliminar.TabIndex = 58;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Visible = false;
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(518, 10);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(87, 34);
            this.btnModificar.TabIndex = 57;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Visible = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(704, 10);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(87, 34);
            this.btnAceptar.TabIndex = 55;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(797, 9);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(87, 34);
            this.btnCancelar.TabIndex = 56;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.panelCedula);
            this.panelContenedor.Controls.Add(this.panel2);
            this.panelContenedor.Controls.Add(this.label3);
            this.panelContenedor.Controls.Add(this.listboxHabitaciones);
            this.panelContenedor.Controls.Add(this.txtNotas);
            this.panelContenedor.Controls.Add(this.label2);
            this.panelContenedor.Controls.Add(this.label4);
            this.panelContenedor.Controls.Add(this.groupBox1);
            this.panelContenedor.Controls.Add(this.txtEdad);
            this.panelContenedor.Controls.Add(this.lblEdad);
            this.panelContenedor.Controls.Add(this.txtTotal);
            this.panelContenedor.Controls.Add(this.lblTotal);
            this.panelContenedor.Controls.Add(this.label1);
            this.panelContenedor.Controls.Add(this.txtTelefono2);
            this.panelContenedor.Controls.Add(this.txtTelefono1);
            this.panelContenedor.Controls.Add(this.txtNombre);
            this.panelContenedor.Controls.Add(this.lblTelefono);
            this.panelContenedor.Controls.Add(this.lblNombre);
            this.panelContenedor.Location = new System.Drawing.Point(0, 0);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(912, 541);
            this.panelContenedor.TabIndex = 55;
            // 
            // panelCedula
            // 
            this.panelCedula.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelCedula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCedula.Controls.Add(this.btnCheckCedula);
            this.panelCedula.Controls.Add(this.txtCedula);
            this.panelCedula.Controls.Add(this.lblCedula);
            this.panelCedula.Location = new System.Drawing.Point(253, 216);
            this.panelCedula.Name = "panelCedula";
            this.panelCedula.Size = new System.Drawing.Size(415, 104);
            this.panelCedula.TabIndex = 113;
            // 
            // btnCheckCedula
            // 
            this.btnCheckCedula.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCheckCedula.Location = new System.Drawing.Point(287, 20);
            this.btnCheckCedula.Name = "btnCheckCedula";
            this.btnCheckCedula.Size = new System.Drawing.Size(105, 61);
            this.btnCheckCedula.TabIndex = 112;
            this.btnCheckCedula.Text = "Aceptar";
            this.btnCheckCedula.UseVisualStyleBackColor = false;
            this.btnCheckCedula.Click += new System.EventHandler(this.btnCheckCedula_Click);
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(135, 37);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(130, 26);
            this.txtCedula.TabIndex = 97;
            // 
            // lblCedula
            // 
            this.lblCedula.AutoSize = true;
            this.lblCedula.Location = new System.Drawing.Point(50, 40);
            this.lblCedula.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCedula.Name = "lblCedula";
            this.lblCedula.Size = new System.Drawing.Size(63, 20);
            this.lblCedula.TabIndex = 93;
            this.lblCedula.Text = "Cédula:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblHabitacionNumero);
            this.panel2.Controls.Add(this.lblHabitacionActual);
            this.panel2.Controls.Add(this.linkLblCambiarNumHab);
            this.panel2.Controls.Add(this.dtEntrada);
            this.panel2.Controls.Add(this.dtSalida);
            this.panel2.Controls.Add(this.lblHab);
            this.panel2.Controls.Add(this.comboHabitacion);
            this.panel2.Controls.Add(this.lblFechaIngrso);
            this.panel2.Controls.Add(this.lblFechaSalida);
            this.panel2.Location = new System.Drawing.Point(29, 26);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(859, 93);
            this.panel2.TabIndex = 91;
            // 
            // linkLblCambiarNumHab
            // 
            this.linkLblCambiarNumHab.AutoSize = true;
            this.linkLblCambiarNumHab.Location = new System.Drawing.Point(235, 29);
            this.linkLblCambiarNumHab.Name = "linkLblCambiarNumHab";
            this.linkLblCambiarNumHab.Size = new System.Drawing.Size(68, 20);
            this.linkLblCambiarNumHab.TabIndex = 18;
            this.linkLblCambiarNumHab.TabStop = true;
            this.linkLblCambiarNumHab.Text = "Cambiar";
            this.linkLblCambiarNumHab.Visible = false;
            this.linkLblCambiarNumHab.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblCambiarNumHab_LinkClicked);
            // 
            // dtEntrada
            // 
            this.dtEntrada.CustomFormat = "dd/MMM/yyyy h:mm tt";
            this.dtEntrada.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEntrada.Location = new System.Drawing.Point(606, 10);
            this.dtEntrada.Margin = new System.Windows.Forms.Padding(4);
            this.dtEntrada.MinDate = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            this.dtEntrada.Name = "dtEntrada";
            this.dtEntrada.Size = new System.Drawing.Size(215, 26);
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
            this.dtSalida.Size = new System.Drawing.Size(215, 26);
            this.dtSalida.TabIndex = 17;
            this.dtSalida.Value = new System.DateTime(2017, 2, 16, 14, 0, 0, 0);
            // 
            // lblHab
            // 
            this.lblHab.AutoSize = true;
            this.lblHab.Location = new System.Drawing.Point(11, 29);
            this.lblHab.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblHab.Name = "lblHab";
            this.lblHab.Size = new System.Drawing.Size(122, 20);
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
            this.comboHabitacion.Size = new System.Drawing.Size(74, 28);
            this.comboHabitacion.TabIndex = 12;
            // 
            // lblFechaIngrso
            // 
            this.lblFechaIngrso.AutoSize = true;
            this.lblFechaIngrso.Location = new System.Drawing.Point(478, 16);
            this.lblFechaIngrso.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFechaIngrso.Name = "lblFechaIngrso";
            this.lblFechaIngrso.Size = new System.Drawing.Size(117, 20);
            this.lblFechaIngrso.TabIndex = 14;
            this.lblFechaIngrso.Text = "Fecha entrada:";
            // 
            // lblFechaSalida
            // 
            this.lblFechaSalida.AutoSize = true;
            this.lblFechaSalida.Location = new System.Drawing.Point(491, 57);
            this.lblFechaSalida.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFechaSalida.Name = "lblFechaSalida";
            this.lblFechaSalida.Size = new System.Drawing.Size(103, 20);
            this.lblFechaSalida.TabIndex = 15;
            this.lblFechaSalida.Text = "Fecha salida:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(699, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 20);
            this.label3.TabIndex = 110;
            this.label3.Text = "Tipo de habitación:";
            // 
            // listboxHabitaciones
            // 
            this.listboxHabitaciones.FormattingEnabled = true;
            this.listboxHabitaciones.ItemHeight = 20;
            this.listboxHabitaciones.Location = new System.Drawing.Point(703, 179);
            this.listboxHabitaciones.Name = "listboxHabitaciones";
            this.listboxHabitaciones.Size = new System.Drawing.Size(185, 164);
            this.listboxHabitaciones.TabIndex = 109;
            this.listboxHabitaciones.SelectedIndexChanged += new System.EventHandler(this.listboxHabitaciones_SelectedIndexChanged);
            // 
            // txtNotas
            // 
            this.txtNotas.Location = new System.Drawing.Point(95, 448);
            this.txtNotas.Name = "txtNotas";
            this.txtNotas.Size = new System.Drawing.Size(455, 67);
            this.txtNotas.TabIndex = 108;
            this.txtNotas.Text = "NO ESTÁ IMPLEMENTADO... \n¿POR CLIENTE O POR RESERVACIÓN?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 471);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 107;
            this.label2.Text = "Notas:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(734, 495);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 16);
            this.label4.TabIndex = 105;
            this.label4.Text = "Bs.";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.checkCamion);
            this.groupBox1.Controls.Add(this.txtPlaca);
            this.groupBox1.Controls.Add(this.lblMarca);
            this.groupBox1.Controls.Add(this.txtModelo);
            this.groupBox1.Controls.Add(this.txtMarca);
            this.groupBox1.Controls.Add(this.lblModelo);
            this.groupBox1.Controls.Add(this.lblPlaca);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(29, 264);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 157);
            this.groupBox1.TabIndex = 104;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información sobre el vehículo";
            // 
            // checkCamion
            // 
            this.checkCamion.AutoSize = true;
            this.checkCamion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkCamion.Location = new System.Drawing.Point(424, 21);
            this.checkCamion.Name = "checkCamion";
            this.checkCamion.Size = new System.Drawing.Size(82, 24);
            this.checkCamion.TabIndex = 49;
            this.checkCamion.Text = "Camión";
            this.checkCamion.UseVisualStyleBackColor = true;
            this.checkCamion.CheckedChanged += new System.EventHandler(this.checkCamion_CheckedChanged);
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(323, 61);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(124, 24);
            this.txtPlaca.TabIndex = 62;
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.Location = new System.Drawing.Point(32, 64);
            this.lblMarca.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(57, 20);
            this.lblMarca.TabIndex = 55;
            this.lblMarca.Text = "Marca:";
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(96, 99);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(351, 24);
            this.txtModelo.TabIndex = 58;
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(96, 62);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(156, 24);
            this.txtMarca.TabIndex = 59;
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModelo.Location = new System.Drawing.Point(24, 99);
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
            this.lblPlaca.Location = new System.Drawing.Point(265, 62);
            this.lblPlaca.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(52, 20);
            this.lblPlaca.TabIndex = 57;
            this.lblPlaca.Text = "Placa:";
            // 
            // txtEdad
            // 
            this.txtEdad.Location = new System.Drawing.Point(460, 176);
            this.txtEdad.Name = "txtEdad";
            this.txtEdad.Size = new System.Drawing.Size(77, 26);
            this.txtEdad.TabIndex = 103;
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.Location = new System.Drawing.Point(402, 179);
            this.lblEdad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(51, 20);
            this.lblEdad.TabIndex = 102;
            this.lblEdad.Text = "Edad:";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(768, 489);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(120, 26);
            this.txtTotal.TabIndex = 101;
            this.txtTotal.Text = "0";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(608, 492);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(106, 20);
            this.lblTotal.TabIndex = 100;
            this.lblTotal.Text = "Total a pagar:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(353, 211);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 20);
            this.label1.TabIndex = 99;
            this.label1.Text = "/";
            // 
            // txtTelefono2
            // 
            this.txtTelefono2.Location = new System.Drawing.Point(375, 208);
            this.txtTelefono2.Name = "txtTelefono2";
            this.txtTelefono2.Size = new System.Drawing.Size(162, 26);
            this.txtTelefono2.TabIndex = 98;
            // 
            // txtTelefono1
            // 
            this.txtTelefono1.Location = new System.Drawing.Point(184, 208);
            this.txtTelefono1.Name = "txtTelefono1";
            this.txtTelefono1.Size = new System.Drawing.Size(162, 26);
            this.txtTelefono1.TabIndex = 96;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(184, 144);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(425, 26);
            this.txtNombre.TabIndex = 95;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(70, 211);
            this.lblTelefono.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(93, 20);
            this.lblTelefono.TabIndex = 94;
            this.lblTelefono.Text = "Teléfono(s):";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(25, 147);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(138, 20);
            this.lblNombre.TabIndex = 92;
            this.lblNombre.Text = "Nombre completo:";
            // 
            // lblHabitacionActual
            // 
            this.lblHabitacionActual.AutoSize = true;
            this.lblHabitacionActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHabitacionActual.Location = new System.Drawing.Point(10, 65);
            this.lblHabitacionActual.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblHabitacionActual.Name = "lblHabitacionActual";
            this.lblHabitacionActual.Size = new System.Drawing.Size(123, 15);
            this.lblHabitacionActual.TabIndex = 19;
            this.lblHabitacionActual.Text = "Habitación actual:";
            this.lblHabitacionActual.Visible = false;
            // 
            // lblHabitacionNumero
            // 
            this.lblHabitacionNumero.AutoSize = true;
            this.lblHabitacionNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHabitacionNumero.Location = new System.Drawing.Point(154, 65);
            this.lblHabitacionNumero.Name = "lblHabitacionNumero";
            this.lblHabitacionNumero.Size = new System.Drawing.Size(74, 15);
            this.lblHabitacionNumero.TabIndex = 20;
            this.lblHabitacionNumero.Text = "habitacion";
            this.lblHabitacionNumero.Visible = false;
            // 
            // Reservacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 593);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Reservacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nueva Reservación";
            this.panel1.ResumeLayout(false);
            this.panelContenedor.ResumeLayout(false);
            this.panelContenedor.PerformLayout();
            this.panelCedula.ResumeLayout(false);
            this.panelCedula.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listboxHabitaciones;
        private System.Windows.Forms.RichTextBox txtNotas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkCamion;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.TextBox txtEdad;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTelefono2;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.TextBox txtTelefono1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblCedula;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Button btnCheckCedula;
        private System.Windows.Forms.Panel panelCedula;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtEntrada;
        private System.Windows.Forms.DateTimePicker dtSalida;
        private System.Windows.Forms.Label lblHab;
        private System.Windows.Forms.ComboBox comboHabitacion;
        private System.Windows.Forms.Label lblFechaIngrso;
        private System.Windows.Forms.Label lblFechaSalida;
        private System.Windows.Forms.LinkLabel linkLblCambiarNumHab;
        private System.Windows.Forms.Label lblHabitacionNumero;
        private System.Windows.Forms.Label lblHabitacionActual;
    }
}