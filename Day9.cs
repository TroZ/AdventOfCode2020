using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day9 : Day
    {
        long[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = { "35","20","15","25","47","40","62","55","65","95","102","117","150","182","127","219","299","277","309","576" };
            //*/


            long[] nums = new long[lines.Length];
            for(int i = 0; i < lines.Length; i++)
            {
                nums[i] = long.Parse(lines[i]);
            }

            return nums;
        }

        int preamble = 25;//5;//


        public override void Run()
        {
            long[] data = getData();


            long cur = 0;
            int pos = 0;
            bool found = false;
            for (int i = preamble; i < data.Length; i++)
            {
                cur = data[i];

                found = false;
                for (int j = i - preamble; j < i; j++)
                {
                    for( int k = j + 1; k < i; k++)
                    {
                        if(data[j] + data[k] == cur)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found == true)
                    {
                        break;
                    }
                }

                if (!found)
                {
                    pos = i;
                    break;
                }
            }

            Console.WriteLine("Bad Num at pos " + pos + " : " + cur);

            long findval = cur;
            found = false;
            long min = long.MaxValue;
            long max = long.MinValue;
            int pos1 = 0, pos2 = 0;
            long result = 0;
            for (int i = 0; i < data.Length; i++)
            {
                for(int j = i + 2; j < data.Length; j++)
                {

                    long sum = 0;
                    for (int k = i; k < j; k++)
                    {
                        sum += data[k];
                    }

                    if(sum == findval)
                    {
                        found = true;
                        pos1 = i;
                        pos2 = j - 1;
                        for (int k = i; k < j; k++)
                        {
                            min = Math.Min(min, data[k]);
                            max = Math.Max(max, data[k]);
                        }
                        result = min + max;
                        break;
                    }
                    else
                    {
                        if(sum > findval)  //bigger than val we are looking for, so adding more numbers to this list wont work
                        {
                            break;
                        }
                    }

                }

                if (found)
                {
                    break;
                }
            }

            Console.WriteLine("Set is from " + pos1 + " to " + pos2 + ", sum of min+max = " + (min + max));
        }
    }
}
