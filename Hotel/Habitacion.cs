using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Globalization;

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

        Form1 f1 = (Form1)Application.OpenForms["Form1"];

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
            lst.Columns.Add("Costo", 130, HorizontalAlignment.Center);
            lst.Columns.Add("Estado", 100, HorizontalAlignment.Center);
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
            ActivarBotones(false);
            //btnModificar.Enabled = false;
            //btnEliminar.Enabled = false;
            //btnActDesactivar.Enabled = false;

            if (!panel1.Contains(lst))
            {
                CrearListView();
            }

            lst.Items.Clear();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT ID, Tipo, Descripcion, Costo, Notas, Activa FROM TipoHabitacion ORDER BY Costo", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                //double costo = double.Parse(dr["costo"].ToString());

                                ListViewItem item = new ListViewItem(dr["ID"].ToString());
                                item.SubItems.Add(dr["Tipo"].ToString());
                                item.SubItems.Add(dr["Descripcion"].ToString());
                                //item.SubItems.Add(string.Format("{0:#,##}", costo).Replace(",", "."));
                                item.SubItems.Add("Bs. " + string.Format(new CultureInfo("es-VE"), "{0:#,##0.00}", dr["Costo"]));
                                if (Convert.ToBoolean(dr["Activa"]) == true)
                                {
                                    item.SubItems.Add("Activa");
                                }
                                else
                                {
                                    item.SubItems[0].BackColor = Color.LightGray;
                                    item.SubItems.Add("Inactiva");
                                }
                                item.SubItems.Add(dr["Notas"].ToString());
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
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM TipoHabitacion WHERE ID=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("id", id);
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtTipo.Text = dr["Tipo"].ToString();
                                txtDescripcion.Text = dr["Descripcion"].ToString();
                                txtCosto.Text = string.Format(new CultureInfo("es-VE"), "{0:#,##0.00}", dr["Costo"]);
                                if (Convert.ToBoolean(dr["Activa"]) == true)
                                    comboEstado.Text = "Activa";
                                else
                                    comboEstado.Text = "Inactiva";
                                txtNotas.Text = dr["Notas"].ToString();
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
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO TipoHabitacion (Tipo, Descripcion, Costo, Notas, Activa) VALUES (@tipo, @descripcion, @costo, @notas, @estado)", conn))
                    {
                        cmd.Parameters.AddWithValue("@tipo", txtTipo.Text.Trim());
                        cmd.Parameters.AddWithValue("@descripcion", StringExtensions.NullString(txtDescripcion.Text.Trim()));
                        cmd.Parameters.AddWithValue("@costo", txtCosto.Text.Trim().Replace(".", "").Replace(",", "."));
                        cmd.Parameters.AddWithValue("@notas", StringExtensions.NullString(txtNotas.Text.Trim()));
                        if (comboEstado.Text == "Activa")
                        {
                            cmd.Parameters.AddWithValue("@estado", "1");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@estado", "0");
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
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE TipoHabitacion SET Tipo=@tipo, Descripcion=@descripcion, Costo=@costo, Notas=@notas, Activa=@estado WHERE ID=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@tipo", txtTipo.Text.Trim());
                        cmd.Parameters.AddWithValue("@descripcion", StringExtensions.NullString(txtDescripcion.Text.Trim()));
                        cmd.Parameters.AddWithValue("@costo", txtCosto.Text.Trim().Replace(".", "").Replace(",", "."));
                        cmd.Parameters.AddWithValue("@notas", StringExtensions.NullString(txtNotas.Text.Trim()));
                        if (comboEstado.Text == "Activa")
                        {
                            cmd.Parameters.AddWithValue("@estado", "1");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@estado", "0");
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
                    using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM TipoHabitacion WHERE ID=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

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
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE TipoHabitacion SET Activa=@estado WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("ID", id);
                        cmd.Parameters.AddWithValue("Estado", estado);

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

        public void ActivarBotones(bool verdadero)
        {
            if (!verdadero)
            {
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                btnEliminar.ForeColor = Color.Black;
                btnEliminar.BackColor = Color.Transparent;
                btnActDesactivar.Enabled = false;
            }
            else
            {
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                btnEliminar.ForeColor = Color.White;
                btnEliminar.BackColor = Color.Red;
                btnActDesactivar.Enabled = true;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            habitacionExiste = false;

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
                    idHabitacion = lst.SelectedItems[0].SubItems[0].Text.ToString();
                    CargarHabitacion(idHabitacion);

                    panel1.Controls.Remove(lst);
                    btnAgregar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = true;
                    btnActDesactivar.Enabled = false;

                    panel2.Visible = true;
                    //panel2.BringToFront();
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
                //lst_SelectedIndexChanged(null, null);


            }
        }

        private void btnMostrarHabitaciones_Click(object sender, EventArgs e)
        {
            if (panel2.Visible)
            {
                f1.ActivarTimerEspera();

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
                f1.ActivarTimerEspera();

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
        }

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (String.IsNullOrEmpty(txtCosto.Text.Trim()))
            {
                if (e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == 8)
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (txtCosto.Text.Contains("."))
                {
                    if (e.KeyChar == '.')
                    {
                        e.Handled = true;
                    }
                }

                if (txtCosto.Text.Contains(","))
                {
                    if (e.KeyChar == ',')
                    {
                        e.Handled = true;
                    }
                }

                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != '.' && e.KeyChar != ',')
                {
                    e.Handled = true;
                }
            }
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst.SelectedItems.Count > 0)
            {
                ActivarBotones(true);
                //btnModificar.Enabled = true;
                //btnEliminar.Enabled = true;
                //btnEliminar.ForeColor = Color.White;
                //btnEliminar.BackColor = Color.Red;
                //btnActDesactivar.Enabled = true;
            }
            else
            {
                ActivarBotones(false);
                //btnModificar.Enabled = false;
                //btnEliminar.Enabled = false;
                //btnEliminar.ForeColor = Color.Black;
                //btnEliminar.BackColor = Color.Transparent;
                //btnActDesactivar.Enabled = false;
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
