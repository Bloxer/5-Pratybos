using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace P5
{
    class RaidziuDazniai
    {
        private const int CMax = 256;
        private int[] Rn;
        public string eil { get; set; }
        public RaidziuDazniai()
        {
            eil = "";
            Rn = new int[CMax];
            for (int i = 0; i < CMax; i++)
                Rn[i] = 0;
        }
        public int Imti(char sim)
        {
            return Rn[sim];
        }
        public void kiek()
        {
            for (int i = 0; i < eil.Length; i++)
            {
                if (('a' <= eil[i] && eil[i] <= 'z') ||
                ('A' <= eil[i] && eil[i] <= 'Z'))
                    Rn[eil[i]]++;
            }
        }
    }
    internal class Program
    {
        const string CFd = "..\\..\\U1.txt";
        const string CFr = "..\\..\\Rezultatai.txt";
        static void Main(string[] args)
        {
            RaidziuDazniai eil = new RaidziuDazniai();
            int dazniausias;

            Dazniai(CFd, eil);
            if (eil.eil.Length > 0)
            {
                Spausdinti(CFr, eil);
                RastiDazniausia(eil, out dazniausias);

                SpausdintiMazejant(CFr, eil, dazniausias);
            }
            else
            {
                using (var fr = File.AppendText(CFr))
                {
                    fr.WriteLine("Nera duomenu");
                }
            }
        }
        static void Spausdinti(string fv, RaidziuDazniai eil)
        {
            using (var fr = File.CreateText(fv))
            {
                for (char sim = 'a'; sim <= 'z'; sim++)
                {
                    fr.WriteLine("{0, 3:c} {1, 4:d} |{2, 3:c} {3, 4:d}",
                        sim, eil.Imti(sim),
                        Char.ToUpper(sim), eil.Imti(Char.ToUpper(sim)));
                }
            }
        }
        static void Dazniai(string fv, RaidziuDazniai eil)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    eil.eil = line;
                    eil.kiek();
                }
            }
        }
        static void RastiDazniausia(RaidziuDazniai eil, out int did)
        {
            int didziausias = 0;
            for (char sim = 'a'; sim <= 'z'; sim++)
            {
                if (eil.Imti(sim) > didziausias)
                {
                    didziausias = eil.Imti(sim);
                }
            }
            did = didziausias;
            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine("Dazniausi simboliai yra:");
                for (char sim = 'a'; sim <= 'z'; sim++)
                {
                    if (eil.Imti(sim) == didziausias)
                    {
                        fr.Write(sim + " ");
                    }
                }
                fr.WriteLine("\nJie pasikartoja po:");
                fr.Write(didziausias.ToString() + " kartus\n");
            }
        }
        static void SpausdintiMazejant(string fv, RaidziuDazniai eil, int did)
        {
            int ieskomaReiksme = did;
            using (var fr = File.AppendText(fv))
            {
                for (int i = 0; i <= did; i++)
                {
                    for (char sim = 'a'; sim <= 'z'; sim++)
                    {
                        if (eil.Imti(sim) == ieskomaReiksme)
                        {
                            fr.WriteLine("{0, 3:c} {1, 4:d}",
                                sim, eil.Imti(sim));
                        }
                    }
                    for (char sim = 'A'; sim <= 'Z'; sim++)
                    {
                        if (eil.Imti(sim) == ieskomaReiksme)
                        {
                            fr.WriteLine("{0, 3:c} {1, 4:d}",
                                Char.ToUpper(sim), eil.Imti(Char.ToUpper(sim)));
                        }
                    }
                    ieskomaReiksme--;
                }
            }
        }
    }
}
