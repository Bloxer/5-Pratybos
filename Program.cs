using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace P5_1
{
    class RaidziuDazniai
    {
        private const int CMax = 383;
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
        public void Deti(char sim, int kiekis)
        {
            Rn[sim] = kiekis;
        }
        public int kiekLT(string str, char x)
        {
            int count = 0;
            for (int i = 0; i < eil.Length; i++)
                if (str[i] == x)
                    count++;
            return count;
        }
    }
    internal class Program
    {
        const string CFd = "..\\..\\U1.txt";
        const string CFr = "..\\..\\Rezultatai.txt";
        static void Main(string[] args)
        {
            const string Abecele = "AaĄąBbCcČčDdEeĘęĖėFfGgHhIiĮįYyJjKkLlMmNnOoPpRrSsŠšTtUuŲųŪūVvZzŽž";
            char[] arrAbecele = Abecele.ToCharArray();
            RaidziuDazniai eil = new RaidziuDazniai();
            int dazniausias;
            Skaityti(CFd, eil);
            if (eil.eil.Length > 0)
            {
                Spausdinti(CFr, eil, arrAbecele);
                RastiDazniausia(eil, out dazniausias, arrAbecele);

                SpausdintiMazejant(CFr, eil, dazniausias, arrAbecele);
            }
            else
            {
                using (var fr = File.AppendText(CFr))
                {
                    fr.WriteLine("Nera duomenu");
                }
            }
        }
        static void Skaityti(string fv, RaidziuDazniai eil)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    eil.eil = line;
                }
            }
        }
        static void Spausdinti(string fv, RaidziuDazniai eil, char[] arrAbecele)
        {
            using (var fr = File.CreateText(fv))
            {
                foreach (char c in arrAbecele)
                {
                    int kiekis = eil.kiekLT(eil.eil, c);
                    fr.WriteLine("{0, 3:c} {1, 4:d}",
                        c, kiekis);
                    eil.Deti(c, kiekis);
                }
            }
        }
        static void RastiDazniausia(RaidziuDazniai eil, out int did, char[] arrAbecele)
        {
            int didziausias = 0;
            foreach (char c in arrAbecele)
            {
                if (eil.Imti(c) > didziausias)
                {
                    didziausias = eil.Imti(c);
                }
            }
            did = didziausias;
            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine("Dazniausi simboliai yra:");
                foreach (char c in arrAbecele)
                {
                    if (eil.Imti(c) == didziausias)
                    {
                        fr.Write(c + " ");
                    }
                }
                fr.WriteLine("\nJie pasikartoja po:");
                fr.Write(didziausias + " kartus\n");
            }
        }
        static void SpausdintiMazejant(string fv, RaidziuDazniai eil, int did, char[] arrAbecele)
        {
            int ieskomaReiksme = did;
            using (var fr = File.AppendText(fv))
            {
                for (int i = 0; i <= did; i++)
                {
                    foreach(char c in arrAbecele)
                    {
                        if (eil.Imti(c) == ieskomaReiksme)
                        {
                            fr.WriteLine("{0, 3:c} {1, 4:d}",
                                c, eil.Imti(c));
                        }
                    }
                    ieskomaReiksme--;
                }
            }
        }
    }
}