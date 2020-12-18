using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day18 : Day
    {
        String[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = {
"1 + 2 * 3 + 4 * 5 + 6",
"1 + (2 * 3) + (4 * (5 + 6))",
"2 * 3 + (4 * 5)",
"5 + (8 * 3 + 9 + 3 * 4 * 3)",
"5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))",
"((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"};


            //string[] lines = { "0,3,6" };

                //*/

            return lines;
        }


        public override void Run()
        {
            string[] data = getData();


            long total = 0;

            foreach(string equ in data)
            {
                long val = solve(equ);
                total += val;
            }

            Console.WriteLine("Total is: " + total);


            //part2
            total = 0;
            foreach (string equ in data)
            {
                long val = solve2(equ);
                total += val;
            }
            Console.WriteLine("Total2 is: " + total);

        }


        long solve(string equ)
        {

            while (equ.Contains('('))
            {
                //find matching 
                int count = 1;
                int start = equ.IndexOf('(');
                int end = -1;
                for(int i = start+1; i < equ.Length; i++)
                {
                    char c = equ[i];
                    switch (c)
                    {
                        case '(':
                            count++;
                            break;
                        case ')':
                            count--;
                            break;
                    }
                    if(count == 0)
                    {
                        end = i;
                        string subequ = equ.Substring(start+1, end - start-1);
                        long v = solve(subequ);
                        equ = equ.Substring(0, start) + v.ToString() + equ.Substring(end+1);
                        break;
                    }

                }
            }
            
            string[] parts = equ.Split(' ');

            long val = long.Parse(parts[0]);
            char oper = '+';

            for(int i = 1; i < parts.Length; i++)
            {
                if (i % 2 == 1)
                {
                    //operation
                    oper = parts[i][0];
                }
                else
                {
                    long val2 = long.Parse(parts[i]);
                    switch (oper)
                    {
                        case '+':
                            val += val2;
                            break;
                        case '*':
                            val *= val2;
                            break;
                    }
                }

            }


            return val;
        }


        long solve2(string equ)
        {

            while (equ.Contains('('))
            {
                //find matching 
                int count = 1;
                int start = equ.IndexOf('(');
                int end = -1;
                for (int i = start + 1; i < equ.Length; i++)
                {
                    char c = equ[i];
                    switch (c)
                    {
                        case '(':
                            count++;
                            break;
                        case ')':
                            count--;
                            break;
                    }
                    if (count == 0)
                    {
                        end = i;
                        string subequ = equ.Substring(start + 1, end - start - 1);
                        long v = solve2(subequ);
                        equ = equ.Substring(0, start) + v.ToString() + equ.Substring(end + 1);
                        break;
                    }

                }
            }

            //add
            while (equ.Contains('+'))
            {
                int pos = equ.IndexOf('+');
                int start = pos - 2;
                int end = pos + 2;
                while(start>0 && equ[start] != ' ')
                {
                    start--;
                }
                while(end<equ.Length && equ[end] != ' ')
                {
                    end++;
                }

                string add = equ.Substring(start, end - start).Trim();
                string[] a = add.Split('+');
                long v = long.Parse(a[0]) + long.Parse(a[1]);

                equ = equ.Substring(0, start) + " " + v.ToString()+" " + equ.Substring(end);
                equ = equ.Replace("  ", " ").Trim();
            }

            //multiply
            string[] parts = equ.Split('*');
            long val = long.Parse(parts[0]);
            for(int i = 1; i < parts.Length; i++)
            {
                long val2 = long.Parse(parts[i]);
                val *= val2;
            }

            return val;
        }
    }
}
