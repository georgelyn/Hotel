using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Hotel
{
    public partial class Ajustes : Form
    {
        public Ajustes()
        {
            InitializeComponent();

            double monto = Convert.ToDouble(ConfigurationManager.AppSettings["MontoCamion"]);

            txtMontoCamion.Text = String.Format(new CultureInfo("es-VE"), "{0:#,##0.00}", monto);
        }

        string montoActual = ConfigurationManager.AppSettings["MontoCamion"];

        private void CambiarCargoPorCamion()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings["MontoCamion"].Value = txtMontoCamion.Text.Trim();

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                MessageBox.Show("Monto actualizado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se presentó un error. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMontoCamion.Text.Trim()))
            {
                if (montoActual != txtMontoCamion.Text)
                {
                    Msg msg = new Msg();

                    msg.lblMsg.Text = "¿Seguro desea cambiar el monto?";
                    DialogResult dlgres = msg.ShowDialog();
                    if (dlgres == DialogResult.Yes)
                    {
                        CambiarCargoPorCamion();
                        Close();
                    }
                }
            }

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtMontoCamion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

        }
    }
}
