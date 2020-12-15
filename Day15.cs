using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day15 : Day
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


            string[] lines = { "0,3,6" };

            //*/

            return lines;
        }

        List<int> numbers = new List<int>();
        Dictionary<int, numLoc> numsDic = new Dictionary<int, numLoc>();

        public override void Run()
        {
            string[] data = getData();

            string[] nums = data[0].Split(',');


            foreach( string num in nums)
            {
                numbers.Add(int.Parse(num));
            }

            numbergame(2020);

            Console.WriteLine("2020th number is: " + numbers[2020-1]);//-1 because 0 based

            //numbergame(30000000);

            int count = 0;
            int last = 0;
            foreach (string num in nums)
            {
                last = int.Parse(num);
                numLoc loc = new numLoc();
                loc.last = count;
                numsDic.Add(last, loc);
                count++;
            }
            int val = numbergame2(30000000, count,last);


            Console.WriteLine("30000000 number is: " + val);//-1 because 0 based
        }



        void numbergame(int len)
        {
            while(numbers.Count < len)
            {
                int last = numbers[numbers.Count - 1];
                int prev = -1;
                for(int i = numbers.Count - 2; i > -1; i--)
                {
                    if(numbers[i] == last)
                    {
                        prev = i;
                        break;
                    }
                }
                if (prev > -1)
                {
                    int dist = numbers.Count - 1 - prev;
                    numbers.Add(dist);
                }
                else
                {
                    numbers.Add(0);
                }


            }
        }

        int numbergame2(int len, int count, int last)
        {
            while (count < len)
            {
                numLoc loc = numsDic[last];
                if(loc.prev == -1)
                {
                    //first time
                    last = 0;
                    loc = numsDic[last];
                    loc.prev = loc.last;
                    loc.last = count;
                    numsDic[last] = loc;
                }
                else
                {
                    int dist = loc.last - loc.prev;
                    if (!numsDic.ContainsKey(dist))
                    {
                        //new number - add to dictionary
                        loc = new numLoc();
                        loc.last = count;
                        numsDic.Add(dist, loc);
                    }
                    else
                    {
                        //already seen
                        loc = numsDic[dist];
                        loc.prev = loc.last;
                        loc.last = count;
                        numsDic[dist] = loc;
                    }
                    last = dist;
                }

                count++;
            }


            return last;
        }

        class numLoc
        {
            public int last = -1;
            public int prev = -1;
        }
    }
}
