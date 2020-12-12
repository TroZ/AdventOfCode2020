using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day12 : Day
    {
        String[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = {
"F10",
"N3",
"F7",
"R90",
"F11" };
            //*/

            return lines;
        }

        public override void Run()
        {
            string[] data = getData();

            int x = 0;int y = 0;
            int facing = 0;

            foreach(string line in data)
            {
                char command = line[0];
                int value = int.Parse(line.Substring(1));

                switch (command)
                {
                    case 'N':
                        y -= value;
                        break;
                    case 'S':
                        y += value;
                        break;
                    case 'E':
                        x += value;
                        break;
                    case 'W':
                        x -= value;
                        break;
                    case 'R':
                        facing += value;
                        facing %= 360;
                        break;
                    case 'L':
                        facing += (360 - value);
                        facing %= 360;
                        break;
                    case 'F':
                        switch (facing)
                        {
                            case 0:
                                x += value;
                                break;
                            case 90:
                                y += value;
                                break;
                            case 180:
                                x -= value;
                                break;
                            case 270:
                                y -= value;
                                break;
                            default:
                                Console.WriteLine("ERROR in facing");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("ERROR in command");
                        break;
                }
            }

            int mdist = Math.Abs(x) + Math.Abs(y);

            Console.WriteLine("Distance: " + mdist);


            //part 2
            x = 0;
            y = 0;
            int wx = 10;
            int wy = -1;
            foreach (string line in data)
            {
                char command = line[0];
                int value = int.Parse(line.Substring(1));
                switch (command)
                {
                    case 'N':
                        wy -= value;
                        break;
                    case 'S':
                        wy += value;
                        break;
                    case 'E':
                        wx += value;
                        break;
                    case 'W':
                        wx -= value;
                        break;
                    case 'R':
                        while(value > 0)
                        {
                            int tmp = wx;
                            wx = -wy;
                            wy = tmp;
                            value -= 90;
                        }
                        
                        break;
                    case 'L':
                        while (value > 0)
                        {
                            int tmp = wx;
                            wx = wy;
                            wy = -tmp;
                            value -= 90;
                        }
                        break;
                    case 'F':
                        x += (wx * value);
                        y += (wy * value);
                        break;
                    default:
                        Console.WriteLine("ERROR in command");
                        break;
                }
            }

            mdist = Math.Abs(x) + Math.Abs(y);

            Console.WriteLine("Distance 2: " + mdist);
        }
    }

        
}
