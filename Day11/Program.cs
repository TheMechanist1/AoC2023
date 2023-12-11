using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day11
{
    
    internal class Program
    {

        public static int CalculateManhattanDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        public static void Part1(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            Dictionary<int, int> mapRow = new Dictionary<int, int>();
            Dictionary<int, int> mapColumn = new Dictionary<int, int>();

            List<Vector2> galaxyPos = new List<Vector2>();


            for (int y = 0; y < lines.Length; y++)
            {
                if (!mapRow.ContainsKey(y))
                {
                    mapRow.Add(y, 0);
                }
                if (lines[y].Contains("#")) mapRow[y] += 1;

                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (!mapColumn.ContainsKey(x))
                    {
                        mapColumn.Add(x, 0);
                    }

                    if (lines[y][x] == '#')
                    {
                        mapColumn[x] += 1;
                    }
                }
            }

            int offsetX = 0, offsetY = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                if (mapRow[y] == 0)
                {
                    offsetY+=999999;
                }

                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (mapColumn[x] == 0) offsetX+=999999;
                    if (lines[y][x] == '#') galaxyPos.Add(new Vector2((float)x+offsetX, (float)y+offsetY));

                    
                }
                offsetX = 0;
            }

            for (int y2 = 0; y2 < 13; y2++)
            {
                for (int x2 = 0; x2 < 13; x2++)
                {
                    if (galaxyPos.Contains(new Vector2((float)x2, (float)y2)))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.Write("\n");
            }

            long distance = 0;
            var pairs = 0;
            for (int i = 0; i < galaxyPos.Count; i++)
            {
                for (int j = i+1; j < galaxyPos.Count; j++)
                {
                    distance += CalculateManhattanDistance((int)galaxyPos[i].X, (int)galaxyPos[i].Y, (int)galaxyPos[j].X, (int)galaxyPos[j].Y);
                    pairs++;
                }
            }

            Console.WriteLine(distance);

        }

        public void Part2(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
        }

        static void Main(string[] args)
        {
            Part1("puzzle.txt");
        }
    }
}
