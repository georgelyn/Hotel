using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Hotel
{
    public partial class Ajustes : Form
    {
        public Ajustes()
        {
            InitializeComponent();

            double monto = Convert.ToDouble(ConfigurationManager.AppSettings["MontoCamion"]);

            txtMontoCamion.Text = String.Format(new CultureInfo("es-VE"), "{0:#,##0.00}", monto);

            if (String.IsNullOrEmpty(carpetaRespaldos))
            {
                if (Directory.Exists(Application.StartupPath + "\\Respaldos"))
                {
                    txtRespaldos.Text = Application.StartupPath + "\\Respaldos";
                }
                else
                {
                    txtRespaldos.Text = Application.StartupPath;
                }
            }
            else
            {
                txtRespaldos.Text = carpetaRespaldos;
            }

            txtRespaldos.SelectionStart = txtRespaldos.TextLength;
        }

        string montoActual = ConfigurationManager.AppSettings["MontoCamion"];
        string carpetaRespaldos = ConfigurationManager.AppSettings["CarpetaRespaldos"];

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
                MessageBox.Show("Se ha presentado un error. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CambiarCarpetaRespaldos()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings["CarpetaRespaldos"].Value = txtRespaldos.Text;

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                //MessageBox.Show("Monto actualizado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un error. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (montoActual != txtMontoCamion.Text || carpetaRespaldos != txtRespaldos.Text)
            {
                Msg msg = new Msg();

                msg.lblMsg.Text = "¿Está seguro de que desea cambiar los datos?";
                DialogResult dlgres = msg.ShowDialog();
                if (dlgres == DialogResult.Yes)
                {
                    if (montoActual != txtMontoCamion.Text)
                    {
                        if (!String.IsNullOrEmpty(txtMontoCamion.Text.Trim()))
                        {
                            CambiarCargoPorCamion();
                        }
                    }
                    if (carpetaRespaldos != txtRespaldos.Text)
                    {
                        CambiarCarpetaRespaldos();
                    }

                    Close();
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

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog carpetaRespaldos = new FolderBrowserDialog();
            DialogResult result = carpetaRespaldos.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtRespaldos.Text = carpetaRespaldos.SelectedPath;
            }
        }
    }
}
