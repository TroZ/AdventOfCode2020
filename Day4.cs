using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day4 : Day
    {
        String[] getData()
        {

            //*
            string[] lines = Program.readFile(int.Parse(this.GetType().Name.Substring(3)));
            /*/
            string[] lines = { "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd",
"byr:1937 iyr: 2017 cid: 147 hgt: 183cm",
"",
"iyr:2013 ecl: amb cid:350 eyr: 2023 pid: 028048884",
"hcl:#cfa07d byr:1929",
"",
"hcl:#ae17e1 iyr:2013",
"eyr:2024",
"ecl:brn pid:760753108 byr: 1931",
"hgt:179cm",
"",
"hcl:#cfa07d eyr:2025 pid:166559648",
"iyr:2011 ecl: brn hgt:59in" };
            //*/

            /*
            String[] lines = { 
"eyr:1972 cid:100",
"hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926",
"",
"iyr:2019",
"hcl:#602927 eyr:1967 hgt:170cm",
"ecl:grn pid:012533040 byr:1946",
"",
"hcl:dab227 iyr:2012",
"ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
"",
"hgt:59cm ecl:zzz",
"eyr:2038 hcl:74454a iyr:2023",
"pid:3556412378 byr:2007"};
            */
/*
            string[] lines =
                        {
"pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980",
"hcl:#623a2f",
"",
"eyr:2029 ecl:blu cid:129 byr:1989",
"iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
"",
"hcl:#888785",
"hgt:164cm byr:2001 iyr:2015 cid:88",
"pid:545766238 ecl:hzl",
"eyr:2022",
"",
"iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"
            };
*/
            return lines;
        }

        bool byr, iyr, eyr, hgt, hcl, ecl, pid, cid;
        int valid = 0;
        int invalid = 0;
        readonly static Regex hclregex = new Regex(@"^#[0-9a-f]{6}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        readonly static Regex pidregex = new Regex(@"^\d{9}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public override void Run()
        {
            string[] data = getData();

            int len = data[0].Length;

            byr = iyr = eyr = hgt = hcl = ecl = pid = cid = false;

            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(data[i]);

                if(data[i].Length < 2)
                {
                    eval();
                }
                else
                {
                    string l = data[i];
                    string[] d = l.Split(' ');
                    foreach(string part in d){
                        string[] fact = part.Split(':');

                        add2(fact);
                    }
                }
            }
            eval();

            Console.WriteLine("Valid = " + valid);
        }

        private void add1(string[] fact)
        {
            switch (fact[0])
            {
                case "byr": byr = true; break;
                case "iyr": iyr = true; break;
                case "eyr": eyr = true; break;
                case "hgt": hgt = true; break;
                case "hcl": hcl = true; break;
                case "ecl": ecl = true; break;
                case "pid": pid = true; break;
                case "cid": cid = true; break;
            }
        }

        private void add2(string[] fact)
        {
            switch (fact[0])
            {
                case "byr": {
                        int year = int.Parse(fact[1]);
                        if (year >= 1920 && year <= 2002)
                        {
                            byr = true;
                        }
                        break;
                    }
                case "iyr": { 
                        int year = int.Parse(fact[1]);
                        if (year >= 2010 && year <= 2020)
                        {
                            iyr = true;
                        }
                        break;
                    }
                case "eyr": {
                        int year = int.Parse(fact[1]);
                        if (year >= 2020 && year <= 2030)
                        {
                            eyr = true;
                        }
                        break; 
                    }
                case "hgt": {
                        if (fact[1].Length < 3)
                            break;
                        string numstr = fact[1].Substring(0, fact[1].Length - 2);
                        int num = int.Parse(numstr);
                        bool inch = fact[1].EndsWith("in");
                        bool cm = fact[1].EndsWith("cm");
                        if (inch)
                        {
                            if(num >= 59 && num <= 76 )
                            {
                                hgt = true;
                            }
                        }
                        if (cm)
                        {
                            if (num >= 150 && num <= 193)
                            {
                                hgt = true;
                            }
                        }
                        break; 
                    }
                case "hcl": {
                        MatchCollection mc = hclregex.Matches(fact[1]);
                        if (mc.Count == 1)
                        {
                            hcl = true;
                        }
                        break; 
                    }
                case "ecl": {
                        if (fact[1] == "amb" || fact[1] == "blu" || fact[1] == "brn" || fact[1] == "gry" || fact[1] == "grn" || fact[1] == "hzl" || fact[1] == "oth")
                        {
                            ecl = true;
                        }
                        break; 

                    }
                case "pid": {
                        MatchCollection mc = pidregex.Matches(fact[1]);
                        if (mc.Count  == 1)
                        {
                            pid = true;
                        }
                        break;
                    }
                case "cid": { cid = true; break; }
            }
        }

        public void eval()
        {
            if(byr && iyr && eyr && hgt && hcl && ecl && pid)
            {
                valid++;
                Console.WriteLine("VALID!");
            }else
            {
                invalid++;
                Console.Write("invalid    ");
                if (!byr)
                {
                    Console.Write("byr ");
                }
                if (!iyr)
                {
                    Console.Write("iyr ");
                }
                if (!eyr)
                {
                    Console.Write("eyr ");
                }
                if (!hgt)
                {
                    Console.Write("hgt ");
                }
                if (!hcl)
                {
                    Console.Write("hcl ");
                }
                if (!ecl)
                {
                    Console.Write("ecl ");
                }
                if (!pid)
                {
                    Console.Write("pid");
                }
                Console.WriteLine();
            }
            if (cid == true)
            {
                pid = cid; //to stop not reading cid warning
            }
            byr = iyr = eyr = hgt = hcl = ecl = pid = cid = false;
            Console.WriteLine();
        }
    }
}
