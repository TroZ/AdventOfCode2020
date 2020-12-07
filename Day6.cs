using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day6 : Day
    {
        String[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = { "BFFFBBFRRR",
"FFFBBBFRRR",
"BBFFBBFRLL" };
            //*/

            return lines;
        }

        int total = 0;
        bool[] group = new bool[26];

        public override void Run()
        {

            
            string[] data = getData();

            

            foreach(string line in data)
            {
                if (line.Length < 1)
                {
                    addData();
                }
                else
                {
                    foreach(char c in line)
                    {
                        group[c - 'a'] = true;
                    }
                }

            }
            addData();

            Console.WriteLine(" total counts: " + total);

            //part 2
            total = 0;
            fill();
            foreach (string line in data)
            {
                if (line.Length < 1)
                {
                    addData();
                    fill();
                }
                else
                {
                    for(char c = 'a'; c<='z'; c++)
                    {
                        if (!line.Contains(c))
                        {
                            group[c - 'a'] = false;
                        }
                    }
                }

            }
            addData();

            Console.WriteLine(" all total counts: " + total);
        }

        public void addData()
        {
            foreach(bool b in group)
            {
                if (b)
                {
                    total++;
                }
            }
            for (int i= 0; i < group.Length; i++)
            {
                group[i] = false;
            }
        }

        public void fill()
        {
            for (int i = 0; i < group.Length; i++)
            {
                group[i] = true;
            }
        }
    }
}
