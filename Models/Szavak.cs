using System;
using System.Collections.Generic;
using System.IO;

namespace OlvasasGyakorlo.Models
{
    public class DB
    {

        public static List<char> Maganhangzok = new List<char>()
            {
                'a','á','e','é','i','í','o','ó','ö','ő','u','ú','ü','ű'
            };

        public static List<string> Szavak = new List<string>(File.ReadAllLines("hu.txt"));

        public static List<string> Csunya = new List<string>(File.ReadAllLines("csunya.txt"));

    }
}