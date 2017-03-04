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

            CargarListViewClientes();
        }

        Msg msg;
        ListView lst;

        Font font_verdana = new Font("Verdana", 12);

        bool nuevoCliente = true; // Controlar si estoy agregando un nuevo cliente, o modificando uno existente
        string idCliente;

        private void CrearListView()
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

            lst.Columns.Add("ID", 0, HorizontalAlignment.Left);
            lst.Columns.Add("Nombre completo", 400, HorizontalAlignment.Left);
            lst.Columns.Add("Cédula", 150, HorizontalAlignment.Left);
            lst.Columns.Add("Reservaciones activas", 220, HorizontalAlignment.Left);
            lst.Columns.Add("Cliente desde", 150, HorizontalAlignment.Left);

            panel1.Controls.Add(lst);

            lst.BringToFront();

            lst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lst_MouseDoubleClick);
            this.lst.SelectedIndexChanged += new System.EventHandler(this.lst_SelectedIndexChanged);
        }

        private void CargarListViewClientes()
        {
            panel2.Visible = false;

            btnNuevoCliente.Enabled = true;
            ActivarBotones(false);

            if (!panel1.Contains(lst))
            {
                CrearListView();
            }

            lst.Items.Clear();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT *, COUNT(r.id) AS reservaciones FROM cliente LEFT JOIN reservacion r ON cedula=cedula_cliente GROUP BY nombre ORDER BY id", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                ListViewItem item = new ListViewItem(dr["id"].ToString());
                                item.SubItems.Add(dr["apellido"].ToString() + ", " + dr["nombre"].ToString());

                                item.SubItems.Add(dr["cedula"].ToString());

                                item.SubItems.Add(dr["reservaciones"].ToString());

                                var dt = DateTime.Parse(dr["cliente_desde"].ToString());
                                item.SubItems.Add(dt.ToString("dd/MMM/yyyy"));

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
                                lblVehiculos.Text = dr["vehiculos"].ToString();

                                if (int.Parse(dr["reservaciones"].ToString()) > 0)
                                {
                                    btnVerReservaciones.Visible = true;
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
                        CargarListViewClientes();
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
                        CargarListViewClientes();

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

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            nuevoCliente = true;

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
                    }
                }
                //

                panel2.Visible = true;

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
                msg = new Msg();

                msg.lblMsg.Text = $"¿Está seguro de que desea eliminar el registro? \nAdvertencia: Se eliminarán todos los datos asociados (reservaciones y vehículos). \n\nCliente a eliminar: \"{lst.SelectedItems[0].SubItems[1].Text}\" - Cédula: \"{lst.SelectedItems[0].SubItems[2].Text}\".";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes)
                    {
                        EliminarCliente(lst.SelectedItems[0].SubItems[0].Text.ToString());
                        CargarListViewClientes();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void btnMostrarClientes_Click(object sender, EventArgs e)
        {
            if (panel2.Visible)
            {
                CargarListViewClientes();
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
            CargarListViewClientes();
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
            btnModificar_Click(null, null);
        }
    }
}
