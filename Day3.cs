using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day3 : Day
    {
        String[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = { "..##.......",
"#...#...#..",
".#....#..#.",
"..#.#...#.#",
".#...##..#.",
"..#.##.....",
".#.#.#....#",
".#........#",
"#.##...#...",
"#...##....#",
".#..#...#.#" };
            //*/

            return lines;
        }

        public override void Run()
        {
            string[] data = getData();

            int len = data[0].Length;

            Console.WriteLine(" Hit " + findslope(data, 3, 1));

            long value1 = findslope(data, 1, 1);
            long value2 = findslope(data, 3, 1);
            long value3 = findslope(data, 5, 1);
            long value4 = findslope(data, 7, 1);
            long value5 = findslope(data, 1, 2);
            Console.WriteLine(" value: " + (value1*value2* value3 * value4 * value5 ));


            int trees = int.MaxValue;
            int bestx = 0;
            int besty = 0;
            for (int y = 1; y < 100; y++)
            {
                for (int x = 0; x < len; x++)
                {
                    
                    int val = findslope(data, x, y);
                    if (val < trees)
                    {
                        bestx = x;
                        besty = y;
                        trees = val;
                        Console.WriteLine(String.Format("A slope of {0}, {1} gives {2}", x, y, val));
                    }
                }
            }
            Console.WriteLine("Done!");

        }

        public int findslope(string[] data, int moveX, int moveY)
        {
            int len = data[0].Length;
            int trees = 0;

            int x = 0, y = 0;

            while (y < data.Length)
            {
                if (data[y].ToCharArray()[x] == '#')
                {
                    trees++;
                }

                x += moveX;
                x = x % len;
                y += moveY;
            }

            return trees;
        }
    }
}
