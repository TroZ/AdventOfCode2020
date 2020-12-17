using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day17 : Day
    {
        String[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = {
".#.",
"..#",
"###" };


            //string[] lines = { "0,3,6" };

                //*/

            return lines;
        }


        Dictionary<int[], bool> dimension = new Dictionary<int[], bool>(new MyEqualityComparer());


        public override void Run()
        {
            string[] data = getData();

            int z = 0;
            for (int y = 0; y < data.Length; y++)
            {
                for(int x = 0; x < data[y].Length; x++)
                {
                    if(data[y][x] == '#')
                    {
                        int[] pos = new int[3];
                        pos[0] = x; pos[1] = y; pos[2] = z;
                        dimension.Add(pos, true);
                    }
                }
            }



            for(int i = 0; i < 6; i++)
            {
                Dictionary<int[], bool> next = calcNext(dimension);
                dimension = next;
            }

            Console.WriteLine(" Number active: " + dimension.Count);


            // 4 d
            dimension.Clear();
            z = 0;
            int w = 0;
            for (int y = 0; y < data.Length; y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (data[y][x] == '#')
                    {
                        int[] pos = new int[4];
                        pos[0] = x; pos[1] = y; pos[2] = z; pos[3] = w;
                        dimension.Add(pos, true);
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                Dictionary<int[], bool> next = calcNext4(dimension);
                dimension = next;
            }

            Console.WriteLine(" Number active (4d): " + dimension.Count);
        }

        Dictionary<int[], bool> calcNext(Dictionary<int[], bool> dimension)
        {
            Dictionary<int[], bool> next = new Dictionary<int[], bool>(new MyEqualityComparer());

            int minx = 0;
            int maxx = 0;
            int miny = 0;
            int maxy = 0;
            int minz = 0;
            int maxz = 0;

            foreach(int[] key in dimension.Keys)
            {
                minx = Math.Min(minx, key[0]);
                maxx = Math.Max(maxx, key[0]);
                miny = Math.Min(miny, key[1]);
                maxy = Math.Max(maxy, key[1]);
                minz = Math.Min(minz, key[2]);
                maxz = Math.Max(maxz, key[2]);
            }

            for(int zz = minz - 1; zz < maxz + 2; zz++)
            {
                for (int yy = miny - 1; yy < maxy + 2; yy++)
                {
                    for (int xx = minx - 1; xx < maxx + 2; xx++)
                    {
                        bool active = calcPos(dimension, xx, yy, zz);
                        if (active)
                        {
                            int[] pos = new int[3];
                            pos[0] = xx; pos[1] = yy; pos[2] = zz;
                            next.Add(pos, active);
                        }
                    }
                }
            }
            return next;
        }

        bool calcPos(Dictionary<int[], bool> dimension, int x, int y, int z)
        {
            int count = 0;
            bool alive = false;
            int[] pos = new int[3];
            pos[0] = x; pos[1] = y; pos[2] = z;
            if (dimension.ContainsKey(pos))
                alive = true;
            for(int xx = -1; xx < 2; xx++)
            {
                for (int yy = -1; yy < 2; yy++)
                {
                    for (int zz = -1; zz < 2; zz++)
                    {
                        if (xx == 0 && yy == 0 && zz == 0)
                            continue;
                        pos[0] = x+xx; pos[1] = y+yy; pos[2] = z+zz;
                        if (dimension.ContainsKey(pos))
                        {
                            count++;
                        }
                    }
                }
            }

            if(alive && (count == 2 || count == 3))
            {
                return true;
            }
            if(!alive && count == 3)
            {
                return true;
            }
            return false;
        }

        Dictionary<int[], bool> calcNext4(Dictionary<int[], bool> dimension)
        {
            Dictionary<int[], bool> next = new Dictionary<int[], bool>(new MyEqualityComparer());

            int minw = 0;
            int maxw = 0;
            int minx = 0;
            int maxx = 0;
            int miny = 0;
            int maxy = 0;
            int minz = 0;
            int maxz = 0;

            foreach (int[] key in dimension.Keys)
            {
                minx = Math.Min(minx, key[0]);
                maxx = Math.Max(maxx, key[0]);
                miny = Math.Min(miny, key[1]);
                maxy = Math.Max(maxy, key[1]);
                minz = Math.Min(minz, key[2]);
                maxz = Math.Max(maxz, key[2]);
                minw = Math.Min(minz, key[3]);
                maxw = Math.Max(maxz, key[3]);
            }

            for (int ww = minw - 1; ww < maxw + 2; ww++)
            {
                for (int zz = minz - 1; zz < maxz + 2; zz++)
                {
                    for (int yy = miny - 1; yy < maxy + 2; yy++)
                    {
                        for (int xx = minx - 1; xx < maxx + 2; xx++)
                        {
                            bool active = calcPos4(dimension, xx, yy, zz, ww);
                            if (active)
                            {
                                int[] pos = new int[4];
                                pos[0] = xx; pos[1] = yy; pos[2] = zz; pos[3] = ww;
                                next.Add(pos, active);
                            }
                        }
                    }
                }
            }
            return next;
        }

        bool calcPos4(Dictionary<int[], bool> dimension, int x, int y, int z, int w)
        {
            int count = 0;
            bool alive = false;
            int[] pos = new int[4];
            pos[0] = x; pos[1] = y; pos[2] = z; pos[3] = w;
            if (dimension.ContainsKey(pos))
                alive = true;
            for (int xx = -1; xx < 2; xx++)
            {
                for (int yy = -1; yy < 2; yy++)
                {
                    for (int zz = -1; zz < 2; zz++)
                    {
                        for (int ww = -1; ww < 2; ww++)
                        {
                            if (xx == 0 && yy == 0 && zz == 0 && ww == 0)
                                continue;
                            pos[0] = x + xx; pos[1] = y + yy; pos[2] = z + zz; pos[3] = w + ww;
                            if (dimension.ContainsKey(pos))
                            {
                                count++;
                            }
                        }
                    }
                }
            }

            if (alive && (count == 2 || count == 3))
            {
                return true;
            }
            if (!alive && count == 3)
            {
                return true;
            }
            return false;
        }

        public class MyEqualityComparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y)
            {
                if (x.Length != y.Length)
                {
                    return false;
                }
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != y[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            public int GetHashCode(int[] obj)
            {
                int result = 17;
                for (int i = 0; i < obj.Length; i++)
                {
                    unchecked
                    {
                        result = result * 23 + obj[i];
                    }
                }
                return result;
            }
        }
    }
}
