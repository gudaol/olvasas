using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace olvasas
{
    class Program
    {

        static string szotagol(string szo)
        {
            //System.Console.WriteLine(szo);
            string ret = szo;
            List<int> magindex = new List<int>();
            for (int i = 0; i < szo.Length; i++)
            {
                char kar = szo[i];
                if (DB.Maganhangzok.Contains(kar))
                {
                    //System.Console.WriteLine(i);
                    magindex.Add(i);
                }
            }
            if (magindex.Count > 1)
            {
                ret = szo[0..(magindex[0] + 1)];
                for (int i = 0; i < magindex.Count - 1; i++)
                {
                    if (magindex[i] + 1 == magindex[i + 1])
                    {
                        ret += szo[(magindex[i] + 1)..(magindex[i + 1])] + "-";
                    }
                    else
                    {
                        ret += szo[(magindex[i] + 1)..(magindex[i + 1])] + "-" + szo[magindex[i + 1] - 1];
                    }
                }
                ret += szo.Substring(magindex[magindex.Count - 1]);
                //System.Console.WriteLine(ret);
            }
            return ret;
        }

        static void Main(string[] args)
        {

            foreach (var szo in DB.Csunya)
            {
                DB.Szavak.RemoveAll(p => p == szo);
            }
            try
            {
                Console.WriteLine("Add meg a betűket amik szerepeljenek a szavakban, majd nyomj entert (pl.: aerthv[ENTER])!");
                var betuk = Console.ReadLine();
                betuk = betuk.ToLower();
                System.Console.WriteLine("Add meg számmal, legfeljebb milyen hosszú szavakat keressek!");
                var hossz = int.Parse(Console.ReadLine());
                var szurtSzavak = DB.Szavak.Where(p => p.Length <= hossz).Where(p => p.ToLower().All(c => betuk.Contains(c))).Where(p => p.Any(c => DB.Maganhangzok.Contains(c))).ToList();
                int i = 0;
                using (var o = new StreamWriter(new FileStream("szavak.txt", FileMode.Create, FileAccess.Write, FileShare.Read)))
                    foreach (var szo in szurtSzavak)
                    {
                        string v = szo + " :: " + szotagol(szo) + ' ';
                        System.Console.Write(v);
                        o.Write(v.AsSpan());
                        if ((i++ % 10) == 9)
                        {
                            o.Write(Environment.NewLine.AsSpan());
                            System.Console.Write(Environment.NewLine);
                        }

                    }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("naszépenviselkedjé!");
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine(e.StackTrace);
            }
            System.Console.WriteLine();
            System.Console.WriteLine("A szavakat a szavak.txt-be is beleírtam!");
            Console.ReadLine();
        }
    }
}
