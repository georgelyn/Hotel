using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel
{
    class CrearBaseDatos
    {
        public static string query = @"CREATE TABLE IF NOT EXISTS [Clientes] (
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

                        CREATE TABLE IF NOT EXISTS Seguridad (
                        [ID] INTEGER PRIMARY KEY AUTOINCREMENT,
                        [User] VARCHAR,
                        [Password] VARCHAR
                    )";
    }
}
