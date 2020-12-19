using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day19 : Day
    {
        String[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = {
"0: 4 1 5",
"1: 2 3 | 3 2",
"2: 4 4 | 5 5",
"3: 4 5 | 5 4",
"4: \"a\"",
"5: \"b\"",
"",
"ababbb",
"bababa",
"abbbab",
"aaabbb",
"aaaabbb"};


            //string[] lines = { "0,3,6" };

                //*/

            return lines;
        }

        Dictionary<int, Rule> rules = new Dictionary<int, Rule>();
        

        public override void Run()
        {
            string[] data = getData();



            bool rule = true;
            int goodCount = 0;

            string fullRule = "";

            Regex tester = null;

            for(int i = 0; i < data.Length; i++)
            {
                string line = data[i];

                if(line.Trim().Length == 0)
                {
                    rule = false;

                    fullRule = computeRule(0);

                    fullRule = "^" + fullRule + "$";

                    tester = new Regex(fullRule);

                    continue;
                }

                if (rule)
                {
                    //read a rule
                    string[] parts = line.Split(':');
                    int ruleNum = int.Parse(parts[0]);

                    string rulestr = parts[1].Trim();
                    Rule r = new Rule();
                    if (rulestr.StartsWith('"'))
                    {
                        r.value = rulestr.Substring(1, rulestr.Length - 2);
                    }
                    else
                    {
                        string[] ruleparts = rulestr.Split('|');
                        r.options = new int[ruleparts.Length][];
                        for(int j=0;j< ruleparts.Length; j++)
                        {
                            string[] theseOptions = ruleparts[j].Trim().Split(' ');
                            r.options[j] = new int[theseOptions.Length];
                            for(int k = 0; k < theseOptions.Length; k++)
                            {
                                r.options[j][k] = int.Parse(theseOptions[k]);
                            }
                        }
                    }
                    rules.Add(ruleNum, r);

                }
                else
                {
                    //validate entries
                    if (tester.IsMatch(line))
                    {
                        goodCount++;
                    }
                }
            }

            Console.WriteLine("Valid Packets: " + goodCount);


            //part2
            goodCount = 0;
            rules.Clear();
            rule = true;

            for (int i = 0; i < data.Length; i++)
            {
                string line = data[i];

                if (line.Trim().Length == 0)
                {
                    rule = false;

                    fullRule = computeRule(0);

                    fullRule = "^" + fullRule + "$";

                    tester = new Regex(fullRule);

                    continue;
                }

                if (rule)
                {
                    if (line.StartsWith("8:"))
                    {
                        line = "8: 42 | 42 42 | 42 42 42 | 42 42 42 42 | 42 42 42 42 42 | 42 42 42 42 42 42 | 42 42 42 42 42 42 42 | 42 42 42 42 42 42 42 42 | 42 42 42 42 42 42 42 42 42 | 42 42 42 42 42 42 42 42 42 42 | 42 42 42 42 42 42 42 42 42 42 42";
                    }

                    if (line.StartsWith("11:"))
                    {
                        line = "11: 42 31 | 42 42 31 31 | 42 42 42 31 31 31 | 42 42 42 42 31 31 31 31 | 42 42 42 42 42 31 31 31 31 31 | 42 42 42 42 42 42 31 31 31 31 31 31 | 42 42 42 42 42 42 42 31 31 31 31 31 31 31 | 42 42 42 42 42 42 42 42 31 31 31 31 31 31 31 31 | 42 42 42 42 42 42 42 42 42 31 31 31 31 31 31 31 31 31 | 42 42 42 42 42 42 42 42 42 42 31 31 31 31 31 31 31 31 31 31 | 42 42 42 42 42 42 42 42 42 42 42 31 31 31 31 31 31 31 31 31 31 31";
                    }

                    //read a rule
                    string[] parts = line.Split(':');
                    int ruleNum = int.Parse(parts[0]);

                    string rulestr = parts[1].Trim();
                    Rule r = new Rule();
                    if (rulestr.StartsWith('"'))
                    {
                        r.value = rulestr.Substring(1, rulestr.Length - 2);
                    }
                    else
                    {
                        string[] ruleparts = rulestr.Split('|');
                        r.options = new int[ruleparts.Length][];
                        for (int j = 0; j < ruleparts.Length; j++)
                        {
                            string[] theseOptions = ruleparts[j].Trim().Split(' ');
                            r.options[j] = new int[theseOptions.Length];
                            for (int k = 0; k < theseOptions.Length; k++)
                            {
                                r.options[j][k] = int.Parse(theseOptions[k]);
                            }
                        }
                    }
                    rules.Add(ruleNum, r);

                }
                else
                {
                    //validate entries
                    if (tester.IsMatch(line))
                    {
                        goodCount++;
                    }
                }

                
            }

            Console.WriteLine("Valid Packets 2: " + goodCount);

        }

        string computeRule(int ruleNum)
        {
            Rule rule = rules[ruleNum];
            string optionstr = "(?:";
            bool start = true;

            if(rule.value != null)
            {
                return rule.value;
            }

            for (int i = 0; i < rule.options.Length; i++)
            {
                if (start)
                {
                    start = false;
                }
                else
                {
                    optionstr += "|";
                }

                for(int j = 0; j < rule.options[i].Length; j++)
                {
                    optionstr += computeRule(rule.options[i][j]);
                }

            }

            optionstr += ")";

            rule.value = optionstr;

            return optionstr;
        }

        string computeRule2(int ruleNum)
        {
            Rule rule = rules[ruleNum];
            string optionstr = "(?:";
            bool start = true;

            if (rule.value != null)
            {
                return rule.value;
            }

            for (int i = 0; i < rule.options.Length; i++)
            {
                if (start)
                {
                    start = false;
                }
                else
                {
                    optionstr += "|";
                }

                for (int j = 0; j < rule.options[i].Length; j++)
                {
                    optionstr += computeRule(rule.options[i][j]);
                }

            }

            optionstr += ")";

            if(ruleNum == 8)
            {
                optionstr += "+";
            }

            rule.value = optionstr;

            return optionstr;
        }



        class Rule
        {
            public string value = null;
            public int[][] options = null;
        }
    }

}
