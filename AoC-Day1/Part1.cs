using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC_Day1
{
    internal class Part1
    {
        ArrayList puzzleInput = new ArrayList() { };
        static void MainOFF(string[] args)
        {
            string[] lines = File.ReadAllLines("PuzzleInput1.txt");
            int output = 0;
            foreach (var item in lines)
            {
                string first = ""; string second = "";
                Match match = Regex.Match(item, @"\d");
                if(match.Success)
                {
                    first = match.Value;
                }

                foreach (Match match1 in Regex.Matches(item, @"\d"))
                {
                    // Update lastInteger with the latest found integer
                    second = match1.Value;
                }

                Console.WriteLine(item + " " + first + " " + second);
                output += int.Parse(first + second);
            }

            Console.WriteLine(output);
        }
    }

    internal class Part2
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("PuzzleInput1.txt");
            int output = 0;
            Dictionary<string, int> wordToInt = new Dictionary<string, int>();
            wordToInt.Add("one", 1);
            wordToInt.Add("two", 2);
            wordToInt.Add("three", 3);
            wordToInt.Add("four", 4);
            wordToInt.Add("five", 5);
            wordToInt.Add("six", 6);
            wordToInt.Add("seven", 7);
            wordToInt.Add("eight", 8);
            wordToInt.Add("nine", 9);

            foreach (string line in lines) {
                var digits = new List<int>();

                for (var i = 0; i < line.Length; i++)
                {
                    if (char.IsNumber(line[i]))
                    {
                        digits.Add(item: line[i] - '0');
                        continue;
                    }

                    foreach (var pair in wordToInt)
                    {
                        if (i + pair.Key.Length - 1 >= line.Length || !line.Substring(i, pair.Key.Length).Equals(pair.Key))
                        {
                            continue;
                        }

                        digits.Add(pair.Value);
                        break;
                    }
                }
                Console.Write(digits.Count + " ");

                output += (digits.First() * 10);
                output += digits.Last();
            }

            Console.WriteLine(output);
        }
    }
}
