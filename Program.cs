﻿using System;
using System.Drawing;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            Day7 prog = new Day7();
           // Day6 prog = new Day6();
            //Day5 prog = new Day5();
            //Day4 prog = new Day4();
            //Day3 prog = new Day3();
            //Day2 prog = new Day2();
            //Day1 prog = new Day1();
            prog.Run();


            Console.WriteLine("\n\n\nPress Enter");
            Console.In.ReadLine();
        }


        public static string[] readFile(int num)
        {
            /*
            int counter = 0;
            string line;
            Dictionary<int, string> lines = new Dictionary<int, string>();

            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\TroZ\Projects\AdventOfCode\AdventOfCode\AdventOfCode\day"+num+".txt");
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(counter, line);
                System.Console.WriteLine(line);
                counter++;
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);

            string[] ret = new string[lines.Count];
            for(int i = 0; i < ret.Length; i++)
            {
                ret[i] = lines[i];
            }
            /*/
            return System.IO.File.ReadAllLines(@"C:\Users\TroZ\source\repos\AdventOfCode2020\AdventOfCode2020\day" + num + ".txt");
            //*/
        }

        internal static void saveImg(Bitmap pic, int num)
        {
            pic.Save(@"C:\Users\TroZ\source\repos\AdventOfCode2020\AdventOfCode2020\day" + num + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
