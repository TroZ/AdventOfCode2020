using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day5 : Day
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

        public override void Run()
        {
            string[] data = getData();

            int maxseat = 0;
            

            foreach(string seat in data)
            {
                int id = getSeatID(seat);
                Console.WriteLine("Seat " + seat + "  id: " + id);

                if (id > maxseat)
                {
                    maxseat = id;
                }
            }

            Console.WriteLine("max seat: " + maxseat);
            Console.WriteLine();

            bool[] seats = new bool[maxseat + 1];

            foreach (string seat in data)
            {
                int id = getSeatID(seat);
                seats[id] = true;
            }

            for(int i = 1; i < maxseat; i++)
            {
                if(seats[i] == false && seats[i-1] == true && seats[i+1] == true)
                {
                    Console.WriteLine(" your seat " + i);
                }
            }
        }

        private int getSeatID(string seat)
        {
            string rowstr = seat.Substring(0, 7);
            string colstr = seat.Substring(7);

            rowstr = rowstr.Replace('F', '0').Replace('B', '1');
            int row = Convert.ToInt32(rowstr, 2);

            colstr = colstr.Replace('L', '0').Replace('R', '1');
            int col = Convert.ToInt32(colstr, 2);

            int id = row * 8 + col;

            int seatid = Convert.ToInt32(seat.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2); 
            return id;
        }
    }
}
