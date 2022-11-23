using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace P5_2
{
    class Program
    {
        const string CFd = "..\\..\\U1.txt";
        const string CFr = "..\\..\\Rezultatai.txt";
        const int CMax = 256;
        static void Main(string[] args)
        {
            string[] eilutes = new string[CMax];
            eilutes[0] = "";
            Skaityti(CFd, ref eilutes);
            Spausdinti(CFr, eilutes);
        }
        static void Skaityti(string fv, ref string[] eilutes)
        {
            int i = 0;
            string[] lines = File.ReadAllLines(fv);
            foreach (string line in lines)
            {
                if (line.Length != 0)
                {
                    eilutes[i] = line.ToString();
                    i++;
                }
            }
        }
        static void Spausdinti(string fvr, string[] eilutes)
        {
            using (var fr = File.CreateText(fvr))
            {
                foreach (string line in eilutes)
                {
                    fr.WriteLine(line);
                }
            }
        }
    }
}
