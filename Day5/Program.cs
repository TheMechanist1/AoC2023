using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static Day5.Prog;

namespace Day5
{
    public class Prog
    {
        public class Range
        {
            public long Source, Destination, Size;

            //50 98 2
            //52 50 48

            public bool Contains(long value)
            {
                if (value >= Source && value < Source + Size)
                {
                    return true;
                }
                return false;
            }
            public long MatPat(long value)
            {
                return value - Source + Destination;
            }

            public override string ToString()
            {
                return Destination.ToString() + " " + Source.ToString() + " " + Size.ToString();
            }


        }
        public class AgricultureMaps
        {
            // Lists of lists for each map.
            public List<Range> seeds = new List<Range>();
            public List<Range> SeedToSoilMap { get; set; }
            public List<Range> SoilToFertilizerMap { get; set; }
            public List<Range> FertilizerToWaterMap { get; set; }
            public List<Range> WaterToLightMap { get; set; }
            public List<Range> LightToTemperatureMap { get; set; }
            public List<Range> TemperatureToHumidityMap { get; set; }
            public List<Range> HumidityToLocationMap { get; set; }

            public AgricultureMaps()
            {
                SeedToSoilMap = new List<Range>();
                SoilToFertilizerMap = new List<Range>();
                FertilizerToWaterMap = new List<Range>();
                WaterToLightMap = new List<Range>();
                LightToTemperatureMap = new List<Range>();
                TemperatureToHumidityMap = new List<Range>();
                HumidityToLocationMap = new List<Range>();
            }
            public List<long> SeedToMap(List<long> Input, List<Range> Destination)
            {
                //Dont know which range it is so we have to loop over them until we get it
                //If we dont then it is one to one
                List<long> output = new List<long>();

                foreach (long seed in Input) 
                {
                    var contFlag = false;
                    Range contRange = new Range();
                    foreach (Range range in Destination)
                    {
                        if (range.Contains(seed))
                        {
                            contFlag = true;
                            contRange = range;
                            break;
                        }
                    }
                    if (contFlag)
                    {
                        output.Add(contRange.MatPat(seed));
                    }
                    else
                    {
                        output.Add(seed);

                    }
                }

                return output;
            }

            public long SingleToMap(long Input, List<Range> Destination)
            {
                //Dont know which range it is so we have to loop over them until we get it
                //If we dont then it is one to one
                long output = 0;

                    var contFlag = false;
                    Range contRange = new Range();
                    foreach (Range range in Destination)
                    {
                        if (range.Contains(Input))
                        {
                            contFlag = true;
                            contRange = range;
                            break;
                        }
                    }
                    if (contFlag)
                    {
                        output = contRange.MatPat(Input);
                    }
                    else
                    {
                        output = Input;

                    }

                return output;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine("Seeds:");
                sb.AppendLine(ConvertMapToString(seeds));

                sb.AppendLine("Seed To Soil Map:");
                sb.AppendLine(ConvertMapToString(SeedToSoilMap));

                sb.AppendLine("Soil To Fertilizer Map:");
                sb.AppendLine(ConvertMapToString(SoilToFertilizerMap));

                sb.AppendLine("Fertilizer To Water Map:");
                sb.AppendLine(ConvertMapToString(FertilizerToWaterMap));

                sb.AppendLine("Water To Light Map:");
                sb.AppendLine(ConvertMapToString(WaterToLightMap));

                sb.AppendLine("Light To Temperature Map:");
                sb.AppendLine(ConvertMapToString(LightToTemperatureMap));

                sb.AppendLine("Temperature To Humidity Map:");
                sb.AppendLine(ConvertMapToString(TemperatureToHumidityMap));

                sb.AppendLine("Humidity To Location Map:");
                sb.AppendLine(ConvertMapToString(HumidityToLocationMap));

                return sb.ToString();
            }

            private string ConvertMapToString(List<Range> map)
            {
                var sb = new StringBuilder();
                foreach (var row in map)
                {
                    sb.AppendLine(row.ToString());
                }
                return sb.ToString();
            }

            private string ConvertListToString(List<long> map)
            {
                var sb = new StringBuilder();
                foreach (var row in map)
                {
                    sb.AppendLine(row.ToString());
                }
                return sb.ToString();
            }
        }
        public void part1(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            AgricultureMaps agricultureMaps = new AgricultureMaps();
            int mapid = -1;
            foreach (var line in lines)
            {
                if (line == " ") continue;
                if (Regex.IsMatch(line, @"[a-zA-Z]"))
                {
                    mapid += 1;
                }

                var match = Regex.Matches(line, @"\d+");

                if (match.Count == 0) { continue; }




                if (mapid == 0)
                {
                    Range range = new Range();
                    for (int i = 0; i < match.Count; i++)
                    {
                        var m = match[i];
                        if (i % 2 == 0) range.Source = long.Parse(m.ToString());
                        if (i % 2 == 1)
                        {
                            range.Size = long.Parse(m.ToString());
                            agricultureMaps.seeds.Add(range);
                            range = new Range();
                        }

                    }
                }
                else
                {

                    Range range = new Range();
                    range.Destination = long.Parse(match[0].Value);
                    range.Source = long.Parse(match[1].Value);
                    range.Size = long.Parse(match[2].Value);

                    switch (mapid)
                    {
                        case 1:
                            agricultureMaps.SeedToSoilMap.Add(range);
                            break;
                        case 2:
                            agricultureMaps.SoilToFertilizerMap.Add(range);
                            break;
                        case 3:
                            agricultureMaps.FertilizerToWaterMap.Add(range);
                            break;
                        case 4:
                            agricultureMaps.WaterToLightMap.Add(range);
                            break;
                        case 5:
                            agricultureMaps.LightToTemperatureMap.Add(range);
                            break;
                        case 6:
                            agricultureMaps.TemperatureToHumidityMap.Add(range);
                            break;
                        case 7:
                            agricultureMaps.HumidityToLocationMap.Add(range);
                            break;
                    }
                }


            }
            long min = long.MaxValue;
            for (int i = 0; i < agricultureMaps.seeds.Count; i++)
            {
                Range range = agricultureMaps.seeds[i];
                Console.WriteLine(agricultureMaps.seeds[i]);
                for (long j = range.Source; j < range.Source+range.Size; j++)
                {
                    long seed = agricultureMaps.SingleToMap(j, agricultureMaps.SeedToSoilMap);
                    seed = agricultureMaps.SingleToMap(seed, agricultureMaps.SoilToFertilizerMap);
                    seed = agricultureMaps.SingleToMap(seed, agricultureMaps.FertilizerToWaterMap);
                    seed = agricultureMaps.SingleToMap(seed, agricultureMaps.WaterToLightMap);
                    seed = agricultureMaps.SingleToMap(seed, agricultureMaps.LightToTemperatureMap);
                    seed = agricultureMaps.SingleToMap(seed, agricultureMaps.TemperatureToHumidityMap);
                    seed = agricultureMaps.SingleToMap(seed, agricultureMaps.HumidityToLocationMap);
                    if (seed < min) min = seed;
                    if(j%1000000==0) Console.WriteLine((range.Source + range.Size)-j);
                }
            }
            Console.WriteLine("min is " + min);
        }

        class Program
        {
            static void Main(string[] args)
            {
                Prog prog = new Prog();
                prog.part1("puzzle.txt");
            }
        }
    }
}
