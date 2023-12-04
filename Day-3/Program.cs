using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Numerics;

namespace Day3
{

    public class Program
    {
        public static bool isSafe(string[] list, int y, int x)
        {
            try 
            {
                var temp = list[y][x];
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool isSymbol(string[] list, int y, int x)
        {
            try
            {
                return Regex.IsMatch(list[y][x].ToString(), @"[\*|\#|\$|\+|\%|\/|\@|\=|\&|\-]");
            } 
            catch
            {
                return false;
            }
        }

        public static bool isStar(string[] list, int y, int x)
        {
            try
            {
                return Regex.IsMatch(list[y][x].ToString(), @"\*");
            }
            catch
            {
                return false;
            }
        }

        public static bool isDigit(string[] list, int y, int x)
        {
            try
            {
                return Regex.IsMatch(list[y][x].ToString(), @"\d");
            }
            catch
            {
                return false;
            }
        }

        public class FirstItemEqualityComparer : IEqualityComparer<(int, string)>
        {
            public bool Equals((int, string) x, (int, string) y)
            {
                // Compare only the first item of the tuples
                return x.Item1 == y.Item1;
            }

            public int GetHashCode((int, string) obj)
            {
                // Use the hash code of the first item for hashing
                return obj.Item1.GetHashCode();
            }
        }

        public static (int, int) intBuilder(string[] list, int y, int x, bool first)
        {
            string output = "";
            if (!isDigit(list, y, x)) return (-1, -1);
            if (!isDigit(list, y, x - 1) || x - 1 < 0) first = true;
            if(first)
            {
                while (isDigit(list, y, x + 1))
                {
                    output += list[y][x];
                    x += 1;
                }
                output += list[y][x];
                return (y * list[x].Length + x, int.Parse(output));
            } 
            else
            {
                return intBuilder(list, y, x - 1, false);
            }
        }
        public static int part1(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int fullAmount = 0;
            
            for (var y = 0; y < lines.Length; y++)
            {
                var symbolFlag = false;
                string number = "";

                //Get the last index of the line
                for (var x = 0; x < lines[y].Length; x++)
                {
                    if(Char.IsDigit(lines[y][x]))
                    {
                        number += lines[y][x].ToString();
                        if (isSymbol(lines, y - 1, x) || isSymbol(lines, y - 1, x + 1) || isSymbol(lines, y - 1, x - 1) ||
                            isSymbol(lines, y, x + 1)     || isSymbol(lines, y, x - 1)     || isSymbol(lines, y + 1, x) ||
                            isSymbol(lines, y + 1, x + 1) || isSymbol(lines, y + 1, x - 1)) symbolFlag = true;
                    } else {
                        if (number.Length > 0)
                        {
                            if (symbolFlag) fullAmount += int.Parse(number);
                            number = "";
                            symbolFlag = false;
                        }
                    }
                }
                if (symbolFlag) fullAmount += int.Parse(number);
                number = "";
                symbolFlag = false;
            }

            return fullAmount;
        }

        public static int part2(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int number = 0;
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    if(isStar(lines, y, x))
                    {
                        List<(int, int)> parts = new List<(int, int)>
                        {
                            intBuilder(lines, y - 1, x - 1, false),
                            intBuilder(lines, y - 1, x, false),
                            intBuilder(lines, y - 1, x + 1, false),
                            intBuilder(lines, y, x - 1, false),
                            intBuilder(lines, y, x + 1, false),
                            intBuilder(lines, y + 1, x - 1, false),
                            intBuilder(lines, y + 1, x, false),
                            intBuilder(lines, y + 1, x + 1, false)
                        };

                        var distinctParts = parts.GroupBy(p => p.Item1)  // Group by the first item
                         .Select(g => g.First()) // Select the first element from each group
                         .ToList();

                        distinctParts.RemoveAll(z => z.Item1 == -1);

                        if(distinctParts.Count == 2)
                        {
                            number += distinctParts[0].Item2 * distinctParts[1].Item2;
                        }
                    }
                }
            }
            return number;
        }
        static void Main(string[] args)
        {
            int p1 = part1("puzzle.txt");
            Console.WriteLine(p1);

            int p2 = part2("puzzle.txt");
            Console.WriteLine(p2);
        }
    }
}
