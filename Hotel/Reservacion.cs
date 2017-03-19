using System;
using System.Windows.Forms;
using System.Transactions;
using System.Data.SQLite;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Text;
using System.Configuration;
using System.Collections.Generic;
using System.Globalization;

namespace Hotel
{
    public partial class Reservacion : Form
    {
        public Reservacion()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("es-VE");
            InitializeComponent();

            /*dtEntrada.Value = DateTime.UtcNow; */// Ocurría un error son esto... 
            // A las 12 am, el ValueChanged del dtSalida tenía un desajuste en la fecha, como si fuera un día más.
            // Usando TimeSpan en lugar de TotalDays parece solucionarlo... pero antes me daba la cuenta mal... Seguir probando
            // Volví a agregarlo... ya que antes de las 12 no cargaba los montos al cambiar tipo habitación. Seguir probando...
            // ^ Seguía pasando. Ver *

            // Un día después de fecha actual, con hora 2:00 pm
            //DateTime fechaSalida = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0);
            //fechaSalida = fechaSalida.AddDays(1);
            //dtSalida.Value = fechaSalida;

            // * Mejor solución, olvidando lo de las 2:00 pm

            dtEntrada.Value = DateTime.Now;
            dtSalida.Value = dtEntrada.Value.AddDays(1);

            // Llamada a métodos
            CargarNumHabitaciones("disponible");
            CargarTipoHabitacion();
            PanelCedula(true);
        }

        Form1 f1 = (Form1)Application.OpenForms["Form1"];
        Msg msg;
        Vehiculo vehiculo;

        /* 
        ** VARIABLES
        */

        double total; // El total a pagar
        double montoCamion = double.Parse(ConfigurationManager.AppSettings["MontoCamion"]);

        bool vehiculoAlmacenado = false; // Se evalúa al cargar reservación (si tiene vehículo agregado). Útil para query de modificar reservación
        int habitacionActual = 0; // Toma su valor en CargarHuespedPorHabitacion. Útil para cambiar habitación en Modificar Reservación

        List<String> idVehiculo; // Para almacenar los IDs de cada vehículo del cliente
        int numeroVehiculos = 0; // Útil para llevar cuenta de cuántos vehículos tiene registrado el cliente.

        int diasReservados = 1;

        /* 
        ** METODOS
        */

