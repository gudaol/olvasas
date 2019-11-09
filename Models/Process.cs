using OlvasasGyakorlo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OlvasasGyakorlo.Models
{
    static class Process
    {
        static Process()
        {
            foreach (var szo in DB.Csunya)
            {
                DB.Szavak.RemoveAll(p => p == szo);
            }
        }

        static string szotagol(string szo)
        {
            string ret = szo;
            List<int> magindex = new List<int>();
            for (int i = 0; i < szo.Length; i++)
            {
                char kar = szo[i];
                if (DB.Maganhangzok.Contains(kar))
                {
                    magindex.Add(i);
                }
            }
            if (magindex.Count > 1)
            {
                ret = szo[0..(magindex[0])];
                for (int i = 0; i < magindex.Count - 1; i++)
                {
                    if (magindex[i] + 1 == magindex[i + 1])
                    {
                        ret += szo[(magindex[i])..(magindex[i + 1])] + "-";
                    }
                    else
                    {
                        ret += szo[(magindex[i])..(magindex[i + 1] - 1)] + "-" + szo[magindex[i + 1] - 1];
                    }
                }
                ret += szo.Substring(magindex[magindex.Count - 1]);
            }
            return ret;
        }

        internal static string GetUpdatedOutput(string betuk, int hossz)
        {
            try
            {
                var szurtSzavak = DB.Szavak.Where(
                    p => p.Length <= hossz
                    && p.ToLower().All(c => betuk.Contains(c))
                    && p.Any(c => DB.Maganhangzok.Contains(c))
                ).ToList();
                var szavak = new List<string>();
                int j = 0;
                var r = new Random();
                while (j++ < 30 && szurtSzavak.Count != 0)
                {
                    var selected = r.Next(szurtSzavak.Count);
                    szavak.Add(szurtSzavak[selected]);
                    szurtSzavak.RemoveAt(selected);
                }

                int i = 0;
                using var o = new StreamWriter(new FileStream("szavak.txt", FileMode.Create, FileAccess.Write, FileShare.Read));
                string buffer = "";
                foreach (var szo in szavak)
                {
                    string v = szotagol(szo) + "   ";
                    buffer += v;
                    o.Write(v);
                    if ((i++ % 5) == 4)
                    {
                        buffer += Environment.NewLine;
                        o.Write(Environment.NewLine);
                    }

                }
                return buffer;
            }
            catch (System.Exception e)
            {
                return "Na szépen viselkedjél, dobáltatod itt a hibákat..." + Environment.NewLine + e.Message + Environment.NewLine + e.StackTrace;
            }
        }
    }
}
