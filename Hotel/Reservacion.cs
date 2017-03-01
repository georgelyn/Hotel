﻿using System;
using System.Windows.Forms;
using System.Transactions;
//using System.Globalization;
//using System.Threading;
using System.Data.SQLite;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Text;

namespace Hotel
{
    public partial class Reservacion : Form
    {
        public Reservacion()
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
            CargarTipoHabitacion();

            PanelCedula(true);
            //panelContenedor.Visible = false;
        }

        /* 
        ** VARIABLES
        */

        double total; // El total a pagar
        bool vehiculoAlmacenado = false; // Se evalúa al cargar reservación (si tiene vehículo agregado). Útil para query de modificar reservación
        int habitacionActual = 0; // Toma su valor en CargarHuespedPorHabitacion. Útil para cambiar habitación en Modificar Reservación

        /* 
        ** METODOS
        */

        public void CargarNumHabitaciones(string estado)
        {
            // habitacionActual = Si se desea incluir en el combobox la habitación actual. Útil si se quiere modificar reservación

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
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT tipo FROM tipo_habitacion", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    listboxHabitaciones.Items.Add(dr["tipo"].ToString());
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

        public void CargarHuespedPorHabitacion(int numero_hab, string estado)
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

                CargarNumHabitaciones("disponible");
                comboHabitacion.Items.Add(habitacionActual);
                comboHabitacion.Text = numero_hab.ToString();

                PanelCedula(false);
                txtCedula.Enabled = false;

                btnModificar.Location = new Point(704, 10);
                btnModificar.Visible = true;
                btnAceptar.Visible = false;

                this.Text = "Modificar Reservación";
            }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    //using (SQLiteCommand cmd = new SQLiteCommand("SELECT c.nombre, c.apellido, c.cedula, edad, telefono, telefono2, " +
                    //    " r.numero_hab, r.fecha_ingreso, fecha_salida FROM cliente c, reservacion r ON cedula = r.cedula_cliente " +
                    //    " INNER JOIN habitacion h on h.numero_hab = r.numero_hab WHERE h.numero_hab = '" + numero_hab + "' ", conn))
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT nombre, cedula, edad, telefono, telefono2, " + 
                        " r.id, r.numero_hab, fecha_ingreso, fecha_salida, tipo_habitacion, costo_total, marca, modelo, placa, es_camion " + 
                        " FROM cliente INNER JOIN reservacion r ON cedula = r.cedula_cliente " + 
                        " LEFT JOIN vehiculo v ON r.id = v.reservacion_id "+ 
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

                                if (dr["es_camion"] != DBNull.Value)
                                {
                                    if (Convert.ToBoolean(dr["es_camion"]) == true)
                                        checkCamion.Checked = true;

                                    vehiculoAlmacenado = true;
                                    //MessageBox.Show("No es null");
                                }

                                txtMarca.Text = dr["marca"].ToString();
                                txtModelo.Text = dr["modelo"].ToString();
                                txtPlaca.Text = dr["placa"].ToString();

                                dtEntrada.Value = Convert.ToDateTime(dr["fecha_ingreso"].ToString());
                                dtSalida.Value = Convert.ToDateTime(dr["fecha_salida"].ToString());

                                listboxHabitaciones.Text = dr["tipo_habitacion"].ToString();

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
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM cliente WHERE cedula ='" + cedula + "'", conn))
                    //"SELECT cliente.*, modelo, marca, es_camion, placa FROM cliente INNER JOIN vehiculo on cedula = cedula_cliente WHERE cedula ='" + cedula + "'", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                txtNombre.Text = dr["nombre"].ToString();
                                txtCedula.Text = cedula; // Hasta ahora no es realmente necesario. Se carga en btnCheckCedula
                                txtEdad.Text = dr["edad"].ToString();
                                txtTelefono1.Text = dr["telefono"].ToString();
                                txtTelefono2.Text = dr["telefono2"].ToString();

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
            // Estado por default // if (chequear)

