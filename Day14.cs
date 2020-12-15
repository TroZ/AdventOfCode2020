using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day14 : Day
    {
        String[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
//            string[] lines = {
//"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
//"mem[8] = 11",
//"mem[7] = 101",
//"mem[8] = 0" };


            string[] lines = {
"mask = 000000000000000000000000000000X1001X",
"mem[42] = 100",
"mask = 00000000000000000000000000000000X0XX",
"mem[26] = 1" };

            //*/

            return lines;
        }

        readonly static Regex assignRegex = new Regex(@"^mem\[(\d*)\] = (\d*)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        Dictionary<long, long> mem = new Dictionary<long, long>();

        public override void Run()
        {
            string[] data = getData();
/*
            long and = 0x000fffff;
            long or = 0;

            foreach (string line in data)
            {

                MatchCollection mc = assignRegex.Matches(line);
                if (mc.Count > 0)
                {
                    int loc = int.Parse(mc[0].Groups[1].Value);
                    long val = long.Parse(mc[0].Groups[2].Value);

                    val &= and;
                    val |= or;

                    mem[loc] = val;
                }
                else
                {
                    //must be mask set
                    if(line.StartsWith("mask = "))
                    {
                        string mask = line.Substring(7);

                        and = 0;
                        or = 0;
                        foreach(char c in mask)
                        {
                            or = or << 1;
                            and = and << 1;
                            switch (c)
                            {
                                case '1':
                                    or |= 1;
                                    and |= 1;
                                    break;
                                case '0':
                                    break;
                                case 'X':
                                    and |= 1;
                                    break;
                            }
                        }
                    }
                }
            }

            //done - now sum

            long total = 0;
            foreach(var memloc in mem)
            {
                total += memloc.Value;
            }

            Console.WriteLine("Total mem: " + total);
*/

            //part 2
            mem.Clear();
            string addrmask = "";
            foreach (string line in data)
            {

                MatchCollection mc = assignRegex.Matches(line);
                if (mc.Count > 0)
                {
                    int loc = int.Parse(mc[0].Groups[1].Value);
                    long val = long.Parse(mc[0].Groups[2].Value);


                    write(loc, val, addrmask, 0);
                }
                else
                {
                    //must be mask set
                    if (line.StartsWith("mask = "))
                    {
                        addrmask = line.Substring(7);

                    }
                }
            }

            //done - now sum

            long total = 0;
            foreach (var memloc in mem)
            {
                total += memloc.Value;
            }

            Console.WriteLine("Total mem 2: " + total);
        }

        void write(long loc, long val, string addrmask, int pos)
        {
            //Console.WriteLine("loc = " + Convert.ToString(loc, 2).ToString());

            if (pos == 36)
            {
                mem[loc] = val;
            }
            else
            {
                int strpos = 35 - pos;
                if(addrmask[strpos] == 'X')
                {
                    //Console.WriteLine("loc = " + Convert.ToString((~(1 << (pos))), 2).ToString());
                    loc &= (~(1L << (pos)));
                    write(loc, val, addrmask, pos + 1);
                    loc |= 1L << (pos);
                    write(loc, val, addrmask, pos + 1);
                }
                if(addrmask[strpos] == '1')
                {
                    loc |= 1L << (pos);
                    write(loc, val, addrmask, pos + 1);
                }
                if(addrmask[strpos] == '0'){
                    write(loc, val, addrmask, pos + 1);
                }
            }
        }
    }
}