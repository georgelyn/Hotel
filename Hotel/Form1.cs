﻿using System;
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
            //ActualizarColores();

            foreach (Button b in tableLayoutPanel1.Controls)
            {
                b.ContextMenuStrip = contextMenuStrip1;
                b.FlatStyle = FlatStyle.Flat;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();

            if (OperacionesSQLite.ProbarConexion()) // Conexión exitosa
            {
                Inicio inicio = new Inicio();
                if (inicio.ShowDialog() == DialogResult.OK)
                {
                    ActualizarColores();
                    this.Show();
                    this.WindowState = FormWindowState.Maximized;
                }
            }
            else // No se pudo conectar con la base de datos
            {
                msg = new Msg();

                msg.button1.Text = "Restablecer";
                msg.button1.Size = new Size(91, 29);
                msg.button1.Location = new Point(242, 113);
                msg.button2.Text = "Restaurar";
                msg.button2.Size = new Size(89, 29);
                msg.button2.Location = new Point(337, 113);

                msg.lblMsg.Text = "No se pudo acceder a la base de datos.\n¿Desea restablecerla o restaurar una copia?";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes) // Restablecer
                    {
                        if (OperacionesSQLite.CrearBaseDeDatos()) // Se puedo crear la base de datos
                        {
                            //MessageBox.Show("La base de datos ha sido restablecida.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Inicio inicio = new Inicio();
                            if (inicio.ShowDialog() == DialogResult.OK)
                            {
                                ActualizarColores();
                                this.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("La aplicación se va a cerrar. Puede intentarlo nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Close();
                        }
                    }
                    else if (dlgres == DialogResult.No) // Restaurar una copia
                    {
                        //int x = 0;

                        OperacionesSQLite.Eliminar(); // SQLite al no encontrar el archivo, lo vuelve a crear. Así elimino el vacío.

                        //if (OperacionesSQLite.RestaurarCopia())
                        //do
                        //{
                            if (OperacionesSQLite.RestaurarCopia())
                            {
                                //x = 1;
                                Inicio inicio = new Inicio();
                                if (inicio.ShowDialog() == DialogResult.OK)
                                {
                                    ActualizarColores();
                                    this.Show();
                                    this.WindowState = FormWindowState.Maximized;
                                    //break;
                                }
                            }
                            //MessageBox.Show("La base de datos ha sido restaurada.\nLa aplicación se cerrará. Por favor, vuelva a abrirla.", "Copia restaurada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //Close();
                        //} while (x == 0 || dlgres == DialogResult.Cancel); //(!OperacionesSQLite.RestaurarCopia());
                        else
                        {
                            Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }

                }
            }
        }

        // Clases
        Reservacion reservacion;
        Cliente cliente;
        ListaVehiculos vehiculos;
        Habitacion habitacion;
        Msg msg;

        Color disponible = Color.LimeGreen;
        Color inactiva = Color.DarkGray;
        Color limpieza = Color.White;
        Color mantenimiento = Color.Yellow;
        Color ocupada = Color.IndianRed;

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
                                            b.BackColor = disponible;
                                        }
                                    }
                                    else if (dr["Estado"].ToString() == "ocupada")
                                    {
                                        if (dr["NumeroHabitacion"].ToString() == b.Name.Remove(0, 6))
                                        {
                                            b.BackColor = ocupada;
                                        }
                                    }
                                    else if (dr["Estado"].ToString() == "limpieza")
                                    {
                                        if (dr["NumeroHabitacion"].ToString() == b.Name.Remove(0, 6))
                                        {
                                            b.BackColor = limpieza;
                                        }
                                    }
                                    else if (dr["Estado"].ToString() == "mantenimiento")
                                    {
                                        if (dr["NumeroHabitacion"].ToString() == b.Name.Remove(0, 6))
                                        {
                                            b.BackColor = mantenimiento;
                                        }
                                    }
                                    else if (dr["Estado"].ToString() == "inactiva")
                                    {
                                        if (dr["NumeroHabitacion"].ToString() == b.Name.Remove(0, 6))
                                        {
                                            b.BackColor = inactiva;
                                            b.ForeColor = inactiva;
                                            b.Enabled = false;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (SQLiteException)
            {
                return; // No muestres mensaje de error

                //if (ex.ErrorCode == 10) // Disk I/O Error
                //{
                //    ActivarTimerEspera();
                //    ActualizarColores();
                //}
                //else
                //{
                //if (ex.Message.Contains("unable to connect"))
                //{
                //    MessageBox.Show("No se puede acceder a la base de datos. El programa se cerrará.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    Close();
                //}
                //else
                //{
                ////MessageBox.Show("No se pudo conectar con la base de datos. \nDescripción del error: \n\n>> " + +ex.ErrorCode + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
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

                        //MessageBox.Show(numeroHabitacion);
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

        public void ComprobarReservas()
        {
            using (reservacion = new Reservacion())
            {
                foreach (Button b in tableLayoutPanel1.Controls)
                {
                    if (b.BackColor == ocupada)
                    {
                        if (!reservacion.HayReserva(int.Parse(b.Name.Remove(0, 6))))
                        {
                            //MessageBox.Show(b.Name.Remove(0, 6));
                            CambiarEstadoHabitacion(false, "disponible", b.Name.Remove(0, 6));
                            b.BackColor = disponible;
                            //ActualizarColores();

                        }
                    }

                    /*if (b.BackColor == disponible)
                    {
                        if (reservacion.HayReserva(int.Parse(b.Name.Remove(0, 6))))
                        {
                            //MessageBox.Show(b.Name.Remove(0, 6));
                            CambiarEstadoHabitacion(true, "ocupada", b.Name.Remove(0, 6));
                            b.BackColor = ocupada;
                            //ActualizarColores();

                        }
                    }*/
                }
            }
        }

        private void button53_Click(object sender, EventArgs e) // Nueva reservación
        {
            ActivarTimerEspera();

            foreach (Button b in tableLayoutPanel1.Controls) // Verifica si hay al menos una habitación disponible
            {
                if (b.BackColor == disponible)
                {
                    reservacion = new Reservacion();
                    reservacion.ShowDialog();
                    return;
                }
            }

            MessageBox.Show("No hay ninguna habitación disponible.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == limpieza || ((Button)sender).BackColor == mantenimiento)
            {
                msg = new Msg();

                msg.lblMsg.Text = "¿Desea cambiar el estado a disponible?";
                msg.Text = $"Confirmación | Habitación {((Button)sender).Name.Remove(0, 6)}";
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
            }

            if (((Button)sender).BackColor == disponible || (((Button)sender).BackColor == ocupada))
            {
                ActivarTimerEspera();

                int numeroHabitacion = int.Parse(((Button)sender).Name.Remove(0, 6));//Text);
                reservacion = new Reservacion();

                reservacion.PanelCedula(false);

                if (((Button)sender).BackColor == ocupada)
                {
                    if (!reservacion.HayReserva(numeroHabitacion)) // Aparece como ocupada pero NO tiene datos de reserva almacenados
                    {
                        CambiarEstadoHabitacion(false, "disponible", numeroHabitacion.ToString());
                        ActualizarColores();
                        return;
                    }
                    reservacion.CargarReservacion(numeroHabitacion, "ocupada");
                }
                else if (((Button)sender).BackColor == disponible)
                {
                    if (reservacion.HayReserva(numeroHabitacion)) // Aparece como disponible pero aún tiene datos de reserva almacenados
                    {
                        CambiarEstadoHabitacion(true, "disponible", numeroHabitacion.ToString());
                    }

                    reservacion.CargarReservacion(numeroHabitacion, "disponible");
                }

                reservacion.ShowDialog();
            }
        }

        private void button55_Click(object sender, EventArgs e) // Habitaciones ocupadas
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

        private void button54_Click(object sender, EventArgs e) // Lista de clientes
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

                msg.lblMsg.Text = "Al cambiar su estado a limpieza, se eliminará la reservación actual.\n\n¿Desea continuar?";
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

            if (btn.BackColor == disponible) // La habitación aparece disponible pero aún están los datos de la reserva en la base de datos
            {
                if (CambiarEstadoHabitacion(true, "disponible", btn.Name.Remove(0, 6)))
                {
                    btn.BackColor = disponible;
                }
            }

            else if (btn.BackColor == limpieza || btn.BackColor == mantenimiento) // Estaba en limpieza o en mantenimiento
            {
                if (CambiarEstadoHabitacion(false, "disponible", btn.Name.Remove(0, 6)))
                {
                    btn.BackColor = disponible;
                }
            }

            else if (btn.BackColor == ocupada) // Esta(ba) ocupada
            {
                msg = new Msg();

                msg.lblMsg.Text = "Al cambiar su estado a disponible, se eliminará la reservación actual.\n\n¿Desea continuar?";
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

                msg.lblMsg.Text = "Al cambiar su estado a mantenimiento, se eliminará la reservación actual.\n\n¿Desea continuar?";
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

                msg.lblMsg.Text = "Al desactivar la habitación, se eliminará la reservación actual.\n\n¿Desea continuar?";
                msg.Text = $"Confirmación | Habitación {btn.Name.Remove(0, 6)}";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes)
                    {
                        if (CambiarEstadoHabitacion(true, "inactiva", btn.Name.Remove(0, 6)))
                        {
                            //btn.FlatStyle = FlatStyle.System;
                            btn.ForeColor = inactiva;
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
                    btn.ForeColor = inactiva;
                    btn.BackColor = inactiva;
                    btn.Enabled = false;
                }
            }

        }

        private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
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
                            btn.ForeColor = Color.Black;
                            btn.Enabled = true;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
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
            if (vehiculos == null || vehiculos.IsDisposed)
            {
                vehiculos = new ListaVehiculos();
                vehiculos.Show();
            }
            else
            {
                vehiculos.Activate();
            }          
        }

        private void tiposDeHabitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();

            if (login.ShowDialog() == DialogResult.OK)
            {
                habitacion = new Habitacion();
                habitacion.ShowDialog();
            }

        }

        private void contraseñasDelSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();

            if (login.ShowDialog() == DialogResult.OK)
            {
                Seguridad seguridad = new Seguridad();
                seguridad.ShowDialog();
            }
        }

        private void restablecerBaseDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();

            if (login.ShowDialog() == DialogResult.OK)
            {
                msg = new Msg();
                msg.lblMsg.Text = "Al restablecer la base de datos, perderá los datos actuales.\n¿Desea continuar?";
                DialogResult dlgres = msg.ShowDialog();
                if (dlgres == DialogResult.Yes)
                {
                    ActivarTimerEspera();
                    OperacionesSQLite.Restablecer();
                    ActualizarColores();

                    foreach (Button btn in tableLayoutPanel1.Controls)
                    {
                        if (!btn.Enabled)
                        {
                            btn.Enabled = true;
                            btn.ForeColor = default(Color);
                        }
                    }
                }
            }
            
        }

        private void respaldarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperacionesSQLite.HacerCopia();
        }

        private void restaurarCopiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();

            if (login.ShowDialog() == DialogResult.OK)
            {
                if (OperacionesSQLite.RestaurarCopia())
                {
                    //MessageBox.Show("La base de datos ha sido restaurada.\nLa aplicación se cerrará. Por favor, vuelva a abrirla.", "Copia restaurada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Close();
                    ActivarTimerEspera();
                    ActualizarColores();
                }
            }
            
        }

        private void cargoPorCamiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();

            if (login.ShowDialog() == DialogResult.OK)
            {
                Ajustes ajustes = new Ajustes();
                ajustes.ShowDialog();
            }
        }

        private void acercadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acerca acerca = new Acerca();
            acerca.ShowDialog();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //timer3.Start();

            if (OperacionesSQLite.ProbarConexion())
            {
                ActivarTimerEspera();
                ActualizarColores();
            }
            else
            {
                MessageBox.Show("No se puede acceder a la base de datos. El programa se cerrará.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }

        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            timer3.Start();
            timer2.Start();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            timer3.Stop();
            timer2.Stop();
        }

        private void listaNegraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListaNegra negra = new ListaNegra();
            negra.ShowDialog();
        }

        /*private void button56_Click(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Baneados", conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Lista negra eliminada.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }

        }*/
    }
}
