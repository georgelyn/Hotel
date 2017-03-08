using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Hotel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Llamada a métodos
            ActualizarColores();

            foreach (Button b in tableLayoutPanel1.Controls)
            {
                b.ContextMenuStrip = contextMenuStrip1;
                b.FlatStyle = FlatStyle.Flat;
            }


        }

        // Clases
        Reservacion reservacion;
        Cliente cliente;
        Vehiculo vehiculo;
        Habitacion habitacion;
        Msg msg;

        Color disponible = Color.Green;
        Color inactiva = Color.LightGray;
        Color limpieza = Color.LightCyan;
        Color mantenimiento = Color.DarkKhaki;
        Color ocupada = Color.Red;

        public void ActivarTimerEspera()
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
        }

        public void ActualizarColores()
        {
            //List<Control> listControls = flowLayoutPanel1.Controls.Cast<Control>().ToList();

            try
            {

                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT NumeroHabitacion, Estado FROM Habitaciones", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                foreach (Button b in tableLayoutPanel1.Controls)
                                {
                                    if (dr["Estado"].ToString() == "disponible")
                                    {
                                        if (dr["NumeroHabitacion"].ToString() == b.Name.Remove(0, 6)) //b.Text) // Remove 0,6 -> button
                                        {
                                            //b.FlatStyle = FlatStyle.Flat;
                                            b.BackColor = disponible;
                                        }
                                    }
                                    else if (dr["Estado"].ToString() == "ocupada")
                                    {
                                        if (dr["NumeroHabitacion"].ToString() == b.Name.Remove(0, 6))
                                        {
                                            //b.FlatStyle = FlatStyle.Flat;
                                            b.BackColor = ocupada;
                                        }
                                    }
                                    else if (dr["Estado"].ToString() == "limpieza")
                                    {
                                        if (dr["NumeroHabitacion"].ToString() == b.Name.Remove(0, 6))
                                        {
                                            //b.FlatStyle = FlatStyle.Flat;
                                            b.BackColor = limpieza;
                                        }
                                    }
                                    else if (dr["Estado"].ToString() == "mantenimiento")
                                    {
                                        if (dr["NumeroHabitacion"].ToString() == b.Name.Remove(0, 6))
                                        {
                                            //b.FlatStyle = FlatStyle.Flat;
                                            b.BackColor = mantenimiento;
                                        }
                                    }
                                    else if (dr["Estado"].ToString() == "inactiva")
                                    {
                                        if (dr["NumeroHabitacion"].ToString() == b.Name.Remove(0, 6))
                                        {
                                            //b.FlatStyle = FlatStyle.System;
                                            b.BackColor = inactiva;
                                            b.Enabled = false;
                                            //b.BackColor = Color.DarkGray;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("No se pudo conectar con la base de datos. \nDescripción del error: \n\n>> " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CambiarEstadoHabitacion(bool ocupada, string estado, string numeroHabitacion)
        {
            try
            {
                string query = "UPDATE Habitaciones SET Estado=@estado WHERE NumeroHabitacion=@numeroHabitacion";

                if (ocupada)
                {
                    query += "; DELETE FROM Reservaciones WHERE NumeroHabitacion=@numeroHabitacion";
                }

                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@estado", estado);
                        cmd.Parameters.AddWithValue("@numeroHabitacion", numeroHabitacion);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        return true; // Se completó sin problemas
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema. \nDescripción del error: \n\n>> " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false; // Hubo un problema
        }

        private void button53_Click(object sender, EventArgs e)
        {
            ActivarTimerEspera();

            //if (habitaciones == null || habitaciones.IsDisposed)
            //{
                reservacion = new Reservacion();
                reservacion.ShowDialog();
            //}
            //else
            //{
            //    habitaciones.Activate();
            //}


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == limpieza)
            {
                msg = new Msg();

                msg.lblMsg.Text = "¿Desea cambiar el estado a disponible?";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes)
                    {
                        if (CambiarEstadoHabitacion(false, "disponible", ((Button)sender).Name.Remove(0, 6)))
                        {
                            ((Button)sender).BackColor = disponible;
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                //MessageBox.Show("Limpieza - Falta implementar o.ó");
                ////MessageBox.Show(((Button)sender).Name.Remove(0, 6).ToString());
            }
            if (((Button)sender).BackColor == disponible || (((Button)sender).BackColor == ocupada))
            {
                ActivarTimerEspera();

                int numero_habitacion = int.Parse(((Button)sender).Text);
                reservacion = new Reservacion();

                reservacion.PanelCedula(false);

                if (((Button)sender).BackColor == ocupada)
                {
                    reservacion.CargarReservacion(numero_habitacion, "ocupada");
                }
                else if (((Button)sender).BackColor == disponible)
                {
                    reservacion.CargarReservacion(numero_habitacion, "disponible");
                }

                reservacion.ShowDialog();
            }
            //MessageBox.Show(((Button)sender).Name + " was pressed!");
        }


        private void button55_Click(object sender, EventArgs e)
        {
            ActivarTimerEspera();

            if (cliente == null || cliente.IsDisposed)
            {
                cliente = new Cliente();
                cliente.CargarListView("habitacion");
                cliente.Show();
            }
            else
            {
                cliente.Activate();
            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            ActivarTimerEspera();

            if (cliente == null || cliente.IsDisposed)
            {
                cliente = new Cliente();
                cliente.CargarListView("cliente");
                cliente.Show();
            }
            else
            {
                cliente.Activate();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void enLimpiezaToolStripMenuItem_Click(object sender, EventArgs e) // Limpieza
        {
            Button btn = (Button)contextMenuStrip1.SourceControl;

            if (btn.BackColor == disponible || btn.BackColor == mantenimiento) // Si estaba disponible o en mantenimiento
            {
                if (CambiarEstadoHabitacion(false, "limpieza", btn.Name.Remove(0, 6)))
                {
                    btn.BackColor = limpieza;
                }
            }
            else if (btn.BackColor == ocupada)
            {
                msg = new Msg();

                msg.lblMsg.Text = "Al cambiar su estado a limpieza, se eliminará la reservación actual.\n\n¿Seguro desea hacer eso?";
                msg.Text = $"Confirmación | Habitación {btn.Name.Remove(0, 6)}";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes)
                    {
                        if (CambiarEstadoHabitacion(true, "limpieza", btn.Name.Remove(0, 6)))
                        {
                            btn.BackColor = limpieza;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

            }

        }

        private void marcarComoDisponibleToolStripMenuItem_Click(object sender, EventArgs e) // Disponible
        {
            Button btn = (Button)contextMenuStrip1.SourceControl;

            if (btn.BackColor == limpieza || btn.BackColor == mantenimiento) // Estaba en limpieza o en mantenimiento
            {
                if (CambiarEstadoHabitacion(false, "disponible", btn.Name.Remove(0, 6)))
                {
                    btn.BackColor = disponible;
                }
            }

            else if (btn.BackColor == ocupada) // Esta(ba) ocupada
            {
                msg = new Msg();

                msg.lblMsg.Text = "Al cambiar su estado a disponible, se eliminará la reservación actual.\n\n¿Seguro desea hacer eso?";
                msg.Text = $"Confirmación | Habitación {btn.Name.Remove(0, 6)}";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes)
                    {
                        if (CambiarEstadoHabitacion(true, "disponible", btn.Name.Remove(0, 6)))
                        {
                            btn.BackColor = disponible;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void marcarMantenimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button btn = (Button)contextMenuStrip1.SourceControl;

            if (btn.BackColor == ocupada)
            {
                msg = new Msg();

                msg.lblMsg.Text = "Al cambiar su estado a mantenimiento, se eliminará la reservación actual.\n\n¿Seguro desea hacer eso?";
                msg.Text = $"Confirmación | Habitación {btn.Name.Remove(0, 6)}";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes)
                    {
                        if (CambiarEstadoHabitacion(true, "mantenimiento", btn.Name.Remove(0, 6)))
                        {
                            btn.BackColor = mantenimiento;
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
                if (CambiarEstadoHabitacion(false, "mantenimiento", btn.Name.Remove(0, 6)))
                {
                    btn.BackColor = mantenimiento;
                }
            }
        }

        private void marcarInactivaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button btn = (Button)contextMenuStrip1.SourceControl;

            if (btn.BackColor == ocupada)
            {
                msg = new Msg();

                msg.lblMsg.Text = "Al desactivar la habitación, se eliminará la reservación actual.\n\n¿Seguro desea hacer eso?";
                msg.Text = $"Confirmación | Habitación {btn.Name.Remove(0, 6)}";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes)
                    {
                        if (CambiarEstadoHabitacion(true, "inactiva", btn.Name.Remove(0, 6)))
                        {
                            //btn.FlatStyle = FlatStyle.System;
                            btn.BackColor = inactiva;
                            btn.Enabled = false;
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
                if (CambiarEstadoHabitacion(false, "inactiva", btn.Name.Remove(0, 6)))
                {
                    //btn.FlatStyle = FlatStyle.System;
                    btn.BackColor = inactiva;
                    btn.Enabled = false;
                }
            }

        }

        private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("e.Location: " + e.Location.ToString() + "\nButton52.Location: " + button52.Location +
            //    "\n\n Button52.ClientRectangle: " + button52.ClientRectangle + "\n\n Contains(e.Location): " + button52.ClientRectangle.Contains(e.Location));

            Button btn = (Button)tableLayoutPanel1.GetChildAtPoint(e.Location);

            if (btn != null && !btn.Enabled)
            {
                msg = new Msg();

                msg.lblMsg.Text = $"¿Desea activar la habitación {btn.Name.Remove(0, 6)}?";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes)
                    {
                        if (CambiarEstadoHabitacion(false, "disponible", btn.Name.Remove(0, 6)))
                        {
                            //btn.FlatStyle = FlatStyle.Flat;
                            btn.BackColor = disponible;
                            btn.Enabled = true;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            //Point ubicacionClick = new Point(e.Location.X, e.Location.Y);
            //Point ubicacionBoton = new Point(btn.Location.X, btn.Location.Y);

            //int ubicacionClickX = ubicacionClick.X;
            //int ubicacionClickY = ubicacionClick.Y;

            //int ubicacionBotonX = button52.Location.X;
            //int ubicacionBotonY = button52.Location.Y;

            //if (ubicacionClickX >= ubicacionBotonX && ubicacionBotonY <= ubicacionClickY)
            //{
            //    if (!btn.Enabled)
            //    {
            //        btn.Enabled = true;
            //    }

            //}

            //MessageBox.Show($"ubicacionClick: {ubicacionClick} \n\nubicaciónBoton: {ubicacionBoton}");

            //if (ubicacionClick.Equals(ubicacionBoton))
            //{
            //    MessageBox.Show("Hola");
            //}


            //if (button52.ClientRectangle.Contains(button52.PointToClient(e.Location)))
            //{
            //    MessageBox.Show("Hola");
            //}

            //if (button53.ClientRectangle.Contains(e.Location))
            //{
            //    MessageBox.Show("Hola");
            //    gridPSR.Enabled = true;
            //}
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString(" dddd" + ", " + "dd/MMM/yyyy" + " - " + "hh:mm:ss tt");
            timer1.Start();
        }

        private void clientesAlmacenadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button54_Click(null, null);
        }

        private void habitacionesOcupadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button55_Click(null, null);
        }

        private void vehículosAlmacenadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivarTimerEspera();

            if (vehiculo == null || vehiculo.IsDisposed)
            {
                vehiculo = new Vehiculo();
                vehiculo.Show();
            }
            else
            {
                vehiculo.Activate();
            }
        }

        private void tiposDeHabitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            habitacion = new Habitacion();
            habitacion.ShowDialog();

        }
    }
}
