using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day7 : Day
    {
        String[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = {
"light red bags contain 1 bright white bag, 2 muted yellow bags.",
"dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
"bright white bags contain 1 shiny gold bag.",
"muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
"shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
"dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
"vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
"faded blue bags contain no other bags.",
"dotted black bags contain no other bags." 
            
//"shiny gold bags contain 2 dark red bags.",
//"dark red bags contain 2 dark orange bags.",
//"dark orange bags contain 2 dark yellow bags.",
//"dark yellow bags contain 2 dark green bags.",
//"dark green bags contain 2 dark blue bags.",
//"dark blue bags contain 2 dark violet bags.",
//"dark violet bags contain no other bags."
            };
            //*/

            return lines;
        }

        readonly static Regex oneitem = new Regex(@"^(.*) bags contain (\d*) (.*) bags?\.$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        readonly static Regex twoitem = new Regex(@"^(.*) bags contain (\d*) (.*) bags?, (\d*) (.*) bags?\.$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        readonly static Regex threeitem = new Regex(@"^(.*) bags contain (\d*) (.*) bags?, (\d*) (.*) bags?, (\d*) (.*) bags?\.$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        readonly static Regex fouritem = new Regex(@"^(.*) bags contain (\d*) (.*) bags?, (\d*) (.*) bags?, (\d*) (.*) bags?, (\d*) (.*) bags?\.$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        readonly static Regex fiveitem = new Regex(@"^(.*) bags contain (\d*) (.*) bags?, (\d*) (.*) bags?, (\d*) (.*) bags?, (\d*) (.*) bags?, (\d*) (.*) bags?\.$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public override void Run()
        {
            string[] data = getData();


            NameValueCollection containdBy = new NameValueCollection();

            Dictionary<string, List<BagCount>> contains = new Dictionary<string, List<BagCount>>();


            foreach (string line in data)
            {
                MatchCollection mc = fiveitem.Matches(line);
                if(mc.Count > 0)
                {
                    foreach (Match match in mc)
                    {
                        string outer = match.Groups[1].Value;
                        string one = match.Groups[3].Value;
                        string two = match.Groups[5].Value;
                        string three = match.Groups[7].Value;
                        string four = match.Groups[9].Value;
                        string five = match.Groups[11].Value;

                        containdBy.Add(one, outer);
                        containdBy.Add(two, outer);
                        containdBy.Add(three, outer);
                        containdBy.Add(four, outer);
                        containdBy.Add(five, outer);

                        List<BagCount> holds = new List<BagCount>();
                        holds.Add(new BagCount(match.Groups[2].Value, one));
                        holds.Add(new BagCount(match.Groups[4].Value, two));
                        holds.Add(new BagCount(match.Groups[6].Value, three));
                        holds.Add(new BagCount(match.Groups[8].Value, four));
                        holds.Add(new BagCount(match.Groups[10].Value, five));
                        contains.Add(outer, holds);
                    }
                    continue;
                }
                mc = fouritem.Matches(line);
                if (mc.Count > 0)
                {
                    foreach (Match match in mc)
                    {
                        string outer = match.Groups[1].Value;
                        string one = match.Groups[3].Value;
                        string two = match.Groups[5].Value;
                        string three = match.Groups[7].Value;
                        string four = match.Groups[9].Value;

                        containdBy.Add(one, outer);
                        containdBy.Add(two, outer);
                        containdBy.Add(three, outer);
                        containdBy.Add(four, outer);

                        List<BagCount> holds = new List<BagCount>();
                        holds.Add(new BagCount(match.Groups[2].Value, one));
                        holds.Add(new BagCount(match.Groups[4].Value, two));
                        holds.Add(new BagCount(match.Groups[6].Value, three));
                        holds.Add(new BagCount(match.Groups[8].Value, four));
                        contains.Add(outer, holds);
                    }
                    continue;
                }
                mc = threeitem.Matches(line);
                if (mc.Count > 0)
                {
                    foreach (Match match in mc)
                    {
                        string outer = match.Groups[1].Value;
                        string one = match.Groups[3].Value;
                        string two = match.Groups[5].Value;
                        string three = match.Groups[7].Value;

                        containdBy.Add(one, outer);
                        containdBy.Add(two, outer);
                        containdBy.Add(three, outer);

                        List<BagCount> holds = new List<BagCount>();
                        holds.Add(new BagCount(match.Groups[2].Value, one));
                        holds.Add(new BagCount(match.Groups[4].Value, two));
                        holds.Add(new BagCount(match.Groups[6].Value, three));
                        contains.Add(outer, holds);
                    }
                    continue;
                }
                mc = twoitem.Matches(line);
                if (mc.Count > 0)
                {
                    foreach (Match match in mc)
                    {
                        string outer = match.Groups[1].Value;
                        string one = match.Groups[3].Value;
                        string two = match.Groups[5].Value;

                        containdBy.Add(one, outer);
                        containdBy.Add(two, outer);

                        List<BagCount> holds = new List<BagCount>();
                        holds.Add(new BagCount(match.Groups[2].Value, one));
                        holds.Add(new BagCount(match.Groups[4].Value, two));
                        contains.Add(outer, holds);
                    }
                    continue;
                }
                mc = oneitem.Matches(line);
                if (mc.Count > 0)
                {
                    foreach (Match match in mc)
                    {
                        string outer = match.Groups[1].Value;
                        string one = match.Groups[3].Value;

                        containdBy.Add(one, outer);

                        List<BagCount> holds = new List<BagCount>();
                        holds.Add(new BagCount(match.Groups[2].Value, one));
                        contains.Add(outer, holds);
                    }
                }
                
            }

            //find possible
            List<string> possible = new List<string>();
            int size = -1;
            possible.Add("shiny gold");
            while(possible.Count > size)
            {
                size = possible.Count;

                List<string> toadd = new List<string>();
                foreach (string bag in possible)
                {
                    if (containdBy[bag] != null)
                    {
                        string[] bags = containdBy[bag].Split(',');
                        foreach (string b in bags)
                        {
                            if (!possible.Contains(b) && !toadd.Contains(b))
                            {
                                toadd.Add(b);
                            }
                        }
                    }
                }

                foreach(String str in toadd)
                {
                    possible.Add(str);
                }


            }


            Console.WriteLine("Possible containers: " + (size-1));



            List<BagCount> totalBags = new List<BagCount>();
            AddBags(contains, totalBags, "shiny gold", 1);


            int count = 0;
            foreach(BagCount bc in totalBags)
            {
                count += bc.count;
            }

            Console.WriteLine("total needed bags: " + count);
        }


        public void AddBags(Dictionary<string, List<BagCount>> contains, List<BagCount> totalBags, String bag, int count)
        {
            List<BagCount> thisbag;
            if (contains.TryGetValue(bag, out thisbag))
            {
                foreach (BagCount bags in thisbag)
                {
                    //look for type bags.bag in totalBags
                    bool found = false;
                    foreach(BagCount tb in totalBags)
                    {
                        if(tb.bag == bags.bag)
                        {
                            tb.count += bags.count * count;
                            found = true;
                        }
                    }

                    //not found, add new typw
                    if (!found)
                    {
                        totalBags.Add(new BagCount("" + (bags.count * count), bags.bag));
                    }

                    //now add contained bags of this type
                    AddBags(contains, totalBags, bags.bag, bags.count * count);
                }
            }
        }

        public class BagCount
        {
            public string bag;
            public int count;

            public BagCount(string cnt, string bag)
            {
                this.bag = bag;
                this.count = int.Parse(cnt);
            }
        }
    }

    
}