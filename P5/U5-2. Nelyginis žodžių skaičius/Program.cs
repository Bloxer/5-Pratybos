using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace U5_2.Nelyginis_žodžių_skaičius
{
    internal class Program
    {
        const string CFr = "..\\..\\Results.txt";
        const string CFa = "..\\..\\Analysis.txt";
        const string CFd = "..\\..\\Data.txt";
        static void Main(string[] args)
        {
            if (File.Exists(CFr))
                File.Delete(CFr);
            if (File.Exists(CFa))
                File.Delete(CFa);

            string dividers = " .-_,!?;:()\t";
            char[] arrDividers = dividers.ToCharArray();
            string[] lines = new string[NoOfLines(CFd)];

            ReadFile(CFd, ref lines);
            ChangeOutput(lines, CFr, CFa, arrDividers);
        }
        static int NoOfLines(string fd)
        {
            int amount = 0;
            string[] lines = File.ReadAllLines(fd);
            foreach (string line in lines)
            {
                if (line.Length != 0)
                {
                    amount++;
                }
            }
            return amount;
        }
        static void ReadFile(string fd, ref string[] linesIn)
        {
            int i = 0;
            string[] lines = File.ReadAllLines(fd);
            foreach (string line in lines)
            {
                if (line.Length != 0)
                {
                    linesIn[i] = line.ToString();
                    i++;
                }
            }
        }
        static void ChangeOutput(string[] lines, string fr,
            string fa, char[] dividers)
        {
            int i = 1;
            string newLine = "";
            foreach(string line in lines)
            {
                string[] parts = line.Split(dividers,
                                          StringSplitOptions.
                                          RemoveEmptyEntries);

                using (var analysisFile = File.AppendText(fa))
                {
                    analysisFile.WriteLine(parts.Count() +
                        " words in line " + i);
                    if(parts.Count() % 2 != 0)
                    {
                        int n = parts.Count();
                        n = n / 2 + 1;
                        analysisFile.WriteLine("Word changed:" +
                            parts[n]);

                        int index = line.IndexOf(parts[n]);
                        newLine = line.Remove(index, parts[n].Length);
                        newLine = newLine.Insert(index, "xxooxx");
                    }
                }
                using (var resultsFile = File.AppendText(fr))
                {
                    if (parts.Count() % 2 == 0)
                    {
                        resultsFile.WriteLine(line);
                    }
                    if(parts.Count() % 2 != 0)
                    {
                        resultsFile.WriteLine(newLine);
                    }
                }
                i++;
            }
        }
    }
}
