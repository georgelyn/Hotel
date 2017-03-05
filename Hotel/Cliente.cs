using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Hotel
{
    public partial class Cliente : Form
    {
        public Cliente()
        {
            InitializeComponent();

            CargarListView("cliente");
        }

        Msg msg;
        ListView lst;
        Vehiculo vehiculo;
        Reservacion reservacion;

        Font font_verdana = new Font("Verdana", 12);

        bool nuevoCliente = true; // Controlar si estoy agregando un nuevo cliente, o modificando uno existente
        string idCliente;
        string opcion = ""; // Para que al dar click en Cancelar, cargue el último ListView creado según la opción dada. Toma valor en CargarListview

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

            if (opcion == "cliente" || opcion == "actual" || opcion == "buscar")
            {
                lst.Columns.Add("ID", 0, HorizontalAlignment.Left);
                lst.Columns.Add("Nombre completo", 400, HorizontalAlignment.Left);
                lst.Columns.Add("Cédula", 150, HorizontalAlignment.Left);
                lst.Columns.Add("Reservaciones activas", 220, HorizontalAlignment.Left);
                lst.Columns.Add("Cliente desde", 150, HorizontalAlignment.Left);
            }

            else if (opcion == "habitacion")
            {
                lst.Columns.Add("IDCliente", 0, HorizontalAlignment.Left);
                lst.Columns.Add("IDHab", 0, HorizontalAlignment.Left);
                lst.Columns.Add("Nro. Habitación", 150, HorizontalAlignment.Left);
                lst.Columns.Add("Cliente", 400, HorizontalAlignment.Left);
                lst.Columns.Add("Cédula", 150, HorizontalAlignment.Left);
            }

            panel1.Controls.Add(lst);

            lst.BringToFront();

            lst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lst_MouseDoubleClick);
            this.lst.SelectedIndexChanged += new System.EventHandler(this.lst_SelectedIndexChanged);
        }

        public void CargarListView(string opcion)
        {
            string query = "";

            this.opcion = opcion;

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

            if (panel1.Contains(lst))
            {
                panel1.Controls.Remove(lst);
            }

            CrearListView(opcion);

            if (opcion == "cliente")
            {
                query = "SELECT *, COUNT(r.id) AS reservaciones FROM cliente LEFT JOIN reservacion r ON cedula = cedula_cliente GROUP BY nombre ORDER BY id";
                toolStripComboBox1.SelectedIndex = 0;
            }
            else if (opcion == "actual")
            {
                query = "SELECT *, COUNT(r.id) AS reservaciones FROM cliente INNER JOIN reservacion r ON cedula = cedula_cliente GROUP BY nombre ORDER BY id";
                toolStripComboBox1.SelectedIndex = 1;
            }
            else if (opcion == "buscar")
            {
                query = "SELECT *, COUNT(r.id) AS reservaciones FROM cliente LEFT JOIN reservacion r ON cedula = cedula_cliente WHERE nombre LIKE '%" + txtBuscar.Text.Trim() + "%' OR apellido LIKE '%" + txtBuscar.Text.Trim() + "%' OR cedula LIKE '%" + txtBuscar.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "") + "%' GROUP BY nombre";
                //nombre like '%" + txtBuscar.Text.Trim()  + "% ' or apellido like '%" + txtBuscar.Text.Trim()  + "% ' or cedula like '%" + txtBuscar.Text.Trim() + "% ' GROUP BY nombre ORDER BY id";
            }
            else if (opcion == "habitacion")
            {
                query = "SELECT reservacion.id as reservacionID, cliente.id as id, numero_hab, nombre, apellido, cedula FROM cliente INNER JOIN reservacion ON cedula=cedula_cliente ORDER BY numero_hab";
                toolStripComboBox1.SelectedIndex = 2;
            }

            lst.Items.Clear();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        //if (opcion == "buscar")
                        //{
                        //    cmd.Parameters.AddWithValue("@criterio", txtBuscar.Text.Trim());
                        //}
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                ListViewItem item = new ListViewItem(dr["id"].ToString());

                                if (opcion == "cliente" || opcion == "actual" || opcion == "buscar")
                                {
                                    item.SubItems.Add(dr["apellido"].ToString() + ", " + dr["nombre"].ToString());                                
                                    item.SubItems.Add(dr["cedula"].ToString());
                                    item.SubItems.Add(dr["reservaciones"].ToString());

                                    var dt = DateTime.Parse(dr["cliente_desde"].ToString());
                                    item.SubItems.Add(dt.ToString("dd/MMM/yyyy"));
                                }
                                else if (opcion == "habitacion")
                                {
                                    item.SubItems.Add(dr["ReservacionID"].ToString());
                                    item.SubItems.Add(dr["numero_hab"].ToString());
                                    item.SubItems.Add(dr["nombre"].ToString() + " " + dr["apellido"].ToString());
                                    item.SubItems.Add(dr["cedula"].ToString());
                                }

                                lst.Items.Add(item);
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

        private void CargarCliente(string id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT *, COUNT(distinct v.id) AS vehiculos, COUNT(distinct r.id) AS reservaciones FROM cliente LEFT JOIN vehiculo v ON cedula=v.cedula_cliente LEFT JOIN reservacion r ON cedula=r.cedula_cliente WHERE cliente.id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                txtNombre.Text = dr["nombre"].ToString();
                                txtApellido.Text = dr["apellido"].ToString();
                                txtCedula.Text = dr["cedula"].ToString();
                                txtEdad.Text = dr["edad"].ToString();
                                txtTelefono1.Text = dr["telefono"].ToString();
                                txtTelefono2.Text = dr["telefono2"].ToString();
                                txtNotas.Text = dr["notas"].ToString();

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

        private void CargarReservacionCliente(string cedula)
        {
            listboxReservaciones.Items.Clear();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT numero_hab FROM reservacion WHERE cedula_cliente=@cedula ORDER BY numero_hab", conn))
                    {
                        cmd.Parameters.AddWithValue("@cedula", cedula);

                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listboxReservaciones.Items.Add(dr["numero_hab"].ToString());
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
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO cliente (nombre, apellido, cedula, edad, telefono, telefono2, cliente_desde, notas) VALUES (@nombre, @apellido, @cedula, @edad, @telefono1, @telefono2, @clienteDesde, @notas)", conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@apellido", txtApellido.Text.Trim());
                        cmd.Parameters.AddWithValue("@cedula", txtCedula.Text.Trim());
                        cmd.Parameters.AddWithValue("@edad", StringExtensions.NullString(txtEdad.Text.Trim()));
                        cmd.Parameters.AddWithValue("@telefono1", StringExtensions.NullString(txtTelefono1.Text.Trim()));
                        cmd.Parameters.AddWithValue("@telefono2", StringExtensions.NullString(txtTelefono2.Text.Trim()));
                        cmd.Parameters.AddWithValue("@clienteDesde", clienteDesde);
                        cmd.Parameters.AddWithValue("@notas", StringExtensions.NullString(txtNotas.Text.Trim()));

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cliente almacenado satisfactoriamente.");
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

        private void ModificarCliente(string id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE cliente SET nombre=@nombre, apellido=@apellido, cedula=@cedula, edad=@edad, telefono=@telefono1, telefono2=@telefono2, notas=@notas WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@apellido", txtApellido.Text.Trim());
                        cmd.Parameters.AddWithValue("@cedula", txtCedula.Text.Trim());
                        cmd.Parameters.AddWithValue("@edad", StringExtensions.NullString(txtEdad.Text.Trim()));
                        cmd.Parameters.AddWithValue("@telefono1", StringExtensions.NullString(txtTelefono1.Text.Trim()));
                        cmd.Parameters.AddWithValue("@telefono2", StringExtensions.NullString(txtTelefono2.Text.Trim()));
                        cmd.Parameters.AddWithValue("@notas", StringExtensions.NullString(txtNotas.Text.Trim()));

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
                    using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM cliente WHERE id=@id", conn))
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

        public bool ValidacionCamposTexto()
        {
            TextBox[] txtBox = { txtNombre, txtApellido, txtCedula };
            Label[] txtLabel = { lblNombre, lblApellido, lblCedula };

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
                }
                else
                {
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnEliminar.ForeColor = Color.White;
                    btnEliminar.BackColor = Color.Red;
                }
            }
            else
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
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;

            listboxReservaciones.Visible = false;

        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            nuevoCliente = true;
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

            if (lst.SelectedItems.Count > 0)
            {
                idCliente = lst.SelectedItems[0].SubItems[0].Text.ToString();

                if (!panel2.Visible)
                {
                    CargarCliente(idCliente);

                    panel1.Controls.Remove(lst);
                    btnNuevoCliente.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = true;

                    btnBuscar.Visible = false;
                    txtBuscar.Visible = false;
                    lblBuscar.Visible = false;

                    panel2.Visible = true;
                    //panel2.BringToFront();
                    txtNombre.Focus();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lst.SelectedItems.Count > 0)
            {
                if (opcion == "cliente" || opcion == "actual")
                {
                    msg = new Msg();

                    msg.lblMsg.Text = $"¿Está seguro de que desea eliminar el registro? \nAdvertencia: Se eliminarán todos los datos asociados (reservaciones y vehículos). \n\nCliente a eliminar: \"{lst.SelectedItems[0].SubItems[1].Text}\" - Cédula: \"{lst.SelectedItems[0].SubItems[2].Text}\".";
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
                    ModificarCliente(idCliente);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CargarListView(opcion);
            OcultarListbox();
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst.SelectedItems.Count > 0)
            {
                ActivarBotones(true);
            }
            else
            {
                ActivarBotones(false);
            }
        }

        private void lst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (opcion == "cliente" || opcion == "actual" || opcion == "buscar")
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

        private void btnVerVehiculos_Click(object sender, EventArgs e)
        {
            vehiculo = new Vehiculo();
            //vehiculo.CargarVehiculosPorCedula(txtCedula.Text);
            vehiculo.comboCedula.Text = txtCedula.Text;
            vehiculo.ShowDialog();
        }

        private void btnVerReservaciones_Click(object sender, EventArgs e)
        {
            if (label8.Visible)
            {
                OcultarListbox();
            }
            else
            {
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                listboxReservaciones.Visible = true;
                CargarReservacionCliente(txtCedula.Text.Trim());
            }
        }

        private void listboxReservaciones_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            reservacion = new Reservacion();

            reservacion.CargarReservacion(int.Parse(listboxReservaciones.Text), "ocupada");
            reservacion.ShowDialog();
            //this.Hide();

            //MessageBox.Show("De momento da error el método de Actualizar Colores porque Form1 no está activa. Verificar si sigue siendo el caso una vez que cambie Application.Run a Form1 nuevamente.");
        }

        private void btnNuevaReservacion_Click(object sender, EventArgs e)
        {
            reservacion = new Reservacion();

            reservacion.txtCedula.Text = txtCedula.Text.Trim();
            reservacion.ShowDialog();
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
            else // Habitación
            {
                CargarListView("habitacion");
            }

            OcultarListbox();
            ActivarBotones(false);

            //if (toolStripComboBox1.SelectedIndex == 0)
            //{
            //    if (panel2.Visible || panel1.Contains(lstPorHabitacion))
            //    {
            //        CargarListViewClientes();
            //        OcultarListbox();
            //    }
            //}
            //else
            //{
            //    if (panel2.Visible || panel1.Contains(lst))
            //    {
            //        CargarListViewPorHabitacion();
            //        OcultarListbox();
            //    }
            //}
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarListView("buscar");
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnBuscar_Click(null, null);
            }
        }
    }
}