            panelCedula.Visible = true;
            lblCedula.Location = new Point(50, 40);
            panelCedula.Controls.Add(lblCedula);
            txtCedula.Location = new Point(135, 37);
            panelCedula.Controls.Add(txtCedula);

            txtCedula.Select();
            btnAceptar.Visible = false;

            if (!visible)
            {
                lblCedula.Location = new Point(91, 179);
                txtCedula.Location = new Point(184, 176);
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
                    panelCedula.Visible = false;
                    c.Visible = true;
                }
            }
        }

        public void NuevaReservacion(bool clienteNuevo)
        {
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
                        query = "INSERT INTO cliente (nombre, cedula, edad, telefono, telefono2, cliente_desde) VALUES (@nombre, @cedula, @edad, @telefono1, @telefono2, @clienteDesde); " +
                            "UPDATE habitacion SET estado=@estado WHERE numero_hab=@numeroHabitacion;" +
                            "INSERT INTO reservacion (numero_hab, cedula_cliente, fecha_ingreso, fecha_salida, tipo_habitacion, costo_total) VALUES (@numeroHabitacion, @cedula, @fechaIngreso, @fechaSalida, @tipoHabitacion, @costoTotal)";
                    }
                    else if (!clienteNuevo)
                    {
                        query = "UPDATE cliente SET nombre=@nombre, edad=@edad, telefono=@telefono1, telefono2=@telefono2 WHERE cedula=@cedula;" +
                           "UPDATE habitacion SET estado=@estado WHERE numero_hab=@numeroHabitacion;" +
                           "INSERT INTO reservacion (numero_hab, cedula_cliente, fecha_ingreso, fecha_salida, tipo_habitacion, costo_total) VALUES (@numeroHabitacion, @cedula, @fechaIngreso, @fechaSalida, @tipoHabitacion, @costoTotal)";
                    }

                    if (conVehiculo) // Sólo almacenar vehículo si los campos no están vacíos
                    {
                        query += "; INSERT INTO vehiculo (cedula_cliente, reservacion_id, es_camion, marca, modelo, placa) VALUES (@cedula, (SELECT id FROM reservacion WHERE numero_hab = @numeroHabitacion), @camion, @marca, @modelo, @placa)";
                    }

                    using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                    {                      
                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            #region Clientes

                            cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
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
                            cmd.Parameters.AddWithValue("@fechaIngreso", dtEntrada.Value);//.ToString("dd-MM-yyyy h:mm tt", CultureInfo.InvariantCulture));
                            cmd.Parameters.AddWithValue("@fechaSalida", dtSalida.Value);//.ToString("yyyy-MM-dd h:mm tt", CultureInfo.InvariantCulture));
                            cmd.Parameters.AddWithValue("@tipoHabitacion", listboxHabitaciones.Text);
                            cmd.Parameters.AddWithValue("@costoTotal", txtTotal.Text.Trim().Replace(".", "").Replace(",", ""));

                            #endregion

                            #region Vehiculo

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

        public void ModificarReservacion()
        {               
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

                    string query = "UPDATE cliente SET nombre=@nombre, edad=@edad, telefono=@telefono1, telefono2=@telefono2 WHERE cedula=@cedula;" +
                        "UPDATE reservacion SET numero_hab=@numeroHabitacion, fecha_ingreso=@fechaIngreso, fecha_salida=@fechaSalida, tipo_habitacion=@tipoHabitacion, costo_total=@costoTotal WHERE id=(SELECT id FROM reservacion WHERE numero_hab=@habitacionActual)";

                    if (habitacionActual != (comboHabitacion.SelectedIndex + 1)) // En pocas palabras, si se cambió número habtiación
                    {
                        query += "; UPDATE habitacion SET estado='disponible' WHERE numero_hab ='" + habitacionActual + "';" +
                            "UPDATE habitacion SET estado='ocupada' WHERE numero_hab='" + comboHabitacion.Text + "'";
                    }

                    if (conVehiculo)
                    {
                        if (vehiculoAlmacenado)
                        {
                            query += "; UPDATE vehiculo SET es_camion=@camion, marca=@marca, modelo=@modelo, placa=@placa WHERE reservacion_id=(SELECT id FROM reservacion WHERE numero_hab=@numeroHabitacion)";
                        }
                        else
                        {
                            query += "; INSERT INTO vehiculo (cedula_cliente, reservacion_id, es_camion, marca, modelo, placa) VALUES (@cedula, (SELECT id FROM reservacion WHERE numero_hab = @numeroHabitacion), @camion, @marca, @modelo, @placa)";
                        }
                    }


                    using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            // CLIENTE

                            cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                            cmd.Parameters.AddWithValue("@cedula", txtCedula.Text.Replace(".", "").Trim());
                            cmd.Parameters.AddWithValue("@edad", StringExtensions.NullString(txtEdad.Text.Trim()));
                            cmd.Parameters.AddWithValue("@telefono1", StringExtensions.NullString(txtTelefono1.Text.Trim()));
                            cmd.Parameters.AddWithValue("@telefono2", StringExtensions.NullString(txtTelefono2.Text.Trim()));

                            // RESERVACION

                            cmd.Parameters.AddWithValue("@numeroHabitacion", comboHabitacion.Text);
                            cmd.Parameters.AddWithValue("@habitacionActual", habitacionActual);
                            cmd.Parameters.AddWithValue("@fechaIngreso", dtEntrada.Value);
                            cmd.Parameters.AddWithValue("@fechaSalida", dtSalida.Value);
                            cmd.Parameters.AddWithValue("@tipoHabitacion", listboxHabitaciones.Text);
                            cmd.Parameters.AddWithValue("@costoTotal", txtTotal.Text.Trim().Replace(".", "").Replace(",", ""));

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
                            cmd.ExecuteNonQuery();
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
        }

        public bool BuscarPorCedula(string cedula)
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
            if (ValidacionCamposTexto()) // Si la validación se realizó efectivamente
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
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT tipo, costo FROM tipo_habitacion WHERE tipo=@tipo", conn))
                    {
                        cmd.Parameters.AddWithValue("tipo", listboxHabitaciones.Text);
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    total = Convert.ToDouble(dr["costo"].ToString());
                                    //if (checkCamion.Checked)
                                    //    total += 5000;
                                    txtTotal.Text = total.ToString();
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

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13 && !char.IsPunctuation(e.KeyChar)) // 8 = backspace, 13 = enter
            //{
            //    //MessageBox.Show("Carácter inválido.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    e.Handled = true;
            //}
        }

        private void txtTelefono1_Leave(object sender, EventArgs e)
        {
            //long getphn = Convert.ToInt64(txtTelefono1.Text);
            //string formatString = String.Format("{0:0000-000-0000}", getphn);
            //txtTelefono1.Text = formatString;
        }

        private void checkCamion_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("No he implementado el incremento del monto total");

            // El problema de hacerlo así es que el valor por camión no se podrá cambiar fácilmente

            //if (checkCamion.Checked)
            //{
            //    total += 5000;
            //}
            //else
            //{
            //    total -= 5000;
            //}

            //txtTotal.Text = total.ToString();
        }

        #endregion

        private void btnCheckCedula_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCedula.Text.Trim()))
            {
                txtCedula.Text = txtCedula.Text.Replace(".", "").Trim();
                if (BuscarPorCedula(txtCedula.Text))//.Replace(".", ""))) // Si existe el número de cédula (cliente)
                {
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
                ModificarReservacion();
            }
        }

        private void linkLblCambiarNumHab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!comboHabitacion.Enabled)
            {
                comboHabitacion.Enabled = true;
            }

            if (!lblHabitacionActual.Visible && !lblHabitacionNumero.Visible)
            {
                lblHabitacionActual.Visible = true;
                lblHabitacionNumero.Visible = true;
                lblHabitacionNumero.Text = habitacionActual.ToString();
            }
        }
    }
}