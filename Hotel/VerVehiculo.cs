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
    public partial class VerVehiculo : Form
    {
        public VerVehiculo()
        {
            InitializeComponent();

            CargarCedulas();
            //CargarVehiculosPorCedula("25036451");
        }

        Msg msg;
        Form1 f1 = (Form1)Application.OpenForms["Form1"];

        List<String> idVehiculo; // Toma valor en CargarVehiculosPorCedula
        List<String> nombreCliente;

        private void CargarCedulas()
        {
            try
            {
                comboCedula.Items.Clear();
                comboVehiculo.Items.Clear();

                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT Cedula, Nombre FROM Clientes INNER JOIN Vehiculos ON Cedula=Cliente_Cedula GROUP BY Nombre", conn))
                    {
                        nombreCliente = new List<string>();
                        nombreCliente.Clear();
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                nombreCliente.Add(dr["Nombre"].ToString());
                                comboCedula.Items.Add(dr["Cedula"].ToString());
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
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Vehiculos WHERE Cliente_Cedula=@cedula", conn))
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
                                idVehiculo.Add(dr["ID"].ToString());
                                if (combo != null)
                                {
                                    combo.Items.Add(dr["Marca"].ToString() + " - " + dr["Modelo"].ToString());
                                }
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
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Vehiculos WHERE ID=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                txtMarca.Text = dr["Marca"].ToString();
                                txtModelo.Text = dr["Modelo"].ToString();
                                txtPlaca.Text = dr["Placa"].ToString();
                                txtNotas.Text = dr["Notas"].ToString();

                                if (Convert.ToBoolean(dr["EsCamion"]) == true)
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

        private void EncontrarReservacion(string id)
        {
            listboxVehiculoHabitacion.Items.Clear();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT NumeroHabitacion, Vehiculo_ID FROM Reservaciones WHERE Vehiculo_ID=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                lblVehiculoHabitacion.Visible = true;
                                panelListboxVehiculoHabitacion.Visible = true;
                                listboxVehiculoHabitacion.Visible = true;

                                while (dr.Read())
                                {
                                    listboxVehiculoHabitacion.Items.Add(dr["NumeroHabitacion"].ToString());

                                    //if (dr["ID_Vehiculo"] != DBNull.Value)
                                    //{

                                    //}
                                }
                            }
                            else
                            {
                                lblVehiculoHabitacion.Visible = false;
                                panelListboxVehiculoHabitacion.Visible = false;
                                listboxVehiculoHabitacion.Visible = false;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha encontrado un problema.\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ModificarVehiculo(string id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Vehiculos SET EsCamion=@esCamion, Marca=@marca, Modelo=@modelo, Placa=@placa, Notas=@notas WHERE ID=@id", conn))
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
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un error.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void EliminarVehiculo(string id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Vehiculos WHERE ID=@id", conn))
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
            EncontrarReservacion(idVehiculo[comboVehiculo.SelectedIndex].ToString());
            //MessageBox.Show(idVehiculo[comboVehiculos.SelectedIndex].ToString());

            btnEliminar.Visible = true;
            btnModificar.Visible = true;
            //}
        }

        private void comboCedula_SelectedIndexChanged(object sender, EventArgs e)
        {
            RestaurarCampos();
            if (!lblCliente.Visible)
            {
                lblCliente.Visible = true;
                lblNombreCliente.Visible = true;
            }

            lblNombreCliente.Text = nombreCliente[comboCedula.SelectedIndex];
            
            CargarVehiculosPorCedula(comboCedula.Text, comboVehiculo);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMarca.Text.Trim()) && String.IsNullOrEmpty(txtModelo.Text.Trim()))
            {
                MessageBox.Show("Los campos de texto no pueden estar vacíos.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMarca.Select();
            }
            else
            {
                if (ModificarVehiculo(idVehiculo[comboVehiculo.SelectedIndex].ToString()))
                {
                    if (((ListaVehiculos)Application.OpenForms["ListaVehiculos"]) != null)
                    {
                        ListaVehiculos vehiculos = (ListaVehiculos)Application.OpenForms["ListaVehiculos"];
                        vehiculos.CargarVehiculos(false);
                    }

                    this.Close();
                }
            }
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

                    lblCliente.Visible = false;
                    lblNombreCliente.Visible = false;

                    if (listboxVehiculoHabitacion.Visible)
                    {
                        lblVehiculoHabitacion.Visible = false;
                        panelListboxVehiculoHabitacion.Visible = false;
                        listboxVehiculoHabitacion.Visible = false;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listboxVehiculoHabitacion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            f1.ActivarTimerEspera();

            Reservacion reservacion = new Reservacion();

            reservacion.CargarReservacion(int.Parse(listboxVehiculoHabitacion.Text), "ocupada");
            reservacion.ShowDialog();
        }
    }
}