        public void CargarNumHabitaciones(string estado)
        {
            // habitacionActual = Si se desea incluir en el combobox la habitación actual. Útil si se quiere modificar reservación

            comboHabitacion.Items.Clear();

            string query = "SELECT NumeroHabitacion FROM Habitaciones";

            if (estado == "disponible" || estado == "ocupada")
            {
                query = "SELECT NumeroHabitacion FROM Habitaciones WHERE Estado ='" + estado + "' ";
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
                                comboHabitacion.Items.Add(dr["NumeroHabitacion"]);
                            }
                        }
                    }
                }

                comboHabitacion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarTipoHabitacion()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT Tipo FROM TipoHabitacion WHERE Activa='1' ORDER BY Costo", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    listboxHabitaciones.Items.Add(dr["Tipo"].ToString());
                                }
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            listboxHabitaciones.SelectedIndex = 0;
        }

        public void CargarReservacion(int numero_hab, string estado)
        {
            PanelCedula(true);

            //CargarNumHabitaciones("disponible");
            //CargarNumHabitaciones("todas"); // Cargar todas las habitaciones
            //comboHabitacion.SelectedIndex = (numero_hab - 1);
            comboHabitacion.Text = numero_hab.ToString();

            if (estado == "ocupada")
            {
                linkLblCambiarNumHab.Visible = true;
                comboHabitacion.Enabled = false;
                habitacionActual = numero_hab;

                //CargarNumHabitaciones("disponible"); // No es necesario. Ya se carga al iniciar el formulario
                comboHabitacion.Items.Add(habitacionActual); // Agregar tambbién la habitación actual
                comboHabitacion.Text = numero_hab.ToString();

                PanelCedula(false);
                txtCedula.Enabled = false;

                btnModificar.Location = new Point(704, 10);
                btnModificar.Visible = true;
                btnAceptar.Visible = false;

                btnEliminar.Visible = true;

                this.Text = "Modificar Reservación";
            }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT Nombre, Cedula, Edad, Telefono, TelefonoExtra, " + 
                        " r.ID, r.NumeroHabitacion, r.Notas, CiudadOrigen, CiudadDestino, FechaIngreso, FechaSalida, TipoHabitacion, CostoTotal, Marca, Modelo, Placa, EsCamion " + 
                        " FROM Clientes INNER JOIN Reservaciones r ON Cedula = r.Cliente_Cedula" + 
                        " LEFT JOIN Vehiculos v ON r.Vehiculo_ID = v.ID "+ 
                        " WHERE r.NumeroHabitacion='" + numero_hab + "'", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtNombre.Text = dr["Nombre"].ToString();
                                txtCedula.Text = dr["Cedula"].ToString();
                                txtEdad.Text = dr["Edad"].ToString();
                                txtTelefono1.Text = dr["Telefono"].ToString();
                                txtTelefono2.Text = dr["TelefonoExtra"].ToString();

                                if (dr["EsCamion"] != DBNull.Value)
                                {
                                    if (Convert.ToBoolean(dr["EsCamion"]) == true)
                                        checkCamion.Checked = true;

                                    vehiculoAlmacenado = true;
                                    //MessageBox.Show("No es null");
                                }

                                txtMarca.Text = dr["Marca"].ToString();
                                txtModelo.Text = dr["Modelo"].ToString();
                                txtPlaca.Text = dr["Placa"].ToString();

                                txtOrigen.Text = dr["CiudadOrigen"].ToString();
                                txtDestino.Text = dr["CiudadDestino"].ToString();

                                dtEntrada.Value = Convert.ToDateTime(dr["FechaIngreso"].ToString());
                                dtSalida.Value = Convert.ToDateTime(dr["FechaSalida"].ToString());

                                txtNotas.Text = dr["Notas"].ToString();

                                if (listboxHabitaciones.Items.Contains(dr["TipoHabitacion"].ToString()))
                                {
                                    listboxHabitaciones.Text = dr["TipoHabitacion"].ToString();
                                }
                                else
                                {
                                    lblX.Visible = true;
                                    txtTipoHabitacion.Visible = true;
                                    txtTipoHabitacion.Text = dr["TipoHabitacion"].ToString();
                                    listboxHabitaciones.SelectedIndex = -1;
                                }

                                //txtTotal.Text = dr["CostoTotal"].ToString();
                                txtTotal.Text = string.Format(new CultureInfo("es-VE"), "{0:#,##0.00}", dr["CostoTotal"]);

                                total = double.Parse(dr["CostoTotal"].ToString());
                                    //total = double.Parse(txtTotal.Text);

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
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Clientes WHERE Cedula ='" + cedula + "'", conn))
                    //"SELECT cliente.*, modelo, marca, es_camion, placa FROM cliente INNER JOIN vehiculo on cedula = cedula_cliente WHERE cedula ='" + cedula + "'", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                txtNombre.Text = dr["Nombre"].ToString();
                                txtCedula.Text = cedula; // Hasta ahora no es realmente necesario. Se carga en btnCheckCedula
                                txtEdad.Text = dr["Edad"].ToString();
                                txtTelefono1.Text = dr["Telefono"].ToString();
                                txtTelefono2.Text = dr["TelefonoExtra"].ToString();

                                ////// Implementar de otra manera. Permitiendo elegir entre varios vehículos

                                //if (Convert.ToBoolean(dr["es_camion"]) == true)
                                //    checkCamion.Checked = true;

                                //txtMarca.Text = dr["marca"].ToString();
                                //txtModelo.Text = dr["modelo"].ToString();
                                //txtPlaca.Text = dr["placa"].ToString();

                            }
                        }
                    }
                }
            } catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PanelCedula(bool visible)
        {
            // Estado por default // if (visible)

            panelCedula.Visible = true;
            lblCedula.Location = new Point(13, 41);
            panelCedula.Controls.Add(lblCedula);
            txtCedula.Location = new Point(83, 38);
            panelCedula.Controls.Add(txtCedula);

            txtCedula.Select();
            btnAceptar.Visible = false;

            if (!visible)
            {
                lblCedula.Location = new Point(49, 172);
                txtCedula.Location = new Point(119, 169);
                panelContenedor.Controls.Add(lblCedula);
                panelContenedor.Controls.Add(txtCedula);

                btnAceptar.Visible = true;
            }

            foreach (Control c in panelContenedor.Controls)
            {
                if (visible)
                {
                    if (c.Name != "panelCedula")
                    // if (!(c.Name == "lblCedula" || c.Name == "txtCedula" || c.Name == "btnCheckCedula"))
                    {
                        c.Visible = false;
                    }
                }
                else // Ya se evaluó la cédula
                {
                    if (c.Name != "txtTipoHabitacion" && c.Name != "lblX") // Excepto lo relacionado con campos si no se encuentra el Tipo de Habitacion cargado
                    {
                        panelCedula.Visible = false;
                        c.Visible = true;
                    }
                }
            }
        }

        public void NuevaReservacion(bool clienteNuevo)
        {
            int x = 0; // Controla que se llame al método ActualizarColores (Form1) si se completó la transacción

            using (TransactionScope transactionScope = new TransactionScope()) // Si falla un insert, se anulan todos los cambio
            {
                try
                {
                    string query = "";
                    bool conVehiculo = true;

                    if ((!checkCamion.Checked) && (String.IsNullOrEmpty(txtMarca.Text.Trim()))
                         && (String.IsNullOrEmpty(txtModelo.Text.Trim())) && (String.IsNullOrEmpty(txtPlaca.Text.Trim())))
                    {
                        conVehiculo = false; // Si campos están vacíos, no tiene vehículo
                    }

                    if (clienteNuevo)
                    {
                        query = "INSERT INTO Clientes (Nombre, Cedula, Edad, Telefono, TelefonoExtra, ClienteDesde) VALUES (@nombre, @cedula, @edad, @telefono1, @telefono2, @clienteDesde); " +
                            "UPDATE Habitaciones SET Estado=@estado WHERE NumeroHabitacion=@numeroHabitacion;" +
                            "INSERT INTO Reservaciones (NumeroHabitacion, Cliente_Cedula, Vehiculo_ID, CiudadOrigen, CiudadDestino, FechaIngreso, FechaSalida, TipoHabitacion, CostoTotal, Notas) VALUES (@numeroHabitacion, @cedula, null, @ciudadOrigen, @ciudadDestino, @fechaIngreso, @fechaSalida, @tipoHabitacion, @costoTotal, @notasReservacion)";
                    }
                    else if (!clienteNuevo)
                    {
                        query = "UPDATE Clientes SET Nombre=@nombre, Edad=@edad, Telefono=@telefono1, TelefonoExtra=@telefono2 WHERE Cedula=@cedula;" +
                           "UPDATE Habitaciones SET Estado=@estado WHERE NumeroHabitacion=@numeroHabitacion;" +
                           "INSERT INTO Reservaciones (NumeroHabitacion, Cliente_Cedula, Vehiculo_ID, CiudadOrigen, CiudadDestino, FechaIngreso, FechaSalida, TipoHabitacion, CostoTotal, Notas) VALUES (@numeroHabitacion, @cedula, null, @ciudadOrigen, @ciudadDestino, @fechaIngreso, @fechaSalida, @tipoHabitacion, @costoTotal, @notasReservacion)";
                    }

                    if (conVehiculo) // Sólo almacenar vehículo si los campos no están vacíos
                    {
                        if (vehiculoAlmacenado) // Si el vehículo ya está almacenado
                        {
                            query += "; UPDATE Vehiculos SET EsCamion=@camion, marca=@marca, modelo=@modelo, placa=@placa WHERE ID=@id; UPDATE Reservaciones SET Vehiculo_ID=@id WHERE NumeroHabitacion=@numeroHabitacion";
                        }
                        else
                        {
                            query += "; INSERT INTO Vehiculos (Cliente_Cedula, EsCamion, Marca, Modelo, Placa) VALUES (@cedula, @camion, @marca, @modelo, @placa); UPDATE Reservaciones SET Vehiculo_ID=last_insert_rowid() WHERE NumeroHabitacion=@numeroHabitacion";
                        }
                    }

                    using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                    {                      
                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            #region Clientes

                            cmd.Parameters.AddWithValue("@nombre", StringExtensions.FirstLetterToUpper(txtNombre.Text.Trim()));
                            cmd.Parameters.AddWithValue("@cedula", txtCedula.Text.Replace(".", "").Trim());
                            cmd.Parameters.AddWithValue("@edad", StringExtensions.NullString(txtEdad.Text.Trim()));
                            cmd.Parameters.AddWithValue("@telefono1", StringExtensions.NullString(txtTelefono1.Text.Trim()));
                            cmd.Parameters.AddWithValue("@telefono2", StringExtensions.NullString(txtTelefono2.Text.Trim()));

                            if (clienteNuevo)
                            {
                                cmd.Parameters.AddWithValue("@clienteDesde", dtEntrada.Value);
                            }

                            #endregion

                            #region Habitacion

                            cmd.Parameters.AddWithValue("@estado", "ocupada");

                            #endregion

                            #region Reservacion

                            cmd.Parameters.AddWithValue("@numeroHabitacion", comboHabitacion.Text);
                            //cmd.Parameters.AddWithValue("@cedula_cliente", txtCedula.Text.Trim().Replace(".", ""));
                            cmd.Parameters.AddWithValue("@ciudadOrigen", StringExtensions.NullString(txtOrigen.Text.Trim()));
                            cmd.Parameters.AddWithValue("@ciudadDestino", StringExtensions.NullString(txtDestino.Text.Trim()));
                            cmd.Parameters.AddWithValue("@fechaIngreso", dtEntrada.Value);//.ToString("dd-MM-yyyy h:mm tt", CultureInfo.InvariantCulture));
                            cmd.Parameters.AddWithValue("@fechaSalida", dtSalida.Value);//.ToString("yyyy-MM-dd h:mm tt", CultureInfo.InvariantCulture));
                            cmd.Parameters.AddWithValue("@tipoHabitacion", listboxHabitaciones.Text);
                            cmd.Parameters.AddWithValue("@costoTotal", txtTotal.Text.Trim().Replace(".", "").Replace(",", "."));
                            cmd.Parameters.AddWithValue("@notasReservacion", StringExtensions.NullString(txtNotas.Text.Trim()));

                            #endregion

                            #region Vehiculo

                            if (conVehiculo)
                            {
                                //cmd.Parameters.AddWithValue("@existe", false);  // El vehículo no existe, es nuevo.        
                                
                                if (vehiculoAlmacenado)
                                {
                                    cmd.Parameters.AddWithValue("@id", idVehiculo[comboVehiculo.SelectedIndex].ToString());
                                }

                                bool esCamion = false;

                                if (checkCamion.Checked)
                                    esCamion = true;

                                cmd.Parameters.AddWithValue("@camion", esCamion);
                                cmd.Parameters.AddWithValue("@marca", StringExtensions.NullString(txtMarca.Text.Trim()));
                                cmd.Parameters.AddWithValue("@modelo", StringExtensions.NullString(txtModelo.Text.Trim()));
                                cmd.Parameters.AddWithValue("@placa", StringExtensions.NullString(txtPlaca.Text.Trim()));

                            }
                           
                            //cmd.Parameters.AddWithValue("@marca", txtMarca.Text.Trim().NullString());
                            //cmd.Parameters.AddWithValue("@modelo", txtModelo.Text.Trim().NullString());
                            //cmd.Parameters.AddWithValue("@placa", txtPlaca.Text.Trim().NullString());

                            #endregion

                            conn.Open();
                            x = cmd.ExecuteNonQuery();
                            transactionScope.Complete();

                            MessageBox.Show("Datos ingresados correctamente.");

                            Close();

                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    if (ex.Message.Contains("cedula")) // Ya esto no es necesario, pues se verifica la cédula antes de llamar al método
                        MessageBox.Show("El número de cédula ingresado ya existe en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("No se pudo conectar con la base de datos. \nDescripción del error: \n\n>> " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Se ha presentado un error.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (x > 0) // Se complettó la transacción
            {
                f1.ActualizarColores();
            }

        }

        public void ModificarReservacion()
        {
            int x = 0; // Controla que se llame al método ActualizarColores (Form1) si se completó la transacción

            using (TransactionScope transactionScope = new TransactionScope()) // Si falla un insert, se anulan todos los cambio
            {
                try
                {
                    bool conVehiculo = true;

                    if ((!checkCamion.Checked) && (String.IsNullOrEmpty(txtMarca.Text.Trim()))
                         && (String.IsNullOrEmpty(txtModelo.Text.Trim())) && (String.IsNullOrEmpty(txtPlaca.Text.Trim())))
                    {
                        conVehiculo = false; // Si campos están vacíos, no tiene vehículo
                    }

                    string query = "UPDATE Clientes SET Nombre=@nombre, Edad=@edad, Telefono=@telefono1, TelefonoExtra=@telefono2 WHERE Cedula=@cedula;" +
                        "UPDATE Reservaciones SET NumeroHabitacion=@numeroHabitacion, CiudadOrigen=@ciudadOrigen, CiudadDestino=@ciudadDestino, FechaIngreso=@fechaIngreso, FechaSalida=@fechaSalida, TipoHabitacion=@tipoHabitacion, CostoTotal=@costoTotal, Notas=@notasReservacion WHERE ID=(SELECT ID FROM Reservaciones WHERE NumeroHabitacion=@habitacionActual)";


                    if (habitacionActual != (comboHabitacion.SelectedIndex + 1)) // En pocas palabras, si se cambió número habtiación
                    {
                        query += "; UPDATE Habitaciones SET Estado='disponible' WHERE NumeroHabitacion='" + habitacionActual + "';" +
                            "UPDATE Habitaciones SET Estado='ocupada' WHERE NumeroHabitacion='" + comboHabitacion.Text + "'";
                    }

                    if (conVehiculo)
                    {
                        if (vehiculoAlmacenado)
                        {
                            query += "; UPDATE Vehiculos SET EsCamion=@camion, Marca=@marca, Modelo=@modelo, Placa=@placa WHERE ID=(SELECT Vehiculo_ID FROM Reservaciones WHERE NumeroHabitacion=@numeroHabitacion)";
                        }
                        else
                        {
                            query += "; INSERT INTO Vehiculos (Cliente_Cedula, EsCamion, Marca, Modelo, Placa) VALUES (@cedula, @camion, @marca, @modelo, @placa);" +
                                "UPDATE Reservaciones SET Vehiculo_ID=last_insert_rowid() WHERE NumeroHabitacion=@numeroHabitacion";
                        }
                    }
                    else
                    {
                        query += "; UPDATE Reservaciones SET Vehiculo_ID=null WHERE NumeroHabitacion=@numeroHabitacion";
                    }


                    using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            // CLIENTE

                            cmd.Parameters.AddWithValue("@nombre", StringExtensions.FirstLetterToUpper(txtNombre.Text.Trim()));
                            cmd.Parameters.AddWithValue("@cedula", txtCedula.Text.Replace(".", "").Trim());
                            cmd.Parameters.AddWithValue("@edad", StringExtensions.NullString(txtEdad.Text.Trim()));
                            cmd.Parameters.AddWithValue("@telefono1", StringExtensions.NullString(txtTelefono1.Text.Trim()));
                            cmd.Parameters.AddWithValue("@telefono2", StringExtensions.NullString(txtTelefono2.Text.Trim()));

                            // RESERVACION

                            cmd.Parameters.AddWithValue("@numeroHabitacion", comboHabitacion.Text);
                            cmd.Parameters.AddWithValue("@habitacionActual", habitacionActual);
                            cmd.Parameters.AddWithValue("@ciudadOrigen", StringExtensions.NullString(txtOrigen.Text.Trim()));
                            cmd.Parameters.AddWithValue("@ciudadDestino", StringExtensions.NullString(txtDestino.Text.Trim()));
                            cmd.Parameters.AddWithValue("@fechaIngreso", dtEntrada.Value);
                            cmd.Parameters.AddWithValue("@fechaSalida", dtSalida.Value);

                            if (listboxHabitaciones.SelectedIndex >= 0)
                            {
                                cmd.Parameters.AddWithValue("@tipoHabitacion", listboxHabitaciones.Text);
                            }
                            else // La habitación no se encuentra en el Listbox (porque fue eliminada o desactivada, etc)
                            {
                                cmd.Parameters.AddWithValue("@tipoHabitacion", txtTipoHabitacion.Text);                               
                            }

                            cmd.Parameters.AddWithValue("@costoTotal", txtTotal.Text.Trim().Replace(".", "").Replace(",", "."));
                            cmd.Parameters.AddWithValue("@notasReservacion", StringExtensions.NullString(txtNotas.Text.Trim()));

                            // VEHICULO

                            if (conVehiculo)
                            {
                                bool esCamion = false;

                                if (checkCamion.Checked)
                                    esCamion = true;

                                cmd.Parameters.AddWithValue("@camion", esCamion);
                                cmd.Parameters.AddWithValue("@marca", StringExtensions.NullString(txtMarca.Text.Trim()));
                                cmd.Parameters.AddWithValue("@modelo", StringExtensions.NullString(txtModelo.Text.Trim()));
                                cmd.Parameters.AddWithValue("@placa", StringExtensions.NullString(txtPlaca.Text.Trim()));

                            }

                            conn.Open();
                            x = cmd.ExecuteNonQuery();
                            transactionScope.Complete();

                            MessageBox.Show("Datos modificados correctamente.");

                            Close();
                        }
                    }

                } catch (Exception ex)
                {
                    MessageBox.Show("Se ha presentado un error.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (x > 0) // Se complettó la transacción
            {
                f1.ActualizarColores();
            }
        }

        public void EliminarReservacion()
        {
            string query = "DELETE FROM Reservaciones WHERE NumeroHabitacion=@numeroHabitacion;" +
                "UPDATE Habitaciones SET Estado='disponible' WHERE NumeroHabitacion=@numeroHabitacion";

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@numeroHabitacion", habitacionActual);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Datos eliminados correctamente.");

                        f1.ActualizarColores();

                        Close();
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un error.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool BuscarPorCedula(string cedula)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT Cedula FROM Clientes WHERE Cedula='" + cedula + "'", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                //MessageBox.Show("Existe");
                                //CargarDatosCliente(cedula);

                                return true; // Cliente existe

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

            return false; // CLiente no existe
        }

        private bool TieneVehiculo(string cedula)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT Cedula, EsCamion, Cliente_Cedula, COUNT(distinct v.ID) AS vehiculos FROM Clientes INNER JOIN Vehiculos v ON Cedula=Cliente_Cedula WHERE Cedula=@cedula", conn))
                    {
                        cmd.Parameters.AddWithValue("@cedula", cedula);
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    if (dr["EsCamion"] != DBNull.Value) // Si es_camion es NULL, o no existe, pues no tiene vehículo. En la base de datos es_camion siempre tiene un valor de 0 o 1
                                    {
                                        numeroVehiculos = int.Parse(dr["vehiculos"].ToString());
                                        return true; // Tiene vehículo
                                    }
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        public void CargarDatosVehiculo(string id)
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

        public bool ValidacionCamposTexto()
        {
            lblHab.ForeColor = Color.Black;

            if (comboHabitacion.SelectedIndex == -1)
            {
                MessageBox.Show(this, "No ha seleccionado el número de habitación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblHab.ForeColor = Color.Red;
                comboHabitacion.DroppedDown = true;
                return false;
            }

            TextBox[] txtBox = { txtNombre, txtCedula, txtTotal };
            Label[] txtLabel = { lblNombre, lblCedula, lblTotal };

            for (int i = 0; i < txtBox.Length; i++)
            {
                string text = txtBox[i].Text;
                txtLabel[i].ForeColor = Color.Black;

                if (String.IsNullOrEmpty(text.Trim()))
                {                   
                    MessageBox.Show(this, $"El campo de texto \"{txtLabel[i].Text.Replace(":", "")}\" no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBox[i].Clear();
                    txtBox[i].Select();
                    txtLabel[i].ForeColor = Color.Red;
                    return false; // No es válido
                }
            }
            return true; // Es valido, se puede proceder
        }

        /* 
        ** EVENTOS
        */

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidacionCamposTexto()) // Si la validación se realizó con éxito
            {
                if (BuscarPorCedula(txtCedula.Text.Replace(".", "").Trim())) // Si el cliente existe
                {
                    NuevaReservacion(false); // clienteNuevo = false
                }
                else // Es un cliente nuevo
                {
                    NuevaReservacion(true); // clienteNuevo = true
                }
            }
        }

        private void listboxHabitaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT Tipo, Costo FROM TipoHabitacion WHERE Tipo=@tipo", conn))
                    {
                        cmd.Parameters.AddWithValue("Tipo", listboxHabitaciones.Text);
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    total = Convert.ToDouble(dr["Costo"].ToString());
                                    if (checkCamion.Checked)
                                        total += montoCamion;
                                    //txtTotal.Text = total.ToString();
                                    total *= diasReservados;
                                    txtTotal.Text = string.Format(new CultureInfo("es-VE"), "{0:#,##0.00}", total);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region NO IMPLEMENTADOS TODAVIA

        private void txtTelefono1_Leave(object sender, EventArgs e)
        {
            //long getphn = Convert.ToInt64(txtTelefono1.Text);
            //string formatString = String.Format("{0:0000-000-0000}", getphn);
            //txtTelefono1.Text = formatString;
        }

        #endregion

        private void btnCheckCedula_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCedula.Text.Trim()))
            {
                txtCedula.Text = txtCedula.Text.Replace(".", "").Trim();
                if (BuscarPorCedula(txtCedula.Text))//.Replace(".", ""))) // Si existe el número de cédula (cliente)
                {
                    if (TieneVehiculo(txtCedula.Text))
                    {
                        comboVehiculo.Visible = true;
                        vehiculo = new Vehiculo();
                        idVehiculo = vehiculo.CargarVehiculosPorCedula(txtCedula.Text, comboVehiculo);
                    }

                    lblVehiculosAlmacenados.Visible = true;

                    if (numeroVehiculos == 0 || numeroVehiculos > 1)
                    {
                        lblVehiculosAlmacenados.Text = $"[{numeroVehiculos}] vehículos almacenados.";
                    }
                    else
                    {
                        lblVehiculosAlmacenados.Text = $"[{numeroVehiculos}] vehículo almacenado.";
                    }


                    CargarDatosCliente(txtCedula.Text);//.Replace(".", "").Trim());
                    //btnModificar.Location = new Point(704, 10);
                    //btnModificar.Visible = true;
                    //btnAceptar.Visible = false;

                    // No... Nada con botón Modificar. Más bien debo modificar la parte de ingreso para que sea update en lugar de insert
                }

                PanelCedula(false);
                txtCedula.Enabled = false;
                btnAceptar.Visible = true;

                lblCedula.ForeColor = Color.Black;

                return;
            }

            lblCedula.ForeColor = Color.Red;
            txtCedula.Select();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidacionCamposTexto())
            {
                msg = new Msg();

                msg.lblMsg.Text = "¿Está seguro de que desea modificar la reservación?";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes)
                    {
                        ModificarReservacion();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void linkLblCambiarNumHab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLblCambiarNumHab.Text == "Cambiar")
            {
                linkLblCambiarNumHab.Text = "Cancelar";
                comboHabitacion.Enabled = true;
                lblHabitacionActual.Visible = true;
                lblHabitacionNumero.Visible = true;
                lblHabitacionNumero.Text = habitacionActual.ToString();
            }
            else
            {
                linkLblCambiarNumHab.Text = "Cambiar";
                comboHabitacion.Enabled = false;
                lblHabitacionActual.Visible = false;
                lblHabitacionNumero.Visible = false;
                comboHabitacion.Text = habitacionActual.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            msg = new Msg();
            msg.lblMsg.Text = "¿Está seguro de que desea eliminar la reservación?";
            DialogResult dlgres = msg.ShowDialog();
            {
                if (dlgres == DialogResult.Yes)
                {
                    EliminarReservacion();
                }
                else
                {
                    return;
                }
            }
        }

        private void checkCamion_Click(object sender, EventArgs e)
        {
            if (checkCamion.Checked)
            {
                total += montoCamion;
            }
            else
            {
                total -= montoCamion;
            }

            //txtTotal.Text = total.ToString();
            txtTotal.Text = string.Format(new CultureInfo("es-VE"), "{0:#,##0.00}", total);
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCedula.Focused)
            {
                if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13) // 8 = backspace, 13 = enter
                {
                    e.Handled = true;
                }
            }

            if (txtEdad.Focused || txtTelefono1.Focused || txtTelefono2.Focused)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13) 
                {
                    e.Handled = true;
                }
            }

            if (txtTotal.Focused)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != '.' && e.KeyChar != ',')
                {
                    e.Handled = true;  
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //if (ValidacionCamposTexto())
            //{

            //}
            Close();
        }

        private void comboVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboVehiculo.SelectedIndex >= 0)
            {
                CargarDatosVehiculo(idVehiculo[comboVehiculo.SelectedIndex].ToString());

                if (!lblAvisoVehiculo.Visible)
                {
                    lblAvisoVehiculo.Visible = true;
                    linklblNuevoVehiculo.Visible = true;
                }

                vehiculoAlmacenado = true; // El vehículo se encuentra almacenado.
            }

            //MessageBox.Show(idVehiculo[comboVehiculo.SelectedIndex].ToString());
        }

        private void linklblNuevoVehiculo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            vehiculoAlmacenado = false; // Nuevo vehículo. No se encuentra almacenado.

            txtMarca.Text = "";
            txtModelo.Text = "";
            txtPlaca.Text = "";
            checkCamion.Checked = false;

            comboVehiculo.SelectedIndex = -1;
            txtMarca.Select();

            lblAvisoVehiculo.Visible = false;
            linklblNuevoVehiculo.Visible = false;
        }

        private void dtSalida_ValueChanged(object sender, EventArgs e)
        {
            ////if (dtSalida.Value.Month == dtEntrada.Value.Month)
            ////{
            ////    if (dtSalida.Value.Day > dtEntrada.Value.Day)
            ////    {
            ////        total /= diasReservados;
            ////        diasReservados = dtSalida.Value.Day - dtEntrada.Value.Day;

            ////        total *= diasReservados;
            ////        txtTotal.Text = String.Format(new CultureInfo("es-VE"), "{0:#,##0.00}", total);
            ////    }
            ////}

            if (dtSalida.Value.Day > dtEntrada.Value.Day)
            {
                if (checkCamion.Checked) // Feo... pero funciona
                {
                    total -= montoCamion; // Le quito el monto, para que no lo tome en cuenta al dividir
                }
                total /= diasReservados;

                TimeSpan ts = dtSalida.Value - dtEntrada.Value;
                diasReservados = ts.Days;
                //MessageBox.Show(ts.Days.ToString());

                //diasReservados = Convert.ToInt32((dtSalida.Value - dtEntrada.Value).TotalDays);

                total *= diasReservados;
                if (checkCamion.Checked) // ^ Ahora le vuelvo a agregar el cargo por camión
                {
                    total += montoCamion;
                }
                txtTotal.Text = String.Format(new CultureInfo("es-VE"), "{0:#,##0.00}", total);
            }
            else // Si trata de asignar una fecha igual o inferior al de entrada, no le dejo
            {
                dtSalida.Value = dtEntrada.Value.AddDays(1);
            }


        }
    }
}
