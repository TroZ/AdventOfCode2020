using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day8 : Day
    {
        String[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = {
"nop +0",
"acc +1",
"jmp +4",
"acc +3",
"jmp -3",
"acc -99",
"acc +1",
"jmp -4",
"acc +6" };
            //*/

            return lines;
        }

        int acc = 0;
        int pc = 0;
        HashSet<int> ran = new HashSet<int>();

        public override void Run()
        {
            string[] data = getData();


            bool ok = true;
            while (ok)
            {

                if (ran.Contains(pc))
                {
                    ok = false;
                    break;
                }

                //remember we ran this line
                ran.Add(pc);

                if(pc > data.Length)
                {
                    break;
                }


                string[] line = data[pc].Split(' ');
                int val = int.Parse(line[1]);
                switch (line[0])
                {
                    case "nop":
                        pc++;
                        break;
                    case "acc":
                        pc++;
                        acc += val;
                        break;
                    case "jmp":
                        pc += val;
                        break;
                }

                
            }
            Console.WriteLine("Abbout to run line " + pc + " again. ACC: " + acc);



            for(int i = 0; i < data.Length; i++)
            {
                if (data[i].StartsWith("acc"))
                    continue;

                //switch line
                string orig = data[i];
                string[] line = data[i].Split(' ');
                if(line[0] == "nop")
                {
                    line[0] = "jmp";
                }
                else
                {
                    line[0] = "nop";
                }
                data[i] = line[0] + " " + line[1];

                //see if it works
                if (exec(data))
                {
                    Console.WriteLine("Success! Changed line " + i + " to '" + data[i] + ".  ACC:" + acc);
                }

                //now put it back
                data[i] = orig;
            }
        }

        public bool exec(string[] data)
        {
            pc = 0;
            acc = 0;
            ran.Clear();

            bool ok = true;
            while (ok)
            {

                if (ran.Contains(pc))
                {
                    ok = false;
                    break;
                }

                //remember we ran this line
                ran.Add(pc);

                if (pc >= data.Length)
                {
                    break;
                }


                string[] line = data[pc].Split(' ');
                int val = int.Parse(line[1]);
                switch (line[0])
                {
                    case "nop":
                        pc++;
                        break;
                    case "acc":
                        pc++;
                        acc += val;
                        break;
                    case "jmp":
                        pc += val;
                        break;
                }


            }

            return ok;
        }

    }
}
