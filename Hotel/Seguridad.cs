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
    public partial class Seguridad : Form
    {
        public Seguridad()
        {
            InitializeComponent();

            CargarDatos();
        }

        private void CargarDatos()
        {
            TextBox[] txt = { txtAdmin, txtUser };
            int i = 0;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT Password FROM Seguridad", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read() && i < txt.Length)
                            {
                                txt[i].Text = dr["Password"].ToString();
                                i++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema. \nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CambiarDatos()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Seguridad SET Password = case User" +
                        "when 'admin' then @contrasenaAdmin" +
                        "when 'user' then @contrasenaUser" +
                        "when 'emergencia' then 'emergencia'" +
                        "end", conn))
                    {
                        cmd.Parameters.AddWithValue("@contrasenaAdmin", txtAdmin.Text.Trim());
                        cmd.Parameters.AddWithValue("@contrasenaUser", txtUser.Text.Trim());

                        conn.Open();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema. \nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CambiarDatos();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
