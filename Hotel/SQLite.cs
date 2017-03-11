using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace Hotel
{
    public partial class SQLite : Form
    {
        public SQLite()
        {
            InitializeComponent();
        }

        private void HacerCopia()
        {
            string fechaActual = DateTime.Now.ToString("yyyy-MM-dd_hh-mm");
            string nombre = "HotelBD-" + fechaActual + ".db";
            //string ubicacion = Application.StartupPath + "\\Respaldos\\" + nombre;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Application.StartupPath + "\\Respaldos";
            saveFileDialog1.Filter = "Archivo de base de datos|*.db";
            saveFileDialog1.Title = "Guardar copia de seguridad";
            saveFileDialog1.FileName = nombre;
            DialogResult dr = saveFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string query = @"Data Source=|DataDirectory|" + saveFileDialog1.FileName + ";Version=3;New=True;Compress=True;Foreign Keys=ON";

                using (var original = new SQLiteConnection(ConexionBD.connstring))
                using (var respaldo = new SQLiteConnection(query))
                {
                    original.Open();
                    respaldo.Open();
                    original.BackupDatabase(respaldo, "main", "main", -1, null, 0);
                }
            }
        }

        private void RestaurarCopia()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Archivo de base da datos (*.db, *.sqlite, *.sqlite3) | *.db; *.sqlite; *.sqlite3";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\Respaldos";

            openFileDialog1.Multiselect = false;

            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string respaldo = openFileDialog1.FileName;
                string nuevaConexion = "Data Source=" + respaldo + ";Version=3;New=True;Compress=True;Foreign Keys=ON";

                if (ProbarConexion(nuevaConexion))
                {
                    Msg msg = new Msg();

                    msg.lblMsg.Text = $"¿Está seguro de que desea restaurar la copia?\nNota: Los datos actuales se perderán.";
                    DialogResult dlgres = msg.ShowDialog();
                    {
                        if (dlgres == DialogResult.Yes)
                        {
                            if (ActualizarConexion(respaldo))
                            {
                                MessageBox.Show("La base de datos ha sido restaurada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            //if (ProbarConexion())
                            //{
                            //    MessageBox.Show("Respaldo restaurado");
                            //}
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                //else
                //{
                //    MessageBox.Show("El archivo de base de datos no es válido. Por favor, verifique.");
                //}
            }
        }

        private void CrearNuevaBaseDatos()
        {
            try
            {
                System.Data.SQLite.SQLiteConnection.CreateFile("HotelCountry.db"); // Crear el archivo. No realmente necesario, ya que SQLite lo crear automáticamente si no lo encuentra

                using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection(@"Data Source=|DataDirectory|\HotelCountry.db;Version=3;New=True;Compress=True;Foreign Keys=ON"))
                {
                    using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                    {
                        conn.Open(); 

                        cmd.CommandText = CrearBaseDatos.query;
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "INSERT INTO TipoHabitacion (Tipo, Costo, Activa) VALUES ('Matrimonial','15000', '1'), ('Doble', '16000', '1'), ('Triple', '18000', '1'), " +
                            "('Cuádruple', '20000', '1'), ('Séxtuple', '25000', '1'), ('Mini-Suite', '25000', '1')";    
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "INSERT INTO Seguridad (User, Password) VALUES ('admin','ra0408'), ('usuario', '2506'), ('admin', 'emergencia')"; 
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\n\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RestablecerBase()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=|DataDirectory|\\Respaldos\\Hotel-10-03-2017-04-31.db"))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Clientes; DELETE FROM Reservaciones; DELETE FROM Vehiculos; UPDATE Habitaciones SET Estado='disponible'", conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\n\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ProbarConexion(string connstring)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT Nombre FROM Clientes", conn))
                    {
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
                if (ex.Message.Contains("not a database"))
                {
                    MessageBox.Show("El archivo de base de datos no es válido. Por favor, verifique.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Se ha presentado un problema.\n\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }

        private bool ActualizarConexion(string ubicacion)
        {
            try
            {
                string stringConnection = "Data Source=" + ubicacion + ";Version=3;New=True;Compress=True;Foreign Keys=ON";

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.ConnectionStrings.ConnectionStrings["connstring"].ConnectionString = stringConnection;

                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("connectionStrings");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("La base de datos no fue restaurada.\n\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HacerCopia();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            RestaurarCopia();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RestablecerBase();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CrearNuevaBaseDatos();
        }
    }
}
