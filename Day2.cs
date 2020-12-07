using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day2 : Day
    {

        String[] getData()
        {
            
            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = { "1-3 a: abcde","1-3 b: cdefg","2-9 c: ccccccccc" };
            //*/

            return lines;
        }

        public override void Run()
        {
            string[] data = getData();

            int good = 0;

            for(int i = 0; i < data.Length; i++)
            {
                string line = data[i];
                char letter =' ';
                int min = 0, max = 0;
                string password = "";

                string[] tmp = line.Split(" ");
                string[] minmax = tmp[0].Split("-");
                min = int.Parse(minmax[0]);
                max = int.Parse(minmax[1]);
                letter = tmp[1].ToCharArray()[0];
                password = tmp[2];

                int charcount = 0;
                for(int j = 0; j < password.Length; j++)
                {
                    if (password[j] == letter)
                    {
                        charcount++;
                    }
                }

                if(charcount>=min && charcount <= max)
                {
                    good++;
                }
            }

            Console.WriteLine("Good Passwords: " + good);

            int good2 = 0;

            for (int i = 0; i < data.Length; i++)
            {
                string line = data[i];
                char letter = ' ';
                int min = 0, max = 0;
                string password = "";

                string[] tmp = line.Split(" ");
                string[] minmax = tmp[0].Split("-");
                min = int.Parse(minmax[0]);
                max = int.Parse(minmax[1]);
                letter = tmp[1].ToCharArray()[0];
                password = tmp[2];

                if((password[min-1]==letter || password[max-1]==letter) && !(password[min - 1] == letter && password[max - 1] == letter)){
                    good2++;
                }

            }

            Console.WriteLine("Good Passwords v2: " + good2);
        }
    }
}
