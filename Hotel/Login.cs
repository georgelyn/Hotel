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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        bool errorConexion = false;

        public bool IngresarAlSistema(string usuario, string contrasena)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Seguridad WHERE User=@usuario AND Password=@contrasena", conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);
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
                MessageBox.Show("Se ha presentado un problema. \nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorConexion = true;
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtContrasena.Text.Trim()))
            {
                if (IngresarAlSistema("admin", txtContrasena.Text.Trim()))
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    if (!errorConexion)
                    {
                        MessageBox.Show("La contraseña ingresada es incorrecta. Por favor, intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtContrasena.Select();
                        this.DialogResult = DialogResult.None;
                    }
                }
            }
            else
            {
                label2.ForeColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
