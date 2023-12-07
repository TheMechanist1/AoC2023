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
        public int copies = 1;

        public LottryTicket() { }

        public int pointCalc()
        {
            int len = winnings.Intersect(scratched).ToArray().Length;
            if (len == 0) return 0;
            return (int) len;
        }
        public override string ToString()
        {
            string output = "";
            //foreach (int i in winnings) output += i.ToString() + " ";
            //output += "| ";
            //foreach (int i in scratched) output += i.ToString() + " ";
            output += "Copies: " + copies;
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
            }
        }

        public static void part2(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            List<LottryTicket> lottryTickets = new List<LottryTicket>();

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
                lottryTickets.Add(lottryTicket);
            }

            for (int i = 0; i < lottryTickets.Count; i++)
            {
                var ticket = lottryTickets[i];
                int count = ticket.pointCalc();
                Console.WriteLine(count);

                for (int j = i+1; j <= i+count; j++)
                {
                    lottryTickets[j].copies += ticket.copies;
                }
            }
            var cards = 0;
            foreach (var ticket in lottryTickets)
            {
                cards += ticket.copies;
            }
            Console.WriteLine("Cards" + cards);
        }
        static void Main(string[] args)
        {
            part2("puzzle.txt");
        }
    }
}
