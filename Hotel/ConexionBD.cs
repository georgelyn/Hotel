using System;
using System.Collections.Generic;
using System.Configuration;

namespace Hotel
{
    class ConexionBD
    {
        public static string connstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;      
    }
}
