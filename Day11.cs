using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day11 : Day
    {
        public string[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = {
"L.LL.LL.LL",
"LLLLLLL.LL",
"L.L.L..L..",
"LLLL.LL.LL",
"L.LL.LL.LL",
"L.LLLLL.LL",
"..L.L.....",
"LLLLLLLLLL",
"L.LLLLLL.L",
"L.LLLLL.LL" };
            //*/


            return lines;
        }

        public override void Run()
        {
            string[] data = getData();


            char[,] src = new char[data[0].Length, data.Length];
            char[,] dest = new char[data[0].Length, data.Length];

            //setup data
            for (int y = 0; y < data.Length; y++)
            {
                for (int x = 0; x < data[0].Length; x++)
                {
                    src[x, y] = data[y][x];
                }
            }



            bool notsame = true;
            //run
            while (notsame)
            {

                step(src, dest);

                notsame = !isSame(src, dest);

                //swap arrays
                char[,] tmp = src;
                src = dest;
                dest = tmp;

                //print(src);
            }


            int count = getCount(src);

            Console.WriteLine("Count: " + count);




            //part 2
            //setup data
            for (int y = 0; y < data.Length; y++)
            {
                for (int x = 0; x < data[0].Length; x++)
                {
                    src[x, y] = data[y][x];
                }
            }


            notsame = true;
            //run 2
            while (notsame)
            {

                step2(src, dest);

                notsame = !isSame(src, dest);

                //swap arrays
                char[,] tmp = src;
                src = dest;
                dest = tmp;

                //print(src);
            }

            count = getCount(src);

            Console.WriteLine("Count 2: " + count);
        }

        void step(char[,] src, char[,] dest)
        {

            for (int y = 0; y < src.GetLength(1); y++)
            {
                for (int x = 0; x < src.GetLength(0); x++)
                {
                    dest[x, y] = src[x, y];
                    if (src[x, y] != '.')
                    {
                        int count = getCountOccupied(src, x, y);
                        if (count == 0 && src[x, y] == 'L')
                        {
                            dest[x, y] = '#';
                        }
                        if (count > 3 && src[x, y] == '#')
                        {
                            dest[x, y] = 'L';
                        }
                    }

                }
            }

        }

        int getCountOccupied(char[,] src, int x, int y)
        {
            int count = 0;
            for (int yy = y - 1; yy < y + 2; yy++)
            {
                for (int xx = x - 1; xx < x + 2; xx++)
                {
                    if ((!(xx == x && yy == y)) && xx > -1 && xx < src.GetLength(0) && yy > -1 && yy < src.GetLength(1) && src[xx, yy] == '#')
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        bool isSame(char[,] src, char[,] dest)
        {

            for (int y = 0; y < src.GetLength(1); y++)
            {
                for (int x = 0; x < src.GetLength(0); x++)
                {
                    if (src[x, y] != dest[x, y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        int getCount(char[,] src)
        {
            int count = 0;
            for (int y = 0; y < src.GetLength(1); y++)
            {
                for (int x = 0; x < src.GetLength(0); x++)
                {
                    if (src[x, y] == '#')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        void print(char[,] src)
        {

            for (int y = 0; y < src.GetLength(1); y++)
            {
                for (int x = 0; x < src.GetLength(0); x++)
                {
                    Console.Write(src[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }



        void step2(char[,] src, char[,] dest)
        {

            for (int y = 0; y < src.GetLength(1); y++)
            {
                for (int x = 0; x < src.GetLength(0); x++)
                {
                    dest[x, y] = src[x, y];
                    if (src[x, y] != '.')
                    {
                        int count = getCountOccupied2(src, x, y);
                        if (count == 0 && src[x, y] == 'L')
                        {
                            dest[x, y] = '#';
                        }
                        if (count > 4 && src[x, y] == '#')
                        {
                            dest[x, y] = 'L';
                        }
                    }

                }
            }

        }

        int getCountOccupied2(char[,] src, int x, int y)
        {
            int count = 0;
            for (int yy = -1; yy < 2; yy++)
            {
                for (int xx = -1; xx < 2; xx++)
                {
                    if(xx == 0 && yy == 0)
                        continue;

                    int xxx = x;
                    int yyy = y;
                    bool ok = true;
                    while (ok)
                    {
                        xxx += xx;
                        yyy += yy;
                        if ( xxx > -1 && xxx < src.GetLength(0) && yyy > -1 && yyy < src.GetLength(1) )
                        {
                            if (src[xxx, yyy] != '.')
                            {
                                //found a chair
                                ok = false;
                                if (src[xxx, yyy] == '#')
                                {
                                    //occupied
                                    count++;
                                }
                            }
                        }
                        else
                        {
                            // went off edge - not occupied
                            ok = false;
                        }
                    }
                }
            }
            return count;
        }
    }
}
