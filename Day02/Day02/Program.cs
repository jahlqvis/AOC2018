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

            foreach(string row in rows)
            {
                foreach(char c in row)
                {
                    if (register.ContainsKey(c))
                    {
                        int val;
                        register.TryGetValue(c, out val);
                        register.Remove(c);
                        val++;
                        register.Add(c, val);

                    }
                    else
                    {
                        register.Add(c, 1);
                    }
                }

                if (register.ContainsValue(2))
                    tuples++;

                if (register.ContainsValue(3))
                    triples++;

                register.Clear();
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
            cf.CountChars();

            Console.WriteLine("Hello World! Checksum is {0}", cf.tuples * cf.triples);
        }
    }
}
