using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;



namespace AoC18_Day10
{
    public class StarSystem
    {

        private List<Star> galaxy = new List<Star>();
        private bool messageFound;
        private int message_rows;
        private int message_cols;
        private long message_max_x;
        private long message_min_x;
        private long message_max_y;
        private long message_min_y;



        public class Star
        {
            long pos_x;
            long pos_y;
            long vel_x;
            long vel_y;

            public Star(long pos_x, long pos_y, long vel_x, long vel_y)
            {
                this.pos_x = pos_x;
                this.Pos_y = pos_y;
                this.Vel_x = vel_x;
                this.Vel_y = vel_y;
            }

            public long Pos_x { get => pos_x; set => pos_x = value; }
            public long Pos_y { get => pos_y; set => pos_y = value; }
            public long Vel_x { get => vel_x; set => vel_x = value; }
            public long Vel_y { get => vel_y; set => vel_y = value; }
        }


        public bool ImportStars(string filename)
        {
            StreamReader reader = System.IO.File.OpenText(filename);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Split on non-digit characters.
                string[] numbers = Regex.Split(line, @"[^\d-]\ *");

                long[] i = new long[4];
                int j = 0;

                foreach (string match in numbers)
                {
                    if (!string.IsNullOrEmpty(match) && j < 4)
                    {
                        i[j] = long.Parse(match);
                        j++;

                    }
                }


                Console.WriteLine("x: {0} y: {1} vel_x: {2} vel_y: {3}", i[0], i[1], i[2], i[3]);
                Star s = new Star(i[0], i[1], i[2], i[3]);
                galaxy.Add(s);
            }
            return true;
        }

        public bool FindMessage(long searchTime)
        {
            long countStars = galaxy.Count;

            long maxSeconds = searchTime;

            long prev_delta_x = 150000;
            long prev_delta_y = 150000;

            messageFound = false;

            for (long second = 0;second < maxSeconds;second++) // time loop
            {
                long max_x = -100000;
                long min_x = 100000;

                long max_y = -100000;
                long min_y = 100000;

                long delta_x = 0;
                long delta_y = 0;

                for (int star = 0; star < countStars;star++)
                {
                    galaxy[star].Pos_x = galaxy[star].Pos_x + galaxy[star].Vel_x;
                    galaxy[star].Pos_y = galaxy[star].Pos_y + galaxy[star].Vel_y;

                    if (max_x < galaxy[star].Pos_x)
                        max_x = galaxy[star].Pos_x;

                    if (min_x > galaxy[star].Pos_x)
                        min_x = galaxy[star].Pos_x;

                    if (max_y < galaxy[star].Pos_y)
                        max_y = galaxy[star].Pos_y;

                    if (min_y > galaxy[star].Pos_y)
                        min_y = galaxy[star].Pos_y;


                }

                delta_x = max_x - min_x;
                delta_y = max_y - min_y;

                long delta_area = delta_x * delta_y;
                long prev_delta_area = prev_delta_x * prev_delta_y;
                // check when it starts to grow again
                if (delta_area > prev_delta_area)
                {
                    max_x = -100000;
                    min_x = 100000;

                    max_y = -100000;
                    min_y = 100000;

                    // back one second
                    for (int star = 0; star < countStars; star++)
                    {


                        galaxy[star].Pos_x = galaxy[star].Pos_x - galaxy[star].Vel_x;
                        galaxy[star].Pos_y = galaxy[star].Pos_y - galaxy[star].Vel_y;

                        if (max_x < galaxy[star].Pos_x)
                            max_x = galaxy[star].Pos_x;

                        if (min_x > galaxy[star].Pos_x)
                            min_x = galaxy[star].Pos_x;

                        if (max_y < galaxy[star].Pos_y)
                            max_y = galaxy[star].Pos_y;

                        if (min_y > galaxy[star].Pos_y)
                            min_y = galaxy[star].Pos_y;

                    }

                    message_max_x = max_x;
                    message_min_x = min_x;
                    message_max_y = max_y;
                    message_min_y = min_y;

                    delta_x = max_x - min_x;
                    delta_y = max_y - min_y;

                    Console.WriteLine("");
                    second = second - 1;
                    if (second < 60 * 60)
                        Console.WriteLine("Found message after {0} seconds (more than {1} minutes): delta x: {2} delta y: {3}", second, TimeSpan.FromSeconds(--second).Minutes, delta_x, delta_y);
                    else if (second < 60 * 60 * 24)
                        Console.WriteLine("Found message after {0} seconds (more than {1} hours): delta x: {2} delta y: {3}", second, TimeSpan.FromSeconds(--second).Hours, delta_x, delta_y);

                    message_cols = (int)delta_x;
                    message_rows = (int)delta_y;


                    messageFound = true;
                    break;
                }




                Console.WriteLine("Second {0}: delta x: {1} delta y: {2}", second, max_x - min_x, max_y - min_y);

                prev_delta_x = delta_x;
                prev_delta_y = delta_y;

            }

            return messageFound;


        }

        private bool ChangeCoordinatesToLocal()
        {
            if (!messageFound)
                return false;

            int countStars = galaxy.Count;
            for (int star = 0; star < countStars; star++)
            {
                galaxy[star].Pos_x = galaxy[star].Pos_x - message_min_x;
                galaxy[star].Pos_y = galaxy[star].Pos_y - message_min_y;
            }

            return true;
        }

        public bool WriteMessageToFile()
        {

            if (!messageFound)
                return false; 
            
            if (!ChangeCoordinatesToLocal())
                return false;




            string[] allrows = new string[message_rows+1];
            int starCount = galaxy.Count;

            // draw

            for (int row = 0; row <= message_rows; row++)
            {
                char[] thisrow = new char[message_cols+1];

                for (int col = 0; col <= message_cols; col++)
                {
                    thisrow[col] = '.';
                    for (int star = 0; star < starCount; star++)
                    {
                        if((galaxy[star].Pos_y == row) && (galaxy[star].Pos_x == col))
                            thisrow[col] = '#';
                    }
                }

                string s = new string(thisrow);
                allrows[row] = s;

            }


            System.IO.File.WriteAllLines(@"message.txt", allrows);

            return true;
        }


    }


    class MainClass
    {
        public static void Main(string[] args)
        {
            StarSystem ss = new StarSystem();

            ss.ImportStars("input.txt");
            if (ss.FindMessage(100000))
                ss.WriteMessageToFile();

        }
    }
}
