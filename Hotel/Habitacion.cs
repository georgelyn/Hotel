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
    public partial class Habitacion : Form
    {
        public Habitacion()
        {
            InitializeComponent();

            CargarListViewHabitaciones();

            comboEstado.Items.Add("Activa");
            comboEstado.Items.Add("Inactiva");
            comboEstado.SelectedIndex = 0;
        }

        Msg msg;
        ListView lst;

        Font font_verdana = new Font("Verdana", 12);

        bool habitacionExiste = false; // Para controlar si se está modificando un tipo de habitación existente, o agregando uno nuevo
        string idHabitacion;

        public void CrearListView()
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
            lst.Columns.Add("Tipo", 200, HorizontalAlignment.Left);
            lst.Columns.Add("Descripcion", 300, HorizontalAlignment.Left);
            lst.Columns.Add("Costo", 130, HorizontalAlignment.Left);
            lst.Columns.Add("Estado", 100, HorizontalAlignment.Left);
            lst.Columns.Add("Notas", 200, HorizontalAlignment.Left);

            panel1.Controls.Add(lst);

            lst.BringToFront();

            lst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lst_MouseDoubleClick);
            this.lst.SelectedIndexChanged += new System.EventHandler(this.lst_SelectedIndexChanged);

        }

        public void CargarListViewHabitaciones()
        {
            panel2.Visible = false;

            btnAgregar.Enabled = true;
            //btnModificar.Enabled = true;
            //btnEliminar.Enabled = true;
            //btnActDesactivar.Enabled = true;

            if (!panel1.Contains(lst))
            {
                CrearListView();
            }

            lst.Items.Clear();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT id, tipo, descripcion, costo, notas, activa FROM tipo_habitacion", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                double costo = double.Parse(dr["costo"].ToString());

                                ListViewItem item = new ListViewItem(dr["id"].ToString());
                                item.SubItems.Add(dr["tipo"].ToString());
                                item.SubItems.Add(dr["descripcion"].ToString());
                                //item.SubItems.Add(string.Format("{0:#,##}", costo).Replace(",", "."));
                                item.SubItems.Add(dr["costo"].ToString());
                                if (Convert.ToBoolean(dr["activa"]) == true)
                                {
                                    item.SubItems.Add("Activa");
                                }
                                else
                                {
                                    item.SubItems[0].BackColor = Color.LightGray;
                                    item.SubItems.Add("Inactiva");
                                }
                                item.SubItems.Add(dr["notas"].ToString());
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

        public void CargarHabitacion(string id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM tipo_habitacion WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("id", id);
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtTipo.Text = dr["tipo"].ToString();
                                txtDescripcion.Text = dr["descripcion"].ToString();
                                txtCosto.Text = dr["costo"].ToString();
                                if (Convert.ToBoolean(dr["activa"]) == true)
                                    comboEstado.Text = "Activa";
                                else
                                    comboEstado.Text = "Inactiva";
                                txtNotas.Text = dr["notas"].ToString();
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

        public void AgregarHabitacion()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO tipo_habitacion (tipo, descripcion, costo, notas, activa) VALUES (@tipo, @descripcion, @costo, @notas, @estado)", conn))
                    {
                        cmd.Parameters.AddWithValue("tipo", txtTipo.Text.Trim());
                        cmd.Parameters.AddWithValue("descripcion", StringExtensions.NullString(txtDescripcion.Text.Trim()));
                        cmd.Parameters.AddWithValue("costo", txtCosto.Text.Trim().Replace(".", "").Replace(",", ""));
                        cmd.Parameters.AddWithValue("notas", StringExtensions.NullString(txtNotas.Text.Trim()));
                        if (comboEstado.Text == "Activa")
                        {
                            cmd.Parameters.AddWithValue("estado", "1");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("estado", "0");
                        }

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("La habitación se ha agregado exitosamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ModificarHabitacion(string id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE tipo_habitacion SET tipo=@tipo, descripcion=@descripcion, costo=@costo, notas=@notas, activa=@estado WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("tipo", txtTipo.Text.Trim());
                        cmd.Parameters.AddWithValue("descripcion", StringExtensions.NullString(txtDescripcion.Text.Trim()));
                        cmd.Parameters.AddWithValue("costo", txtCosto.Text.Trim().Replace(".", "").Replace(",", ""));
                        cmd.Parameters.AddWithValue("notas", StringExtensions.NullString(txtNotas.Text.Trim()));
                        if (comboEstado.Text == "Activa")
                        {
                            cmd.Parameters.AddWithValue("estado", "1");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("estado", "0");
                        }

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Datos modificados exitosamente.");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EliminarHabitacion(string id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM tipo_habitacion WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("id", id);

                        conn.Open();
                        cmd.ExecuteReader();

                        //MessageBox.Show("Los datos de la habitación se han eliminado exitosamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void CambiarEstadoHabitacion(string id, bool estado)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE tipo_habitacion SET activa=@estado WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("estado", estado);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        //MessageBox.Show("Estado de la habitación fue cambiado satisfactoriamente.");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            habitacionExiste = false;

            if (!panel2.Visible)
            {
                panel1.Controls.Remove(lst);
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                btnActDesactivar.Enabled = false;

                // Restaurar/reinicializar valores
                foreach (Control c in panel2.Controls)
                {
                    if (c is TextBox || c is RichTextBox)
                    {
                        c.Text = "";
                    }
                }
                comboEstado.SelectedIndex = 0;
                //

                panel2.Visible = true;

                txtTipo.Focus();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            habitacionExiste = true;

            if (lst.SelectedItems.Count > 0)
            {
                if (!panel2.Visible)
                {
                    panel1.Controls.Remove(lst);
                    btnAgregar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnActDesactivar.Enabled = false;

                    panel2.Visible = true;
                    panel2.BringToFront();

                    idHabitacion = lst.SelectedItems[0].SubItems[0].Text.ToString();
                    CargarHabitacion(idHabitacion);
                    txtTipo.Focus();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lst.SelectedItems.Count > 0)
            {
                msg = new Msg();

                msg.lblMsg.Text = $"¿Está seguro de que desea eliminar el registro? \nTipo de habitación a eliminar: \"{lst.SelectedItems[0].SubItems[1].Text}\".";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes)
                    {
                        EliminarHabitacion(lst.SelectedItems[0].SubItems[0].Text.ToString());
                        CargarListViewHabitaciones();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void btnActDesactivar_Click(object sender, EventArgs e)
        {        
            if (lst.SelectedItems.Count > 0)
            {
                if (lst.SelectedItems[0].SubItems[4].Text == "Activa")
                    CambiarEstadoHabitacion(lst.SelectedItems[0].SubItems[0].Text.ToString(), false);
                else
                    CambiarEstadoHabitacion(lst.SelectedItems[0].SubItems[0].Text.ToString(), true);

                CargarListViewHabitaciones();
            }
        }

        private void btnMostrarHabitaciones_Click(object sender, EventArgs e)
        {
            if (panel2.Visible)
            {
                CargarListViewHabitaciones();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTipo.Text.Trim()))
            {
                MessageBox.Show("Debe agregar el tipo de habitación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTipo.Select();
            }
            else if (String.IsNullOrEmpty(txtCosto.Text))
            {
                MessageBox.Show("Debe agregar el costo de la habitación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCosto.Select();
            }
            else
            {
                if (!habitacionExiste)
                {
                    AgregarHabitacion();
                }
                else
                {
                    ModificarHabitacion(idHabitacion);
                }

                CargarListViewHabitaciones();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CargarListViewHabitaciones();

            if (btnModificar.Enabled)
            {
                btnModificar.Enabled = false;
            }
        }

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst.SelectedItems.Count > 0)
            {
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                btnActDesactivar.Enabled = true;
            }
            else
            {
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                btnActDesactivar.Enabled = false;
            }
        }

        private void lst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnModificar_Click(null, null);

            //MessageBox.Show(Ci);
            //MessageBox.Show(lst.SelectedItems[0].SubItems[0].Text);
        }
    }
}
