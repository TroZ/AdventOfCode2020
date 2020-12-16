using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day16 : Day
    {
        String[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = {
//"class: 1-3 or 5-7",
//"row: 6-11 or 33-44",
//"seat: 13-40 or 45-50",
//"",
//"your ticket:",
//"7,1,14",
//"",
//"nearby tickets:",
//"7,3,47",
//"40,4,50",
//"55,2,20",
//"38,6,12" };


            //string[] lines = { "0,3,6" };

            //*/

            return lines;
        }

        List<int> validNums = new List<int>();

        class Field
        {
            public string name;
            public List<int> valid = new List<int>();
            public int pos;
        }
        List<Field> fields = new List<Field>();
        readonly static Regex rxField = new Regex(@"^([^:]*): (\d*)-(\d*) or (\d*)-(\d*)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        int[] yourticket = new int[0];

        List<int[]> validTickets = new List<int[]>();

        public override void Run()
        {
            string[] data = getData();

            int state = 0;

            int errorRate = 0;

            foreach(string line in data)
            {
                switch (state)
                {
                    case 0: //fields
                        MatchCollection mc = rxField.Matches(line);
                        if (mc.Count > 0)
                        {
                            Field field = new Field();
                            field.name = mc[0].Groups[1].Value;
                            int min = int.Parse(mc[0].Groups[2].Value);
                            int max = int.Parse(mc[0].Groups[3].Value);
                            for(int i = min; i <= max; i++) {
                                field.valid.Add(i);
                                if (!validNums.Contains(i))
                                {
                                    validNums.Add(i);
                                }
                            }
                            min = int.Parse(mc[0].Groups[4].Value);
                            max = int.Parse(mc[0].Groups[5].Value);
                            for (int i = min; i <= max; i++)
                            {
                                field.valid.Add(i);
                                if (!validNums.Contains(i))
                                {
                                    validNums.Add(i);
                                }
                            }
                            fields.Add(field);
                        }
                        if(line == "your ticket:")
                        {
                            state = 1;
                        }
                        break;
                    case 1:
                        if (line == "nearby tickets:")
                        {
                            state = 2;
                        }
                        else if (line.Length > 1)
                        {
                            string[] your = line.Split(',');
                            yourticket = new int[your.Length];
                            for (int i = 0; i < your.Length; i++)
                            {
                                yourticket[i] = int.Parse(your[i]);
                            }
                        }
                        
                        break;
                    case 2:
                        //part 1
                        if (line.Length > 1)
                        {
                            bool ok = true;
                            string[] ticket = line.Split(',');
                            int[] tkt = new int[ticket.Length];
                            for(int i=0;i< ticket.Length;i++)
                            {
                                int n = int.Parse(ticket[i]);
                                if (!validNums.Contains(n))
                                {
                                    errorRate += n;
                                    ok = false;
                                }
                                tkt[i] = n;
                            }

                            if (ok)
                            {
                                validTickets.Add(tkt);
                            }
                        }
                        break;
                }
            }


            Console.WriteLine("Error Rate: " + errorRate);


            //part 2
            bool[,] canBe = new bool[fields.Count, fields.Count];
            for (int f = 0; f < fields.Count; f++)
            {
                for (int pos = 0; pos < fields.Count; pos++)
                {
                    canBe[f, pos] = true;
                }
            }

            //eliminate fields that can't be
            for (int i = 0; i < validTickets.Count; i++)
            {
                for(int f = 0; f < fields.Count; f++)
                {
                    for(int pos = 0; pos < fields.Count; pos++)
                    {

                        int[] ticket = validTickets[i];

                        if (!fields[f].valid.Contains(ticket[pos]))
                        {
                            canBe[f, pos] = false;
                        }
                    }
                }
            }

            //at the point, each field should only have one true in canBe
            //Actually, this isn't true. However, one field will have one possibility, one field will have two possibilities, one will have three, etc.
            //This will print the possibilities, and I manually figured out the fields from this initially.  See below for code that does this.
            for (int f = 0; f < fields.Count; f++)
            {
                Console.Write("Field " + fields[f].name + " is position ");
                int count = 0;
                for (int pos = 0; pos < fields.Count; pos++)
                {
                    if(canBe[f, pos] == true)
                    {
                        Console.Write(" " + pos);
                        count++;
                    }
                }
                Console.WriteLine(" (" + count+")");
            }



            Console.WriteLine("\n\n Your Ticket:");
            for(int i = 0; i < fields.Count; i++)
            {
                Console.Write("" + i + ": " + yourticket[i] + "  ");
            }
            Console.WriteLine();

            //columns figured out from examining the data printed out above
            //THIS ONLY WORKS BECAUSE I MANUALLY FIGURED OUT THE CORRECT FIELDS FOR MY DATA
            long value = (long)(yourticket[6]) * yourticket[10] * yourticket[11] * yourticket[13] * yourticket[16] * yourticket[19];

            Console.WriteLine("\n MANUAL VALUE: "+value);




            //now try to finish off part 2 for real (using code).
            //find the field that can be only one position, use that to remove possibilities
            Field single = null;
            Field next = null;
            for (int f = 0; f < fields.Count; f++)
            {
                int count = 0;
                int realpos = 0;
                for (int pos = 0; pos < fields.Count; pos++)
                {
                    if (canBe[f, pos] == true)
                    {
                        count++;
                        realpos = pos;
                    }
                }
                if(count == 1)
                {
                    next = fields[f];
                    next.pos = realpos;
                }
            }
            while(next != null)
            {
                single = next;
                next = null;

                for (int f = 0; f < fields.Count; f++)
                {
                    //remove the field we just figure out from the possibilities of other fields
                    canBe[f, single.pos] = false;

                    int count = 0;
                    int realpos = 0;
                    for (int pos = 0; pos < fields.Count; pos++)
                    {
                        if (canBe[f, pos] == true)
                        {
                            count++;
                            realpos = pos;
                        }
                    }
                    if (count == 1)
                    {
                        //we just figured out the next field
                        next = fields[f];
                        next.pos = realpos;
                    }
                }
            }

            //at this point, each field should have is .pos filled out;
            long total = 1;
            Console.WriteLine("\n\n\n Your Ticket:");
            for (int f = 0; f < fields.Count; f++)
            {
                Console.WriteLine("" + fields[f].name + ": " + yourticket[fields[f].pos]+"\t\tField "+ fields[f].pos);
                if (fields[f].name.StartsWith("departure")){
                    total *= yourticket[fields[f].pos];
                }
            }
            Console.WriteLine(" TOTAL Departure: " + total);
        }


    }
}
