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
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();

            txtContrasena.Select();
        }

        Form1 f1 = (Form1)Application.OpenForms["Form1"];

        bool errorConexion = false;

        private bool Ingresar(string contrasena)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT Password FROM Seguridad WHERE Password=@contrasena", conn))
                    {
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
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f1.ActivarTimerEspera();

            if (!String.IsNullOrEmpty(txtContrasena.Text.Trim()))
            {
                if (Ingresar(txtContrasena.Text.Trim()))
                {
                    //f1.Show();
                    //this.Close();

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
                label1.ForeColor = Color.Red;
                txtContrasena.Select();
            }
        }
    }
}
