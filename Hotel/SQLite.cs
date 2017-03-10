using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Hotel
{
    public partial class SQLite : Form
    {
        public SQLite()
        {
            InitializeComponent();
        }

        private void Backup()
        {
            string fechaActual = DateTime.Now.ToString("yyyy-MM-dd_hh-mm");
            string nombre = "HotelBD-" + fechaActual + ".db";
            string ubicacion = Application.StartupPath + "\\Respaldos\\" + nombre;

            string query = @"Data Source=|DataDirectory|" + ubicacion + ";Version=3;New=True;Compress=True;Foreign Keys=ON";

            using (var original = new SQLiteConnection(ConexionBD.connstring))
            using (var respaldo = new SQLiteConnection(query))
            {
                original.Open();
                respaldo.Open();
                original.BackupDatabase(respaldo, "main", "main", -1, null, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Backup();
        }

        private bool ProbarConexion()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);
            }
            return false;
        }

        private void ActualizarConexion(string ubicacion)
        {
            string nuevaUbicacion = ubicacion;
            string stringConnection = "Data Source=|" + nuevaUbicacion + ";Version=3;New=True;Compress=True;Foreign Keys=ON";

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.ConnectionStrings.ConnectionStrings["connstring"].ConnectionString = stringConnection;

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("connectionStrings");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Archivo de base da datos (.db)|*.db|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = false;

            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string respaldo = openFileDialog1.FileName;

                Msg msg = new Msg();

                msg.lblMsg.Text = $"¿Está seguro de que desea restaurar la copia?\nNota: Los datos actuales se perderán.";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes)
                    {
                        ConexionBD.nuevaUbicacion = respaldo;
                        ActualizarConexion(respaldo);

                        if (ProbarConexion())
                        {
                            MessageBox.Show("Respaldo restaurado");
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ConexionBD.nuevaUbicacion);
            MessageBox.Show(ConexionBD.connstring);
        }
    }
}
