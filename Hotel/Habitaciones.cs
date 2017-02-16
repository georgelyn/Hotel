using System;
using System.Windows.Forms;
using System.Transactions;
//using System.Globalization;
//using System.Threading;
using System.Data.SQLite;

namespace Hotel
{
    public partial class Habitaciones : Form
    {
        public Habitaciones()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("es-VE");
            InitializeComponent();

            //dtSalida.Value = DateTime.Now.AddDays(1);
            // Un día después de fecha actual, con hora 2:00 pm
            DateTime fechaSalida = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0);
            fechaSalida = fechaSalida.AddDays(1);
            dtSalida.Value = fechaSalida;

            // Llamada a métodos
            CargarNumHabitaciones("disponible");
        }

        public void CargarNumHabitaciones(string estado)
        {
            comboHabitacion.Items.Clear();

            string query = "SELECT numero_hab FROM habitacion";

            if (estado == "disponible" || estado == "ocupada")
            {
                query = "SELECT numero_hab FROM habitacion WHERE estado ='" + estado + "' ";
            }


            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                comboHabitacion.Items.Add(dr["numero_hab"]);
                            }
                        }
                    }
                }

                comboHabitacion.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarHuespedPorHabitacion(int numero_hab)
        {
            CargarNumHabitaciones(null);
            comboHabitacion.SelectedIndex = (numero_hab - 1);

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    //using (SQLiteCommand cmd = new SQLiteCommand("SELECT c.nombre, c.apellido, c.cedula, edad, telefono, telefono2, " +
                    //    " r.numero_hab, r.fecha_ingreso, fecha_salida FROM cliente c, reservacion r ON cedula = r.cedula_cliente " +
                    //    " INNER JOIN habitacion h on h.numero_hab = r.numero_hab WHERE h.numero_hab = '" + numero_hab + "' ", conn))
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT nombre, cedula, edad, telefono, telefono2, " + 
                        " r.numero_hab, fecha_ingreso, fecha_salida, costo_total, marca, modelo, placa, es_camion " + 
                        " FROM cliente INNER JOIN reservacion r ON cedula = r.cedula_cliente " + 
                        " INNER JOIN vehiculo v ON cedula = v.cedula_cliente "+ 
                        " WHERE r.numero_hab ='" + numero_hab + "'", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtNombre.Text = dr["nombre"].ToString();
                                txtCedula.Text = dr["cedula"].ToString();
                                txtEdad.Text = dr["edad"].ToString();
                                txtTelefono1.Text = dr["telefono"].ToString();
                                txtTelefono2.Text = dr["telefono2"].ToString();

                                if (Convert.ToBoolean(dr["es_camion"]) == true)
                                    checkCamion.Checked = true;

                                txtMarca.Text = dr["marca"].ToString();
                                txtModelo.Text = dr["modelo"].ToString();
                                txtPlaca.Text = dr["placa"].ToString();

                                dtEntrada.Value = Convert.ToDateTime(dr["fecha_ingreso"].ToString());
                                dtSalida.Value = Convert.ToDateTime(dr["fecha_salida"].ToString());

                                txtTotal.Text = dr["costo_total"].ToString();
                                
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("No se pudo conectar con la base de datos. \nDescripción del error: \n\n>> " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se presentó un error.\n\n>> " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarDatosCliente(string cedula)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT cliente.*, modelo, marca, es_camion, placa FROM cliente INNER JOIN vehiculo on cedula = cedula_cliente WHERE cedula ='" + cedula + "'", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                txtNombre.Text = dr["nombre"].ToString();
                                txtCedula.Text = cedula;
                                txtEdad.Text = dr["edad"].ToString();
                                txtTelefono1.Text = dr["telefono"].ToString();
                                txtTelefono2.Text = dr["telefono2"].ToString();

                                if (Convert.ToBoolean(dr["es_camion"]) == true)
                                    checkCamion.Checked = true;

                                txtMarca.Text = dr["marca"].ToString();
                                txtModelo.Text = dr["modelo"].ToString();
                                txtPlaca.Text = dr["placa"].ToString();

                            }
                        }
                    }
                }
            } catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void IngresarHuesped()
        {
            using (TransactionScope transactionScope = new TransactionScope()) // Si falla un insert, se anulan todos los cambio
            {
                try
                {
                    string query = "";

                    using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                    {
                        query = "INSERT INTO cliente (nombre, cedula, edad, telefono, telefono2) VALUES (@nombre, @cedula, @edad, @telefono1, @telefono2); " +
                            "UPDATE habitacion SET estado=@estado WHERE numero_hab='" + comboHabitacion.Text + "';" +
                            "INSERT INTO reservacion (numero_hab, cedula_cliente, fecha_ingreso, fecha_salida, costo_total) VALUES (@numero_habitacion, @cedula, @fechaIngreso, @fechaSalida, @costoTotal); " +
                            "INSERT INTO vehiculo (cedula_cliente, es_camion, marca, modelo, placa) VALUES (@cedula, @camion, @marca, @modelo, @placa)";

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            #region Clientes

                            cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                            cmd.Parameters.AddWithValue("@cedula", txtCedula.Text.Replace(".", "").Trim());
                            cmd.Parameters.AddWithValue("@edad", StringExtensions.NullString(txtEdad.Text.Trim()));
                            cmd.Parameters.AddWithValue("@telefono1", StringExtensions.NullString(txtTelefono1.Text.Trim()));
                            cmd.Parameters.AddWithValue("@telefono2", StringExtensions.NullString(txtTelefono2.Text.Trim()));

                            #endregion

                            #region Habitacion

                            cmd.Parameters.AddWithValue("@estado", "ocupada");

                            #endregion

                            #region Reservacion

                            cmd.Parameters.AddWithValue("@numero_habitacion", comboHabitacion.Text);
                            //cmd.Parameters.AddWithValue("@cedula_cliente", txtCedula.Text.Trim().Replace(".", ""));
                            cmd.Parameters.AddWithValue("@fechaIngreso", dtEntrada.Value);//.ToString("dd-MM-yyyy h:mm tt", CultureInfo.InvariantCulture));
                            cmd.Parameters.AddWithValue("@fechaSalida", dtSalida.Value);//.ToString("yyyy-MM-dd h:mm tt", CultureInfo.InvariantCulture));
                            cmd.Parameters.AddWithValue("@costoTotal", txtTotal.Text.Trim().Replace(".", "").Replace(",", ""));

                            #endregion

                            #region Vehiculo

                            bool esCamion = false;

                            if (checkCamion.Checked)
                                esCamion = true;

                            cmd.Parameters.AddWithValue("@camion", esCamion);
                            cmd.Parameters.AddWithValue("@marca", StringExtensions.NullString(txtMarca.Text.Trim()));
                            cmd.Parameters.AddWithValue("@modelo", StringExtensions.NullString(txtModelo.Text.Trim()));
                            cmd.Parameters.AddWithValue("@placa", StringExtensions.NullString(txtPlaca.Text.Trim()));

                            //cmd.Parameters.AddWithValue("@marca", txtMarca.Text.Trim().NullString());
                            //cmd.Parameters.AddWithValue("@modelo", txtModelo.Text.Trim().NullString());
                            //cmd.Parameters.AddWithValue("@placa", txtPlaca.Text.Trim().NullString());

                            #endregion

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            transactionScope.Complete();

                            MessageBox.Show("Datos ingresados correctamente.");

                            Close();

                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    if (ex.Message.Contains("cedula"))
                        MessageBox.Show("El número de cédula ingresado ya existe en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("No se pudo conectar con la base de datos. \nDescripción del error: \n\n>> " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Se ha presentado un error.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public void BuscarPorCedula(string cedula)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT cedula FROM cliente WHERE cedula='" + cedula + "'", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                MessageBox.Show("Existe");
                                CargarDatosCliente(cedula);

                                //CargarHuesped(10);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            IngresarHuesped();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BuscarPorCedula(txtCedula.Text.Replace(".", ""));
        }
    }
}
