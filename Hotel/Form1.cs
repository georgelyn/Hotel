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
        }

        // Clases
        Reservacion reservacion;
        Cliente cliente;

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
                                            b.BackColor = Color.Green;
                                        }
                                    }
                                    else if (dr["Estado"].ToString() == "ocupada")
                                    {
                                        if (dr["NumeroHabitacion"].ToString() == b.Name.Remove(0, 6))
                                        {
                                            b.BackColor = Color.Red;
                                        }
                                    }
                                    else if (dr["Estado"].ToString() == "limpieza")
                                    {
                                        if (dr["NumeroHabitacion"].ToString() == b.Name.Remove(0, 6))
                                        {
                                            b.BackColor = Color.Gray;
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


        private void button53_Click(object sender, EventArgs e)
        {

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
            if (((Button)sender).BackColor == Color.Gray)
            {
                MessageBox.Show("Limpieza - Falta implementar o.ó");
                //MessageBox.Show(((Button)sender).Name.Remove(0, 6).ToString());
            }
            else
            {
                int numero_habitacion = int.Parse(((Button)sender).Text);
                reservacion = new Reservacion();

                reservacion.PanelCedula(false);

                if (((Button)sender).BackColor == Color.Red)
                {
                    reservacion.CargarReservacion(numero_habitacion, "ocupada");
                }
                else if (((Button)sender).BackColor == Color.Green)
                {
                    reservacion.CargarReservacion(numero_habitacion, "disponible");
                }

                reservacion.ShowDialog();
            }
            //MessageBox.Show(((Button)sender).Name + " was pressed!");
        }

        private void button55_Click(object sender, EventArgs e)
        {
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
    }
}
