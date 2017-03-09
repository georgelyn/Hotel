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

        private int CambiarDatos()
        {
            int error = 0;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Seguridad SET Password = case User" +
                        " when 'admin' then @contrasenaAdmin" +
                        " when 'usuario' then @contrasenaUser" +
                        " when 'emergencia' then 'emergencia'" +
                        " end", conn))
                    {
                        cmd.Parameters.AddWithValue("@contrasenaAdmin", txtAdmin.Text.Trim());
                        cmd.Parameters.AddWithValue("@contrasenaUser", txtUser.Text.Trim());

                        conn.Open();

                        error = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema. \nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return error;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(txtAdmin.Text.Trim()) || String.IsNullOrEmpty(txtUser.Text.Trim())))
            {
                if (CambiarDatos() > 0)
                {
                    MessageBox.Show("Los datos han sido cambiados.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Los campos de texto no pueden estar vacíos. \nIntente nuevamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
