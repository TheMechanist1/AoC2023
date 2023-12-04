using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day4
{
    public class LottryTicket
    {
        public List<int> winnings = new List<int>();
        public List<int> scratched = new List<int>();

        public LottryTicket() { }

        public int pointCalc()
        {
            int len = winnings.Intersect(scratched).ToArray().Length;
            if (len == 0) return 0;
            return (int)Math.Pow(2, len-1);
        }
        public override string ToString()
        {
            string output = "win ";
            foreach (int i in winnings) output += i.ToString() + " ";
            output += "| scratch ";
            foreach (int i in scratched) output += i.ToString() + " ";
            return output;
        }

    }
    internal class Program
    {
        public static void part1(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            var output = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Skip(10).ToArray();
                var winloss = Regex.Split(String.Join("", line), @"\|");
                LottryTicket lottryTicket = new LottryTicket();

                for (int j = 0; j < winloss.Length; j++)
                {
                    var matches = Regex.Matches(winloss[j], @"\d+");

                    foreach (var num in matches)
                    {
                        var num2 = int.Parse(num.ToString());

                        if (j == 0)
                        {
                            lottryTicket.winnings.Add(num2);

                        } 
                        else
                        {
                            lottryTicket.scratched.Add(num2);
                        }
                    }


                }
                output += lottryTicket.pointCalc();

                Console.WriteLine(lottryTicket);

            }
            Console.WriteLine(output);

        }
        static void Main(string[] args)
        {
            part1("puzzle.txt");
        }
    }
}
