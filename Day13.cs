using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day13 : Day
    {
        String[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = {
"939",
"7,13,x,x,59,x,31,19" };
            //*/

            return lines;
        }

        public override void Run()
        {
            string[] data = getData();


            int time = int.Parse(data[0]);
            string[] busses = data[1].Split(',');

            int bestTime = int.MaxValue;
            int bestId = 0;

            foreach(string bus in busses)
            {
                if (bus == "x")
                    continue;

                int id = int.Parse(bus);
                int btime = id - (time % id);

                if(btime < bestTime)
                {
                    bestTime = btime;
                    bestId = id;
                }
            }


            Console.WriteLine(" Best bus is " + bestId + " which leaves in " + bestTime + " minutes.  Score: " + (bestId * bestTime));


            int[] bids = new int[busses.Length];
            for(int i = 0; i < bids.Length; i++)
            {
                if (busses[i] == "x")
                    bids[i] = 1;
                else
                    bids[i] = int.Parse(busses[i]);
            }



            /*
            long testtime = 0;
            bool found = false;
            while (!found)
            {
                found = test(testtime, bids);
                if (!found)
                {
                    testtime += bids[0];
                }
            }

            //this ran for over 90 minutes, and mad it to 1260288915887 before I gave up.
            */


            //better version
            long testtime = 0;
            int pos = 0;
            long s = 1;
            while(pos < bids.Length)
            {
                if((testtime + pos) % bids[pos] == 0)
                {
                    s *= bids[pos];
                    pos++;
                }
                else
                {
                    testtime += s;
                }
            }




            Console.WriteLine("Time for leave in order: " + testtime);

        }

        bool test(long testtime, int[] bids)
        {
            for(int i = 0; i < bids.Length; i++)
            {
                if(((testtime + i) % bids[i]) != 0)
                {
                    return false;
                }
            }


            return true;
        }
    }
}
