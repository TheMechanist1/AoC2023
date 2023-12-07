using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    //Time:        45     97     72     95
    //Distance:   305   1062   1110   1695
    internal class Program
    {
        static void Main(string[] args)
        {
            long time = 45977295;
            long dist = 305106211101695;

            var mult = 0;
            for(int i = 0; i < time; i++)
            {
                var ways = i * (time - i) > dist;
                if (ways)
                {
                    mult += 1;
                }
            }

            Console.WriteLine(mult);
        }
    }
}

//28 * 72 * 27 * 48