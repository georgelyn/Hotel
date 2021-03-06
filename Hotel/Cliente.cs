﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace Hotel
{
    public partial class Cliente : Form
    {
        //System.Globalization.CultureInfo culturaOriginal = System.Threading.Thread.CurrentThread.CurrentCulture;


        public Cliente()
        {
            InitializeComponent();
            //CargarListView("cliente");
        }

        Msg msg;
        ListView lst;
        VerVehiculo vehiculo;
        Reservacion reservacion;
        Form1 f1 = (Form1)Application.OpenForms["Form1"];


        Font font_verdana = new Font("Verdana", 12);

        private ListViewColumnSorter lvwColumnSorter;

        public bool nuevoCliente = true; // Controlar si estoy agregando un nuevo cliente, o modificando uno existente
        string idCliente;
        string opcion = ""; // Para que al dar click en Cancelar, cargue el último ListView creado según la opción dada. Toma valor en CargarListview

        public string motivo = ""; // Si está en lista negra.
        string nombre = "";
        string cedula = "";

        private void CrearListView(string opcion)
        {
            lst = new ListView();

            lst.View = View.Details;
            lst.FullRowSelect = true;
            lst.GridLines = true;
            lst.MultiSelect = false;
            lst.ShowItemToolTips = true;
            //lst.Alignment = ListViewAlignment.SnapToGrid;
            lst.Dock = DockStyle.Fill;
            lst.Font = font_verdana;

            if (opcion == "cliente" || opcion == "actual" || opcion == "buscar" || opcion == "lista_negra")
            {
                lst.Columns.Add("ID", 0, HorizontalAlignment.Left);

                lst.Columns.Add("Nro.", 50, HorizontalAlignment.Center);
                lst.Columns.Add("Nombre completo", 350, HorizontalAlignment.Left);
                lst.Columns.Add("Cédula", 150, HorizontalAlignment.Left);
                if (opcion != "lista_negra")
                {
                    lst.Columns.Add("Reserv. Activas", 150, HorizontalAlignment.Center);
                }
                lst.Columns.Add("Cliente desde", 126, HorizontalAlignment.Left);
            }

            else if (opcion == "habitacion")
            {
                lst.Columns.Add("IDCliente", 0, HorizontalAlignment.Left);
                lst.Columns.Add("IDHab", 0, HorizontalAlignment.Left);
                lst.Columns.Add("Habitación", 100, HorizontalAlignment.Center);
                lst.Columns.Add("Cliente", 350, HorizontalAlignment.Left);
                lst.Columns.Add("Cédula", 150, HorizontalAlignment.Left);
                lst.Columns.Add("Fecha de entrada", 180, HorizontalAlignment.Center);
            }

            panel1.Controls.Add(lst);

            lst.BringToFront();

            lst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lst_MouseDoubleClick);
            lst.SelectedIndexChanged += new System.EventHandler(this.lst_SelectedIndexChanged);

            lst.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lst_ColumnClick);

            lvwColumnSorter = new ListViewColumnSorter();
            this.lst.ListViewItemSorter = lvwColumnSorter;
        }

        public void CargarListView(string opcion)
        {
            string query = "";

            this.opcion = opcion;

            if (panel1.Contains(lst))
            {
                panel1.Controls.Remove(lst);
            }

            CrearListView(opcion);

            if (opcion == "cliente")
            {
                query = "SELECT *, COUNT(r.ID) AS reservaciones FROM Clientes LEFT JOIN Reservaciones r ON Cedula = Cliente_Cedula GROUP BY Cedula ORDER BY Nombre";
                toolStripComboBox1.SelectedIndex = 0;
            }
            else if (opcion == "actual")
            {
                query = "SELECT *, COUNT(r.ID) AS reservaciones FROM Clientes INNER JOIN Reservaciones r ON Cedula = Cliente_Cedula GROUP BY Cedula ORDER BY Nombre";
                toolStripComboBox1.SelectedIndex = 1;
            }
            else if (opcion == "buscar")
            {
                query = "SELECT *, COUNT(r.ID) AS reservaciones FROM Clientes LEFT JOIN Reservaciones r ON Cedula = Cliente_Cedula WHERE Nombre LIKE '%" + txtBuscar.Text.Trim() + "%' OR Cedula LIKE '%" + txtBuscar.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "") + "%' GROUP BY Cedula";
                //nombre like '%" + txtBuscar.Text.Trim()  + "% ' or apellido like '%" + txtBuscar.Text.Trim()  + "% ' or cedula like '%" + txtBuscar.Text.Trim() + "% ' GROUP BY nombre ORDER BY id";
            }
            else if (opcion == "habitacion")
            {
                query = "SELECT Reservaciones.ID as reservacionID, Clientes.ID as id, NumeroHabitacion, Nombre, Cedula, FechaIngreso FROM Clientes INNER JOIN Reservaciones ON Cedula = Cliente_Cedula ORDER BY NumeroHabitacion";
                toolStripComboBox1.SelectedIndex = 2;
            }
            else if (opcion == "lista_negra")
            {
                query = "SELECT * FROM Clientes WHERE Baneado_ID IS NOT NULL";
                toolStripComboBox1.SelectedIndex = 3;
            }

            try
            {
                int nroClientes = 1;

                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        //if (opcion == "buscar")
                        //{
                        //    cmd.Parameters.AddWithValue("@criterio", txtBuscar.Text.Trim());
                        //}
                        conn.Open();

                        lst.Items.Clear();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                ListViewItem item = new ListViewItem(dr["id"].ToString());

                                if (opcion == "cliente" || opcion == "actual" || opcion == "buscar" || opcion == "lista_negra")
                                {
                                    if (dr["Baneado_ID"] != DBNull.Value)
                                    {
                                        item.SubItems[0].BackColor = Color.Red;
                                        item.SubItems[0].ForeColor = Color.White;
                                    }

                                    item.SubItems.Add(nroClientes.ToString());
                                    item.SubItems.Add(dr["Nombre"].ToString());                                
                                    item.SubItems.Add(dr["Cedula"].ToString());
                                    if (opcion != "lista_negra")
                                    {
                                        item.SubItems.Add(dr["reservaciones"].ToString());
                                    }
                                    var dt = DateTime.Parse(dr["ClienteDesde"].ToString());
                                    item.SubItems.Add(dt.ToString("dd/MMM/yyyy"));
                                }
                                else if (opcion == "habitacion")
                                {
                                    item.SubItems.Add(dr["ReservacionID"].ToString());
                                    item.SubItems.Add(dr["NumeroHabitacion"].ToString());
                                    item.SubItems.Add(dr["Nombre"].ToString());
                                    item.SubItems.Add(dr["Cedula"].ToString());
                                    var dt = DateTime.Parse(dr["FechaIngreso"].ToString());
                                    item.SubItems.Add(dt.ToString("dd/MMM/yyyy"));
                                }

                                lst.Items.Add(item);
                                nroClientes++;
                            }
                        }
                    }
                }

                // Antes estaban al principio, pero creo que 'refresca' mejor la pantalla si se colocan al final

                if (opcion != "habitacion")
                {
                    btnBuscar.Visible = true;
                    txtBuscar.Visible = true;
                    lblBuscar.Visible = true;
                    toolStripSeparator1.Visible = true;
                }
                else
                {
                    btnBuscar.Visible = false;
                    txtBuscar.Visible = false;
                    lblBuscar.Visible = false;
                    toolStripSeparator1.Visible = false;
                }

                panel2.Visible = false;

                btnNuevoCliente.Enabled = true;
                ActivarBotones(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarCliente(string id)
        {
            try
            {
                ActivarListaNegra(false);

                /*if (!btnNuevaReservacion.Enabled)
                {
                    ActivarListaNegra(false);
                    //btnNuevaReservacion.Enabled = true;
                }*/

                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT *, COUNT(distinct v.ID) AS vehiculos, COUNT(distinct r.ID) AS reservaciones FROM Clientes LEFT JOIN Vehiculos v ON Cedula=v.Cliente_Cedula LEFT JOIN Reservaciones r ON Cedula=r.Cliente_Cedula LEFT JOIN Baneados b ON Cedula=b.Cliente_Cedula WHERE Clientes.ID=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        idCliente = id; // Ahora se carga. Así cuando llame el método viniendo desde Lista Negra, está almacenado el ID

                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                txtNombre.Text = dr["Nombre"].ToString();

                                nombre = dr["Nombre"].ToString();

                                txtCedula.Text = dr["Cedula"].ToString();
                                cedula = dr["Cedula"].ToString();
                                txtEdad.Text = dr["Edad"].ToString();
                                txtTelefono1.Text = dr["Telefono"].ToString();
                                txtTelefono2.Text = dr["TelefonoExtra"].ToString();
                                txtNotas.Text = dr["Notas"].ToString();

                                //dtClienteDesde.Value = Convert.ToDateTime(dr["ClienteDesde"].ToString());
                                var dt = DateTime.Parse(dr["ClienteDesde"].ToString());
                                lblClienteDesde.Text = dt.ToString("dd/MMM/yyyy");
                                panelClienteDesde.Visible = true;

                                if (int.Parse(dr["vehiculos"].ToString()) > 0)
                                {
                                    btnVerVehiculos.Visible = true;
                                }
                                else
                                {
                                    btnVerVehiculos.Visible = false;
                                }
                                lblVehiculos.Text = dr["vehiculos"].ToString();

                                if (int.Parse(dr["reservaciones"].ToString()) > 0)
                                {
                                    btnVerReservaciones.Visible = true;
                                }
                                else
                                {
                                    btnVerReservaciones.Visible = false;
                                }
                                lblReservaciones.Text = dr["reservaciones"].ToString();

                                if (dr["Baneado_ID"] != DBNull.Value)
                                {
                                    ActivarListaNegra(true);

                                    //nombre = dr["Nombre"].ToString();
                                    motivo = dr["Motivo"].ToString();

                                    var dtLN = DateTime.Parse(dr["Fecha"].ToString());
                                    lblFechaListaNegra.Text = dtLN.ToString("dd/MMM/yyyy");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActivarListaNegra(bool opcion)
        {
            panelListaNegra.Visible = opcion;
            btnAgregarListaNegra.Visible = !opcion;
            /*lblListaNegra.Visible = opcion;
            lblAgregadoLista.Visible = opcion;
            lblFechaListaNegra.Visible = opcion;
            lblDetalles.Visible = opcion;*/
            btnNuevaReservacion.Enabled = !opcion;
        }

        private void CargarReservacionCliente(string cedula)
        {
            listboxReservaciones.Items.Clear();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT NumeroHabitacion FROM Reservaciones WHERE Cliente_Cedula=@cedula ORDER BY NumeroHabitacion", conn))
                    {
                        cmd.Parameters.AddWithValue("@cedula", cedula);

                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listboxReservaciones.Items.Add(dr["NumeroHabitacion"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarCliente()
        {
            DateTime clienteDesde = DateTime.Now;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Clientes (Nombre, Cedula, Edad, Telefono, TelefonoExtra, ClienteDesde, Notas) VALUES (@nombre, @cedula, @edad, @telefono1, @telefono2, @clienteDesde, @notas)", conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", StringExtensions.ToTitleCase(txtNombre.Text.Trim()));
                        cmd.Parameters.AddWithValue("@cedula", txtCedula.Text.Trim());
                        cmd.Parameters.AddWithValue("@edad", StringExtensions.NullString(txtEdad.Text.Trim()));
                        cmd.Parameters.AddWithValue("@telefono1", StringExtensions.NullString(txtTelefono1.Text.Trim()));
                        cmd.Parameters.AddWithValue("@telefono2", StringExtensions.NullString(txtTelefono2.Text.Trim()));
                        cmd.Parameters.AddWithValue("@clienteDesde", clienteDesde);
                        cmd.Parameters.AddWithValue("@notas", StringExtensions.NullString(StringExtensions.FirstLetterToUpper(txtNotas.Text.Trim())));

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cliente almacenado satisfactoriamente.");
                        CargarListView("cliente");
                    }
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("Clientes.Cedula"))
                {
                    MessageBox.Show("El número de cédula ingresado ya existe en el sistema. \nPor favor, verifique.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCedula.Select();
                }
                else
                    MessageBox.Show("No se pudo conectar con la base de datos. \nDescripción del error: \n\n>> " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificarCliente(string id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Clientes SET Nombre=@nombre, Cedula=@cedula, Edad=@edad, Telefono=@telefono1, TelefonoExtra=@telefono2, Notas=@notas WHERE ID=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.Parameters.AddWithValue("@nombre", StringExtensions.ToTitleCase(txtNombre.Text.Trim()));
                        cmd.Parameters.AddWithValue("@cedula", txtCedula.Text.Trim());
                        cmd.Parameters.AddWithValue("@edad", StringExtensions.NullString(txtEdad.Text.Trim()));
                        cmd.Parameters.AddWithValue("@telefono1", StringExtensions.NullString(txtTelefono1.Text.Trim()));
                        cmd.Parameters.AddWithValue("@telefono2", StringExtensions.NullString(txtTelefono2.Text.Trim()));
                        cmd.Parameters.AddWithValue("@notas", StringExtensions.NullString(StringExtensions.FirstLetterToUpper(txtNotas.Text.Trim())));

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Los datos del cliente fueron modificados satisfactoriamente.");
                        CargarListView("cliente");

                    }

                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("cedula"))
                {
                    MessageBox.Show("El número de cédula ingresado ya existe en el sistema. \nPor favor, verifique.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCedula.Select();
                }
                else
                    MessageBox.Show("No se pudo conectar con la base de datos. \nDescripción del error: \n\n>> " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarCliente(string id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Clientes WHERE ID=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Los datos han sido eliminados.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool EnListaNegra(string cedula)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT Cedula FROM Clientes WHERE Cedula=@cedula AND Baneado_ID IS NOT NULL", conn))
                    {
                        cmd.Parameters.AddWithValue("@cedula", cedula);
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\n\n>> " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        public bool ValidacionCamposTexto()
        {
            TextBox[] txtBox = { txtNombre, txtCedula };
            Label[] txtLabel = { lblNombre, lblCedula };

            for (int i = 0; i < txtBox.Length; i++)
            {
                string text = txtBox[i].Text;
                txtLabel[i].ForeColor = Color.Black;

                if (String.IsNullOrEmpty(text.Trim()))
                {
                    MessageBox.Show(this, $"El campo de texto \"{txtLabel[i].Text.Replace(":", "")}\" no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBox[i].Clear();
                    txtBox[i].Select();
                    txtLabel[i].ForeColor = Color.Red;
                    return false; // No es válido
                }
            }
            return true; // Es valido, se puede proceder
        }

        public void ActivarBotones(bool verdadero)
        {
            if (opcion == "cliente" || opcion == "actual" || opcion == "buscar")
            {
                btnNuevoCliente.Visible = true;
                //btnModificar.Visible = true;

                //btnEliminar.Visible = true;
                btnEliminar.Text = "Eliminar";
                btnModificar.Text = "Modificar";

                if (!verdadero)
                {
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnEliminar.ForeColor = Color.Black;
                    btnEliminar.BackColor = Color.Transparent;

                    //label5.Visible = false; // Reservaciones
                    //btnVerReservaciones.Visible = false;
                    //lblReservaciones.Visible = false;
                    //label3.Visible = false; // Vehículos almacenados
                    //btnVerVehiculos.Visible = false;
                    //lblVehiculos.Visible = false;
                }
                else
                {
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnEliminar.ForeColor = Color.White;
                    btnEliminar.BackColor = Color.Red;

                    //label5.Visible = true; // Reservaciones
                    //btnVerReservaciones.Visible = true;
                    //lblReservaciones.Visible = true;
                    //label3.Visible = true; // Vehículos almacenados
                    //btnVerVehiculos.Visible = true;
                    //lblVehiculos.Visible = true;
                }
            }
            else // Habitaciones ocupadas
            {
                btnNuevoCliente.Visible = false;
                //btnModificar.Visible = false;
                //btnEliminar.Visible = false;

                btnEliminar.Text = "Ver reservación";
                btnEliminar.ForeColor = Color.Black;
                btnEliminar.BackColor = Color.Transparent;

                btnModificar.Text = "Ver cliente";

                if (!verdadero)
                {
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                }
                else
                {
                    btnEliminar.Enabled = true;
                    btnModificar.Enabled = true;
                }
            }
        }

        private void OcultarListbox()
        {
            panelListboxHabitaciones.Visible = false;
            //label8.Visible = false;
            //label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            btnVerReservacion.Visible = false;

            listboxReservaciones.Visible = false;

            panelClienteDesde.Visible = false;

            panelListaNegra.Visible = false;

        }

        public void ClienteNuevo(bool verdadero) // Si verdadero, se ocultan labels de Reservaciones, Vehículos, btnNuevaReservacion
        {
            if (verdadero)
            {
                label3.Visible = false; // Vehículos registrados
                btnVerVehiculos.Visible = false;
                label5.Visible = false; // Reservaciones activas
                btnVerReservaciones.Visible = false;
                lblReservaciones.Visible = false;
                lblVehiculos.Visible = false;
                btnNuevaReservacion.Visible = false;

                btnAgregarListaNegra.Visible = false;
            }
            else
            {
                label3.Visible = true; // Vehículos registrados
                label5.Visible = true; // Reservaciones activas
                lblReservaciones.Visible = true;
                lblVehiculos.Visible = true;
                btnNuevaReservacion.Visible = true;
            }
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            nuevoCliente = true;
            ClienteNuevo(true); // Ocultar labels y btn NuevaReservación

            //btnNuevaReservacion.Visible = false;

            if (!panel2.Visible)
            {
                panel1.Controls.Remove(lst);
                ActivarBotones(false);

                // Restaurar/reinicializar valores
                foreach (Control c in panel2.Controls)
                {
                    if (c is TextBox || c is RichTextBox)
                    {
                        c.Text = "";
                    }
                    if (c is Label)
                    {
                        if (c.Name == "lblReservaciones" || c.Name == "lblVehiculos")
                            c.Text = "0";
                        //if (c.Name == "label8" || c.Name == "label9" || c.Name == "label10")
                        //{
                        //    c.Visible = false;
                        //}
                    }
                }
                //

                btnBuscar.Visible = false;
                txtBuscar.Visible = false;
                lblBuscar.Visible = false;

                panel2.Visible = true;
                //btnVerReservaciones.Visible = false;

                txtNombre.Select();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            nuevoCliente = false;
            ClienteNuevo(false); // Mostrar labels Reservaciones, Vehículos y btnNuevaReservación

            if (lst.SelectedItems.Count > 0)
            {
                idCliente = lst.SelectedItems[0].SubItems[0].Text.ToString();

                if (!panel2.Visible)
                {
                    CargarCliente(idCliente);

                    PanelCargarCliente();
                }
            }
        }

        public void PanelCargarCliente()
        {
            panel1.Controls.Remove(lst);
            btnNuevoCliente.Enabled = false;
            btnModificar.Enabled = false;
            if (!EnListaNegra(cedula))
            {
                btnEliminar.Enabled = true;
            }
            else
            {
                btnEliminar.Enabled = false;
            }
            btnBuscar.Visible = false;
            txtBuscar.Visible = false;
            lblBuscar.Visible = false;

            panel2.Visible = true;
            //panel2.BringToFront();
            txtNombre.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lst.SelectedItems.Count > 0)
            {
                if (opcion == "cliente" || opcion == "actual" || opcion == "lista_negra")
                {
                    msg = new Msg();

                    msg.lblMsg.Text = $"¿Está seguro de que desea eliminar el registro? \nAdvertencia: Se eliminarán todos los datos asociados (reservaciones y vehículos). \n\nCliente a eliminar: \"{lst.SelectedItems[0].SubItems[2].Text}\" \nCédula: \"{lst.SelectedItems[0].SubItems[3].Text}\".";
                    DialogResult dlgres = msg.ShowDialog();
                    {
                        if (dlgres == DialogResult.Yes)
                        {
                            EliminarCliente(lst.SelectedItems[0].SubItems[0].Text.ToString());
                            CargarListView("cliente");
                            OcultarListbox();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else // Habitacion
                {
                    reservacion = new Reservacion();

                    reservacion.CargarReservacion(int.Parse(lst.SelectedItems[0].SubItems[2].Text.ToString()), "ocupada");
                    reservacion.ShowDialog();

                    //MessageBox.Show("De momento da error el método de Actualizar Colores porque Form1 no está activa. Verificar si sigue siendo el caso una vez que cambie Application.Run a Form1 nuevamente.");

                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidacionCamposTexto()) // Si la validación se realizó con éxito
            {
                if (nuevoCliente)
                    AgregarCliente();
                else
                {
                    msg = new Msg();

                    msg.lblMsg.Text = "¿Está seguro de que desea modificar el registro?";
                    DialogResult dlgres = msg.ShowDialog();
                    {
                        if (dlgres == DialogResult.Yes)
                        {
                            f1.ActivarTimerEspera();
                            ModificarCliente(idCliente);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (this.Text != "") // Cuando es llamado desde ListaNegra
            {
                CargarListView(opcion);
                OcultarListbox();
            }
            else
            {
                Close();
            }
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst.SelectedItems.Count > 0)
            {
                if (lst.SelectedItems[0].BackColor != Color.Red)
                {
                    ActivarBotones(true);
                }
                else
                {
                    btnModificar.Enabled = true;
                }
            }
            else
            {
                ActivarBotones(false);
            }
        }

        private void lst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            f1.ActivarTimerEspera();

            if (opcion == "cliente" || opcion == "actual" || opcion == "buscar" || opcion == "lista_negra")
            {
                btnModificar_Click(null, null);
            }
            else // Habitacion
            {
                reservacion = new Reservacion();

                reservacion.CargarReservacion(int.Parse(lst.SelectedItems[0].SubItems[2].Text.ToString()), "ocupada");
                //this.Hide();
                reservacion.ShowDialog();
                CargarListView("habitacion");
                //this.Show();

                //MessageBox.Show("De momento da error el método de Actualizar Colores porque Form1 no está activa. Verificar si sigue siendo el caso una vez que cambie Application.Run a Form1 nuevamente.");
            }
        }

        private void lst_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lst.Sort();
        }

        private void btnVerVehiculos_Click(object sender, EventArgs e)
        {

            vehiculo = new VerVehiculo();
            //vehiculo.CargarVehiculosPorCedula(txtCedula.Text);
            vehiculo.comboCedula.Text = txtCedula.Text;
            vehiculo.ShowDialog();
        }

        private void btnVerReservaciones_Click(object sender, EventArgs e)
        {
            if (label10.Visible)
            {
                OcultarListbox();
            }
            else
            {
                f1.ActivarTimerEspera();

                CargarReservacionCliente(txtCedula.Text.Trim());
                label10.Visible = true;
                label11.Visible = true;
                panelListboxHabitaciones.Visible = true;
                listboxReservaciones.Visible = true;
            }
        }

        private void listboxReservaciones_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            f1.ActivarTimerEspera();
        
            reservacion = new Reservacion();

            reservacion.CargarReservacion(int.Parse(listboxReservaciones.Text), "ocupada");
            reservacion.ShowDialog();
            //this.Hide();

            //MessageBox.Show("De momento da error el método de Actualizar Colores porque Form1 no está activa. Verificar si sigue siendo el caso una vez que cambie Application.Run a Form1 nuevamente.");
        }

        private void btnNuevaReservacion_Click(object sender, EventArgs e)
        {
            f1.ActivarTimerEspera();

            reservacion = new Reservacion();

            reservacion.txtCedula.Text = txtCedula.Text.Trim();
            reservacion.ShowDialog();
            CargarCliente(idCliente);
            //Hide();

            //MessageBox.Show("De momento da error el método de Actualizar Colores porque Form1 no está activa. Verificar si sigue siendo el caso una vez que cambie Application.Run a Form1 nuevamente.");

        }

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCedula.Focused)
            {
                if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13) // 8 = backspace, 13 = enter
                {
                    e.Handled = true;
                }
            }

            if (txtEdad.Focused || txtTelefono1.Focused || txtTelefono2.Focused)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13)
                {
                    e.Handled = true;
                }
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedIndex == 0) // Clientes
            {
                CargarListView("cliente");
            }
            else if (toolStripComboBox1.SelectedIndex == 1)
            {
                CargarListView("actual");
            }
            else if (toolStripComboBox1.SelectedIndex == 2) // Habitación
            {
                CargarListView("habitacion");
            }
            else // Lista negra
            {
                CargarListView("lista_negra");
            }

            OcultarListbox();
            ActivarBotones(false);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBuscar.Text.Trim()))
            {
                CargarListView("buscar");
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnBuscar_Click(null, null);
            }
        }

        private void btnVerReservacion_Click(object sender, EventArgs e)
        {
            if (listboxReservaciones.SelectedIndex >= 0)
            {
                listboxReservaciones_MouseDoubleClick(null, null);
            }
        }

        private void listboxReservaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!btnVerReservacion.Visible)
            {
                btnVerReservacion.Visible = true;
            }
        }

        private void lblDetalles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AgregarListaNegra agregarListaNegra;
            using (agregarListaNegra = new AgregarListaNegra())
            {
                agregarListaNegra.txtMotivo.Text = motivo;
                agregarListaNegra.Text = "";
                agregarListaNegra.btnAgregar.Text = "Modificar";

                agregarListaNegra.lblCliente.Text = nombre;
                agregarListaNegra.cedula = cedula;

                agregarListaNegra.ShowDialog();

                motivo = agregarListaNegra.txtMotivo.Text; // Actualizar motivo desde el textbox

                /*DialogResult dlgres = agregarListaNegra.ShowDialog();
                if (dlgres == DialogResult.Yes)
                {
                    if (!String.IsNullOrEmpty(agregarListaNegra.txtMotivo.Text))
                    {
                        ModificarMotivoListaNegra();
                    }
                }
                else
                {
                    return;
                }*/
            }
        }

        private void btnAgregarListaNegra_Click(object sender, EventArgs e)
        {
            msg = new Msg();

            msg.lblMsg.Text = "¿Está seguro de que desea agregar al cliente a la lista negra? \nNota: Cualquier reserva activa se eliminará.";
            DialogResult dlgres = msg.ShowDialog();
            {
                if (dlgres == DialogResult.Yes)
                {
                    using (AgregarListaNegra agregarListaNegra = new AgregarListaNegra())
                    {
                        agregarListaNegra.lblCliente.Text = nombre;
                        DialogResult agregar = agregarListaNegra.ShowDialog();

                        if (agregar == DialogResult.Yes)
                        {
                            using (ListaNegra lista = new ListaNegra())
                            {
                                lista.AgregarLista(cedula, agregarListaNegra.txtMotivo.Text.Trim());
                                using (Reservacion reserva = new Reservacion())
                                {
                                    if (reserva.TieneReserva(cedula))
                                    {
                                        reserva.EliminarReservacion(cedula);
                                    }
                                }
                                f1.ActivarTimerEspera();

                                // Desactivar botón de Eliminar cuando agrego a alguien a la lista
                                btnEliminar.Enabled = false;
                                btnEliminar.ForeColor = Color.Black;
                                btnEliminar.BackColor = Color.Transparent;

                                if (this.Text != "") // Como cuando se llama desde ListaNegra
                                {
                                    CargarCliente(idCliente);
                                }
                                else
                                {
                                    Close();
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }


        /*private void ModificarMotivoListaNegra()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Baneados SET Motivo=@motivo WHERE Cliente_Cedula=@cedula", conn))
                    {
                        cmd.Parameters.AddWithValue("@motivo", motivo);
                        cmd.Parameters.AddWithValue("@cedula", cedula);

                        conn.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se presentó un error.\n\n>> " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/
    }
}


