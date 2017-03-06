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
    public partial class Vehiculo : Form
    {
        public Vehiculo()
        {
            InitializeComponent();

            CargarCedulas();
            //CargarVehiculosPorCedula("25036451");
        }

        Msg msg;

        List<String> idVehiculo; // Toma valor en CargarVehiculosPorCedula

        private void CargarCedulas()
        {
            try
            {
                comboCedula.Items.Clear();
                comboVehiculo.Items.Clear();

                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT DISTINCT cedula FROM cliente INNER JOIN vehiculo ON cedula=cedula_cliente", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                comboCedula.Items.Add(dr["cedula"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un error.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<String> CargarVehiculosPorCedula(string cedula, ComboBox combo)
        {
            comboVehiculo.Items.Clear();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM vehiculo WHERE cedula_cliente=@cedula", conn))
                    {
                        idVehiculo = new List<String>();
                        idVehiculo.Clear();

                        cmd.Parameters.AddWithValue("@cedula", cedula);
                        comboCedula.Text = cedula;
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                idVehiculo.Add(dr["id"].ToString());
                                combo.Items.Add(dr["marca"].ToString() + " - " + dr["modelo"].ToString());
                                //comboVehiculo.Items.Add(dr["marca"].ToString() + " - " + dr["modelo"].ToString());
                            }
                        }
                    }

                    return idVehiculo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un error.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private void CargarDatosVehiculo(string id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM vehiculo WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                txtMarca.Text = dr["marca"].ToString();
                                txtModelo.Text = dr["modelo"].ToString();
                                txtPlaca.Text = dr["placa"].ToString();
                                txtNotas.Text = dr["notas"].ToString();

                                if (Convert.ToBoolean(dr["es_camion"]) == true)
                                    checkCamion.Checked = true;
                                else
                                    checkCamion.Checked = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un error.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificarVehiculo(string id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE vehiculo SET es_camion=@esCamion, marca=@marca, modelo=@modelo, placa=@placa, notas=@notas WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        bool esCamion = false;

                        if (checkCamion.Checked)
                            esCamion = true;

                        cmd.Parameters.AddWithValue("@esCamion", esCamion);

                        cmd.Parameters.AddWithValue("@marca", StringExtensions.NullString(txtMarca.Text.Trim()));
                        cmd.Parameters.AddWithValue("@modelo", StringExtensions.NullString(txtModelo.Text.Trim()));
                        cmd.Parameters.AddWithValue("@placa", StringExtensions.NullString(txtPlaca.Text.Trim()));
                        cmd.Parameters.AddWithValue("@notas", StringExtensions.NullString(txtNotas.Text.Trim()));

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Los datos del vehículo han sido modificados.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un error.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarVehiculo(string id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM vehiculo WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        conn.Open();
                        cmd.ExecuteReader();

                        MessageBox.Show("Los datos del vehículo han sido eliminados.");
                        RestaurarCampos();
                        CargarCedulas();
                        //RestaurarCampos();
                        //CargarVehiculosPorCedula(comboCedula.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un error.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RestaurarCampos()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox || c is RichTextBox)
                {
                    c.Text = "";
                }
            }
            checkCamion.Checked = false;
            btnEliminar.Visible = false;
            btnModificar.Visible = false;
            //comboVehiculos.SelectedIndex = -1;
        }

        private void comboVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboVehiculos.SelectedIndex >= 0)
            //{
                CargarDatosVehiculo(idVehiculo[comboVehiculo.SelectedIndex].ToString());
                //MessageBox.Show(idVehiculo[comboVehiculos.SelectedIndex].ToString());

                btnEliminar.Visible = true;
                btnModificar.Visible = true;
            //}
        }

        private void comboCedula_SelectedIndexChanged(object sender, EventArgs e)
        {
            RestaurarCampos();
            CargarVehiculosPorCedula(comboCedula.Text, comboVehiculo);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarVehiculo(idVehiculo[comboVehiculo.SelectedIndex].ToString());
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            msg = new Msg();
            msg.lblMsg.Text = "¿Está seguro de que desea eliminar el registro?";
            DialogResult dlgres = msg.ShowDialog();
            {
                if (dlgres == DialogResult.Yes)
                {
                    EliminarVehiculo(idVehiculo[comboVehiculo.SelectedIndex].ToString());
                }
                else
                {
                    return;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
