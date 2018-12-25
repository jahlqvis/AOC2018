using System;
using System.IO;
using System.Collections.Generic;

namespace Day01
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            FrequencyFinder ff = new FrequencyFinder();

            if (ff.ImportFromFile("input.txt"))
            {

                int res = ff.FindFirstTwiceFreq();

                Console.WriteLine("The first frequency found twice is {0}", res);

            }

        }

        class FrequencyFinder
        {
            private List<int> changes;
            private Dictionary<int, int> totals;

            public FrequencyFinder()
            {

                changes = new List<int>();

            }

            public int FindFirstTwiceFreq()
            {
                int total = 0;
                totals = new Dictionary<int, int>();
                bool search = true;
                while (search)
                {
                    foreach (var item in changes)
                    {
                        total += item;

                        if (totals.ContainsKey(total))
                        {
                            return total;
                        }
                        else
                            totals.Add(total, 1);
                    }
                }

                return 0;
            }


            public bool ImportFromFile(string filename)
            {
                StreamReader reader = System.IO.File.OpenText(filename);

                string line;

                int number;

                while ((line = reader.ReadLine()) != null)
                {

                    number = Convert.ToInt32(line);
                    changes.Add(number);

                }
                return true;
            }


        }
    }

}