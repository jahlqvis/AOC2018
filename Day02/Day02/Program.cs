using System;
using System.IO;
using System.Collections.Generic;

namespace Day02
{
    public class ChecksumFinder
    {
        public Dictionary<char, int> register;
        public List<string> rows;
        public int tuples = 0;
        public int triples = 0;
        public string a="";
        public string b="";



        public bool Import(string filename)
        {
            StreamReader reader = System.IO.File.OpenText(filename);
            rows = new List<string>();

            string row;
            while ((row = reader.ReadLine()) != null)
            {
                rows.Add(row);
            }
            return true;
        }

        public bool CountChars()
        {
            register = new Dictionary<char, int>();
            int pos;
            foreach(string row in rows)
            {
                pos = 0; 
                foreach(char c in row)
                {
                    if (register.ContainsKey(c))
                    {
                        int x;
                        register.TryGetValue(c, out x);
                        register.Remove(c);
                        x++;
                        register.Add(c, x);

                    }
                    else
                    {

                        register.Add(c, 1);
                    }
                    pos++;
                }

                if (register.ContainsValue(2))
                    tuples++;

                if (register.ContainsValue(3))
                    triples++;

                register.Clear();
            }

            return true;

        }

        public bool FindPairs()
        {
            register = new Dictionary<char, int>();

            foreach (string row in rows)
            {
                foreach(string row2 in rows)
                {
                    int d = 0;
                    int mark = 0;
                    for(int i=0;i<row.Length && i<row2.Length;i++)
                    {
                        if (row[i] != row2[i])
                        {
                            d++;
                            mark = i;
                        }

                    }
                   
                    if(d == 1)
                    {

                        a = row.Substring(0, mark);
                        b = row.Substring(mark + 1, row.Length - mark - 1);

                        return true;    
                    }
                }

            }



            return true;

        }
    }

    class MainClass
    {


        public static void Main(string[] args)
        {
            ChecksumFinder cf = new ChecksumFinder();
            cf.Import("input.txt");
            //cf.CountChars();
            cf.FindPairs();

            Console.WriteLine("Lines: {0}", cf.rows.Count);

            //Console.WriteLine("Hello World! Checksum is {0}", cf.tuples * cf.triples);
            //foreach (var item in cf.register)
            //{

            //    if (item.Value >= cf.rows.Count ) 
            //        Console.WriteLine("{0} {1}", item.Key, item.Value);
            //}

            Console.WriteLine("a: {0} b: {1}", cf.a, cf.b);

        }
    }
}
