using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace Hotel
{
    class OperacionesSQLite
    {
        private static string carpetaRespaldos = ConfigurationManager.AppSettings["CarpetaRespaldos"];

        #region Query para crear la base de datos
        private static string queryCrearBaseDatos = @"CREATE TABLE IF NOT EXISTS [Clientes] (
                          [ID] INTEGER PRIMARY KEY AUTOINCREMENT,
                        [Nombre] VARCHAR (300) NOT NULL,
                        [Cedula] VARCHAR(100) NOT NULL UNIQUE,
                        [Edad] VARCHAR(10),
                        [Telefono] VARCHAR(50),
                        [TelefonoExtra] VARCHAR(50),
                        [ClienteDesde] DATE,
                        [Notas] VARCHAR(500)
                    );

                        CREATE TABLE IF NOT EXISTS [Habitaciones] (
                        [ID] INTEGER PRIMARY KEY AUTOINCREMENT,
                        [NumeroHabitacion] INTEGER UNIQUE,
                        [Estado] VARCHAR (50) NOT NULL
                    );

                        CREATE TABLE IF NOT EXISTS [Vehiculos] (
                        [ID] INTEGER  PRIMARY KEY AUTOINCREMENT,
                        [Cliente_Cedula] VARCHAR (100) REFERENCES Clientes (Cedula) ON DELETE CASCADE
                                                                                    ON UPDATE CASCADE,
                        [EsCamion] BOOLEAN,
                        [Marca] VARCHAR (300),
                        [Modelo] VARCHAR (200),
                        [Placa] VARCHAR (100),
                        [Notas] VARCHAR (300) 
                    );

                        CREATE TABLE IF NOT EXISTS [Reservaciones] (
                        [ID] INTEGER PRIMARY KEY AUTOINCREMENT,
                        [Cliente_Cedula] VARCHAR (100) REFERENCES Clientes(Cedula) ON DELETE CASCADE
                                                                                   ON UPDATE CASCADE,
                        [Vehiculo_ID] INTEGER REFERENCES Vehiculos(ID) ON DELETE SET NULL,
                        [NumeroHabitacion] INTEGER  REFERENCES Habitaciones(NumeroHabitacion) UNIQUE,
                        [FechaIngreso] DATE,
                        [FechaSalida] DATE,
                        [TipoHabitacion] VARCHAR(200),
                        [CiudadOrigen] VARCHAR(100),
                        [CiudadDestino] VARCHAR(100),
                        [CostoTotal] DECIMAL,
                        [Notas] VARCHAR(500)
                    );

                        CREATE TABLE IF NOT EXISTS [TipoHabitacion] (
                        [ID] INTEGER PRIMARY KEY AUTOINCREMENT,
                        [Tipo] VARCHAR (300),
                        [Descripcion] VARCHAR (300),
                        [Costo] DECIMAL,
                        [Notas] VARCHAR (500),
                        [Activa] BOOLEAN
                    );

                        CREATE TABLE IF NOT EXISTS [Seguridad] (
                        [ID] INTEGER PRIMARY KEY AUTOINCREMENT,
                        [User] VARCHAR,
                        [Password] VARCHAR
                    );

                        CREATE TABLE IF NOT EXISTS [Ajustes] (
                        [ID] INTEGER PRIMARY KEY AUTOINCREMENT,
                        [MontoCamion] DECIMAL DEFAULT (0),
                        [CarpetaRespaldos] VARCHAR (5000)
                    )";

        #endregion

        public static bool ProbarConexion()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'TipoHabitacion'", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows) // Si existe/no hay problemas
                            {
                                return true;
                            }
                        }

                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CrearBaseDeDatos()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    conn.Open();

                    using (SQLiteTransaction transaction = conn.BeginTransaction())
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(queryCrearBaseDatos, conn))
                        {
                            cmd.ExecuteNonQuery();

                            // Insertar los valores predeterminados/originales

                            for (int i = 1; i <= 52; i++)
                            {
                                cmd.CommandText = "INSERT INTO Habitaciones (NumeroHabitacion, Estado) VALUES (@numero, 'disponible')";
                                cmd.Parameters.AddWithValue("@numero", i);
                                cmd.ExecuteNonQuery();
                            }

                            cmd.CommandText = "INSERT INTO TipoHabitacion (Tipo, Costo, Activa) VALUES ('Matrimonial','15000', '1'), ('Doble', '16000', '1'), ('Triple', '18000', '1'), " +
                                "('Cuádruple', '20000', '1'), ('Séxtuple', '25000', '1'), ('Mini-Suite', '25000', '1')";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO Seguridad (User, Password) VALUES ('admin','ra0408'), ('usuario', '5093'), ('admin', 'emergencia')";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO Ajustes (MontoCamion, CarpetaRespaldos) VALUES (0, NULL)";
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true; // Se creó la base de datos
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\n\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false; // No se pudo crear la base de datos
        }

        //public static void ExisteBaseDeDatos()
        //{
        //    bool crearBaseDatos = true;

        //    using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
        //    {
        //        using (SQLiteCommand cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'Clientes'", conn))
        //        {
        //            conn.Open();

        //            using (SQLiteDataReader dr = cmd.ExecuteReader())
        //            {
        //                if (dr.HasRows) // Sí existe
        //                {
        //                    crearBaseDatos = false;
        //                }
        //            }

        //            if (crearBaseDatos)
        //            {
        //                cmd.CommandText = CrearBaseDatos.query;
        //                cmd.ExecuteNonQuery();
        //            }


        //        }
        //    }
        //}

        public static void Restablecer() // Sólo borrar los datos (excluyendo Habitaciones, TipoHabitacione y Seguridad)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Clientes; DELETE FROM SQLITE_SEQUENCE WHERE name='Clientes'; DELETE FROM Reservaciones; DELETE FROM SQLITE_SEQUENCE WHERE name='Reservaciones'; DELETE FROM Vehiculos; DELETE FROM SQLITE_SEQUENCE WHERE name='Vehiculos'; UPDATE Habitaciones SET Estado='disponible'", conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\n\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 

        //public static void CrearNuevaBaseDeDatos() // Crear la base de datos e insertar los datos necesarios en Habitaciones, TipoHabitacion y Seguridad
        //{
        //    try
        //    {
        //        SQLiteConnection.CreateFile("HotelCountry.db"); // Crear el archivo. No realmente necesario, ya que SQLite lo crear automáticamente si no lo encuentra

        //        using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=|DataDirectory|\HotelCountry.db;Version=3;New=True;Compress=True;Foreign Keys=ON"))
        //        {
        //            using (SQLiteCommand cmd = new SQLiteCommand(conn))
        //            {
        //                conn.Open();

        //                cmd.CommandText = CrearBaseDatos.query;
        //                cmd.ExecuteNonQuery();

        //                for (int i = 1; i <= 52; i++)
        //                {
        //                    cmd.CommandText = "INSERT INTO Habitaciones (NumeroHabitacion) VALUES (@numero)";
        //                    cmd.Parameters.AddWithValue("@numero", i);
        //                    cmd.ExecuteNonQuery();
        //                }



        //                cmd.CommandText = "INSERT INTO TipoHabitacion (Tipo, Costo, Activa) VALUES ('Matrimonial','15000', '1'), ('Doble', '16000', '1'), ('Triple', '18000', '1'), " +
        //                    "('Cuádruple', '20000', '1'), ('Séxtuple', '25000', '1'), ('Mini-Suite', '25000', '1')";
        //                cmd.ExecuteNonQuery();

        //                cmd.CommandText = "INSERT INTO Seguridad (User, Password) VALUES ('admin','ra0408'), ('usuario', '2506'), ('admin', 'emergencia')";
        //                cmd.ExecuteNonQuery();
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Se ha presentado un problema.\n\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        public static void Eliminar()
        {
            string filename = "";

            using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
            {
                conn.Open();
                if (File.Exists(conn.FileName))
                {
                    filename = conn.FileName;
                }
            }

            File.Delete(filename);
        }

        public static void HacerCopia()
        {
            if (String.IsNullOrEmpty(carpetaRespaldos))
            {
                carpetaRespaldos = Application.StartupPath;

                if (Directory.Exists(Application.StartupPath + "\\Respaldos"))
                {
                    carpetaRespaldos = Application.StartupPath + "\\Respaldos";
                }
            }

            string fechaActual = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string nombre = "HotelBD-" + fechaActual + ".db";

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = carpetaRespaldos; //Application.StartupPath + "\\Respaldos";
            saveFileDialog1.Filter = "Archivo de base de datos|*.db";
            saveFileDialog1.Title = "Guardar copia de seguridad";
            saveFileDialog1.FileName = nombre;

            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string query = @"Data Source=" + saveFileDialog1.FileName + ";Version=3;New=True;Compress=True;Foreign Keys=ON";

                try
                {
                    using (var original = new SQLiteConnection(ConexionBD.connstring))
                    using (var respaldo = new SQLiteConnection(query))
                    {
                        original.Open();
                        respaldo.Open();
                        original.BackupDatabase(respaldo, "main", "main", -1, null, 0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Se ha presentado un problema.\n\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static bool ConexionExitosa(string nuevaConexion) // Para evitar que se intente restaurar una base de datos no válida
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(nuevaConexion))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'TipoHabitacion'", conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("No es un archivo válido. Verifique e intente nuevanemente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not a database"))
                {
                    MessageBox.Show(new Form() { TopMost = true }, "El archivo de base de datos no es válido.\nPor favor, verifique e intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(new Form() { TopMost = true }, "Se ha presentado un problema.\n\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        } 

        //private static bool ActualizarConexion(string ubicacion) // Ya no tengo que actualizarla. El archivo restaurado siempre reemplazará el anterior
        //{
        //    try
        //    {
        //        string stringConnection = "Data Source=" + ubicacion + ";Version=3;New=True;Compress=True;Foreign Keys=ON";

        //        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        //        config.ConnectionStrings.ConnectionStrings["connstring"].ConnectionString = stringConnection;

        //        config.Save(ConfigurationSaveMode.Modified);

        //        ConfigurationManager.RefreshSection("connectionStrings");

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("La base de datos no fue restaurada.\n\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    return false;
        //}

        public static bool RestaurarCopia()
        {
            if (String.IsNullOrEmpty(carpetaRespaldos))
            {
                carpetaRespaldos = Application.StartupPath;

                if (Directory.Exists(Application.StartupPath + "\\Respaldos"))
                {
                    carpetaRespaldos = Application.StartupPath + "\\Respaldos";
                }
            }

            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.Filter = "Archivo de base da datos (*.db, *.sqlite, *.sqlite3) | *.db; *.sqlite; *.sqlite3";
                openFileDialog1.Title = "Restaurar copia de seguridad";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.InitialDirectory = carpetaRespaldos; //Application.StartupPath + "\\Respaldos";

                openFileDialog1.Multiselect = false;

                DialogResult dr = openFileDialog1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string respaldo = openFileDialog1.FileName;
                    string nuevaConexion = @"Data Source=" + respaldo + ";Version=3;New=True;Compress=True;Foreign Keys=ON";

                    if (ConexionExitosa(nuevaConexion)) // Es un archivo de base de datos válido
                    {
                        Msg msg = new Msg();

                        msg.lblMsg.Text = "¿Está seguro de que desea restaurar la copia?\nNota: Los datos actuales se perderán.";
                        DialogResult dlgres = msg.ShowDialog(new Form() { TopMost = true });
                        {
                            if (dlgres == DialogResult.Yes)
                            {
                                //if (File.Exists(Application.StartupPath + "\\HotelCountry.db"))
                                //{
                                //    FileInfo archivoAnterior = new FileInfo(Application.StartupPath + "\\HotelCountry.db");
                                //    archivoAnterior.CopyTo(Application.StartupPath + "\\HotelCountry-anterior.db", true);
                                //}

                                FileInfo respaldoBaseDatos = new FileInfo(respaldo);
                                respaldoBaseDatos.CopyTo(Application.StartupPath + "\\HotelCountry.db", true);

                                MessageBox.Show("La base de datos ha sido restaurada.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                return true;

                                //if (ActualizarConexion(respaldo)) // Se cambiaron los datos
                                //{
                                //    //MessageBox.Show("La base de datos ha sido restaurada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    return true;
                                //}
                            }
                        }
                    }
                    //else
                    //{
                    //    MessageBox.Show("No es un archivo válido. Verifique e intente nuevanemente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\n\nDetalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }
    }
}
