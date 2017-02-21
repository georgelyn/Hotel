using System;

namespace Hotel
{
    public static class StringExtensions // Para ingresar 'null' en la base de datos si no hay dato
    {
        public static string NullString(string s)// this string s) This es compatible con Net 3.5+. Necesita System.Core.dll
        {
            return string.IsNullOrEmpty(s) ? null : s;
        }

    }
}
