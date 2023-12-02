using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Day2.Program;

namespace Day2
{
    internal class Program
    {
        public class GameSubSet
        {
            public int red;
            public int green;
            public int blue;

            public bool isPossible()
            {
                return (red <= 12 && green <= 13 && blue <= 14);
            }

            public int power()
            {
                return red * green * blue;
            }
        }

        public class GameGuy
        {
            public List<GameSubSet> gameSubsets = new List<GameSubSet>();

            public void addSubset(GameSubSet sub)
            {
                gameSubsets.Add(sub);
            }

            public bool possibleGames()
            {
                int count = 0;
                foreach (GameSubSet sub in gameSubsets) 
                {
                    if (!sub.isPossible()) count++;
                }

                return count == 0;
            }

            public GameSubSet maxColorVal()
            {
                GameSubSet maxGameSubSet = new GameSubSet();

                foreach (GameSubSet sub in gameSubsets)
                {
                    if (sub.red > maxGameSubSet.red) maxGameSubSet.red = sub.red;
                    if (sub.green > maxGameSubSet.green) maxGameSubSet.green = sub.green;
                    if (sub.blue > maxGameSubSet.blue) maxGameSubSet.blue = sub.blue;
                }

                return maxGameSubSet;
            }
        }

        static public List<GameGuy> Parser(String filePath)
        {
            List<GameGuy> gameGuys = new List<GameGuy>();

            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++) 
            {
                GameGuy game = new GameGuy();
                string modifiedString = lines[i].Remove(0, lines[i].IndexOf(":"));
                string[] games = modifiedString.Split(';');

                for (int j = 0; j < games.Length; j++)
                {
                    GameSubSet gameSubSet = new GameSubSet();
                    games[j] = games[j].Replace("red", "r")
                                       .Replace("green", "g")
                                       .Replace("blue", "b")
                                       .Replace(" ", "");

                    string[] set = games[j].Split(',');
                    //3b,14r
                    for (int k = 0; k < set.Length; k++)
                    {
                        var digitsMatch = Regex.Match(set[k], @"\d+");
                        
                        switch(set[k].Last())
                        {
                            case 'r':
                                gameSubSet.red = int.Parse(digitsMatch.Value); 
                                break;
                            case 'g':
                                gameSubSet.green = int.Parse(digitsMatch.Value);
                                break;
                            case 'b':
                                gameSubSet.blue = int.Parse(digitsMatch.Value);
                                break;
                        }
                    }

                    game.addSubset(gameSubSet);
                }

                gameGuys.Add(game);
            }

            return gameGuys;
        }

        static void Main(string[] args)
        {
            //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            //"game number": d color, d color, d color;
            //Delete game id start at ':'
            //Get color and digit
            //Read until ';'
            List<GameGuy> puzzleGames = Parser("Puzzle.txt");
            Console.WriteLine(puzzleGames.ToString());

            var count = 0;
            var part2Count = 0;
            for (int i = 0; i < puzzleGames.Count; i++) 
            {
                if (puzzleGames[i].possibleGames()) 
                {
                    count += i + 1;
                }

                part2Count += puzzleGames[i].maxColorVal().power();
            }

            Console.WriteLine(count.ToString() + " " + part2Count.ToString());
        }
    }
}
