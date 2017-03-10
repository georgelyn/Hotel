using System;
using System.Collections.Generic;


namespace Hotel
{
    class ConexionBD
    {
        private static string ubicacion = "|DataDirectory|\\hotel.db";

        public static string nuevaUbicacion
        {
            set
            {
                ubicacion = value;
            }
            get
            {
                return ubicacion;
            }
        }

        //public static string ubicacion = "|DataDirectory|\\";
        //public static string nombre = "hotel.db";

        public static string connstring = @"Data Source=" + nuevaUbicacion + ";Version=3;New=True;Compress=True;Foreign Keys=ON";
        
    }
}
