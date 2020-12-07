using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day1 : Day
    {

        int[] getData()
        {
            //*
            string[] lines = Program.readFile(1);
            /*/
            string[] lines = { "1721","979","366","299","675","1456" };
            //*/
            int[] data = new int[lines.Length];
            for (int i=0; i < lines.Length; i++)
            {
                data[i] = int.Parse(lines[i]);
            }
            return data;
        }

        public override void Run()
        {
            Console.WriteLine("Do Something!");

            int[] data = getData();

            {
                for (int i = 0; i < data.Length; i++)
                {
                    for (int j = i + 1; j < data.Length; j++)
                    {
                        if (data[i] + data[j] == 2020)
                        {

                            Console.WriteLine(String.Format("Numbers {0} and {1}, product: {2}", data[i], data[j], data[i] * data[j]));

                        }
                    }
                }
            }


            for (int i = 0; i < data.Length; i++)
            {
                for (int j = i + 1; j < data.Length; j++)
                {
                    for (int k = j + 1; k < data.Length; k++)
                    {
                        if (data[i] + data[j] + data[k] == 2020)
                        {

                            Console.WriteLine(String.Format("Numbers {0}, {1} and {2}, product: {3}", data[i], data[j], data[k], data[i] * data[j] * data[k]));

                        }
                    }
                }
            }
        }

        


    }
}
