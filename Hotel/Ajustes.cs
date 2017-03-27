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
using System.Data.SQLite;

namespace Hotel
{
    public partial class Ajustes : Form
    {
        public Ajustes()
        {
            InitializeComponent();

            CargarAjustes();
            txtRespaldos.SelectionStart = txtRespaldos.TextLength;

            #region Viejo - Con app.config
            //double monto = Convert.ToDouble(ConfigurationManager.AppSettings["MontoCamion"], new CultureInfo("es-ES")); // Esto quita error de "Input string was not in a correct format" si la cultura es una diferente (como en-US)

            //txtMontoCamion.Text = String.Format(new CultureInfo("es-ES"), "{0:#,##0.00}", monto);

            //if (String.IsNullOrEmpty(carpetaRespaldos))
            //{
            //    if (Directory.Exists(Application.StartupPath + "\\Respaldos"))
            //    {
            //        txtRespaldos.Text = Application.StartupPath + "\\Respaldos";
            //    }
            //    else
            //    {
            //        txtRespaldos.Text = Application.StartupPath;
            //    }
            //}
            //else
            //{
            //    txtRespaldos.Text = carpetaRespaldos;
            //}

            //txtRespaldos.SelectionStart = txtRespaldos.TextLength;
            #endregion
        }

        //string montoActual = ConfigurationManager.AppSettings["MontoCamion"];
        //string carpetaRespaldos = ConfigurationManager.AppSettings["CarpetaRespaldos"];

        private void CargarAjustes()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Ajustes", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    txtMontoCamion.Text = String.Format(new CultureInfo("es-ES"), "{0:#,##0.00}", dr["MontoCamion"]);

                                    if (dr["CarpetaRespaldos"] == DBNull.Value)
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
                                        txtRespaldos.Text = dr["CarpetaRespaldos"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CambiarDatos()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Ajustes SET MontoCamion=@monto, CarpetaRespaldos=@ubicacion", conn))
                    {
                        cmd.Parameters.AddWithValue("@monto", txtMontoCamion.Text.Replace(".", "").Replace(",", "."));
                        cmd.Parameters.AddWithValue("@ubicacion", txtRespaldos.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;

            #region Viejo - Con app.config
            //try
            //{
            //    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //    config.AppSettings.Settings["MontoCamion"].Value = txtMontoCamion.Text.Trim();

            //    config.Save(ConfigurationSaveMode.Modified);
            //    ConfigurationManager.RefreshSection("appSettings");

            //    MessageBox.Show("Monto actualizado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Se ha presentado un error. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            #endregion
        }

        //private void CambiarCarpetaRespaldos()
        //{         
        //    #region Viejo - Con app.config
        //    //try
        //    //{
        //    //    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        //    //    config.AppSettings.Settings["CarpetaRespaldos"].Value = txtRespaldos.Text;

        //    //    config.Save(ConfigurationSaveMode.Modified);
        //    //    ConfigurationManager.RefreshSection("appSettings");

        //    //    //MessageBox.Show("Monto actualizado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //    Close();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    MessageBox.Show("Se ha presentado un error. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    //}
        //    #endregion
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            Msg msg = new Msg();

            msg.lblMsg.Text = "¿Está seguro de que desea cambiar los datos?";
            DialogResult dlgres = msg.ShowDialog();
            if (dlgres == DialogResult.Yes)
            {
                //if (!String.IsNullOrEmpty(txtMontoCamion.Text.Trim()))
                //{
                //    CambiarCargoPorCamion();
                //}


                //CambiarCarpetaRespaldos();

                if (CambiarDatos())
                {
                    MessageBox.Show("Los datos han sido cambiados.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }

            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtMontoCamion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (String.IsNullOrEmpty(txtMontoCamion.Text.Trim()))
            {
                if (e.KeyChar == '.' || e.KeyChar == ',')
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (txtMontoCamion.Text.Contains("."))
                {
                    if (e.KeyChar == '.')
                    {
                        e.Handled = true;
                    }
                }

                if (txtMontoCamion.Text.Contains(","))
                {
                    if (e.KeyChar == ',')
                    {
                        e.Handled = true;
                    }
                }

                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != '.' && e.KeyChar != ',') // 8 = backspace, 13 = enter
                {
                    e.Handled = true;
                }

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

        private void txtMontoCamion_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMontoCamion.Text))
            {
                txtMontoCamion.Text = "0,00";
            }
        }
    }
}
