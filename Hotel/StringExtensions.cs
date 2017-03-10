using System;

namespace Hotel
{
    public static class StringExtensions
    {
        public static string NullString(string s)// this string s) This es compatible con Net 3.5+. Necesita System.Core.dll
        {
            return string.IsNullOrEmpty(s) ? null : s;
        }

        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

    }
}
