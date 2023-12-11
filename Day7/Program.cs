using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7
{
    public class Hand
    {
        public string Card = "";
        public int Bid = 0;
        static Dictionary<string, int> cardScores = new Dictionary<string, int>
        {
            { "A", 13 },
            { "K", 12 },
            { "Q", 11 },
            { "J", 10 },
            { "T", 9 },
            { "9", 8 },
            { "8", 7 },
            { "7", 6 },
            { "6", 5 },
            { "5", 4 },
            { "4", 3 },
            { "3", 2 },
            { "2", 1 }
        };

        public Hand(string cards, int bid)
        {
            Card = cards;
            this.Bid = bid;
        }

        public class ScoreComparer : IComparer<Hand>
        {
            public int Compare(Hand x, Hand y)
            {
                if (x.ScoreCalculator() != y.ScoreCalculator())
                {
                    return x.ScoreCalculator().CompareTo(y.ScoreCalculator());
                } else
                {
                    for(int i = 0; i < x.Card.Length; i++)
                    {
                        if (x.Card[i] != y.Card[i])
                        {
                            if (Hand.cardScores[x.Card[i].ToString()] > Hand.cardScores[y.Card[i].ToString()])
                            {
                                return 1;
                            } else
                            {
                                return -1;
                            }
                        } else
                        {
                            continue;
                        }
                    }
                    return 0;
                }
            }
        }

        public int ScoreCalculator()
        {
            if (isOfKind(5)) return 7;
            if (isOfKind(4)) return 6;
            if (isOfKind(3) && isOfKind(2)) return 5; //Full House
            if (isOfKind(3) && isOfKind(1, 2)) return 4; //Three of a kind
            if (isOfKind(2, 2) && isOfKind(1)) return 3; //Two Pair
            if (isOfKind(2) && isOfKind(1, 3)) return 2; //One Pair
            return 1;
        }

        public bool isOfKind(int numOfCards, int numOfTimes)
        {
            Dictionary<string, int> repeatedChars = new Dictionary<string, int>();
            foreach (var letter in Card)
            {
                if (repeatedChars.ContainsKey(letter.ToString()))
                {
                    repeatedChars[letter.ToString()]++;
                }
                else
                {
                    repeatedChars.Add(letter.ToString(), 1);
                }
            }
            return repeatedChars.Values.Count(value => value == numOfCards) == numOfTimes;

        }

        public bool isOfKind(int numOfCards)
        {
            Dictionary<string, int> repeatedChars = new Dictionary<string, int>();
            foreach (var letter in Card)
            {
                if (repeatedChars.ContainsKey(letter.ToString()))
                {
                    repeatedChars[letter.ToString()]++;
                }
                else
                {
                    repeatedChars.Add(letter.ToString(), 1);
                }
            }
            return repeatedChars.ContainsValue(numOfCards);
        }

        public override string ToString()
        {
            return $"({Card}, {Bid})";
        }
    }

    class Program
    {
        public static void Part1(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            List<Hand> hands = new List<Hand>();

            foreach (string line in lines)
            {
                var match = Regex.Split(line, " ");
                Hand hand = new Hand(match.First(), int.Parse(match.Last()));
                hands.Add(hand);
            }

            hands.Sort(new Hand.ScoreComparer());

            var total = 0;
            for (int i = 0; i < hands.Count(); i++)
            {
                var hand = hands[i];
                Console.WriteLine(hand.Card);
                total += (i+1) * hand.Bid;
            }
            Console.WriteLine(total);

        }

        static void Main(string[] args)
        {
            Part1("puzzle.txt");
        }
    }
}
