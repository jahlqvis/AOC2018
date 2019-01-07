using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day03
{
    class Slice
    {
        public int id;
        public int tl_x;
        public int tl_y;
        public int width;
        public int height;

        public int br_x;
        public int br_y;

        public Slice(int id, int x, int y, int w, int h)
        {
            this.id = id;
            tl_x = x;
            tl_y = y;
            width = w;
            height = h;
            br_x = x + width - 1;
            br_y = y + height - 1;
        }

        public void Init(int id, int x, int y, int w, int h)
        {
            this.id = id;
            tl_x = x;
            tl_y = y;
            width = w;
            height = h;
            br_x = x + width - 1;
            br_y = y + height - 1;
        }
    }


    class SliceKeeper
    {
        public List<Slice> Slices;
        public int fabric_width;
        public int fabric_height;
        public int[,] fabric;

        public SliceKeeper()
        {
            Slices = new List<Slice>();

        }


        public long Overlap()
        {
            long n = 0;
            Fabricsize();

            fabric = new int[fabric_width+1, fabric_height+1];

            for (int y=1;y<=fabric_height;y++) 
                for (int x=1;x<=fabric_width;x++)
                {
                    fabric[x, y] = 0;
                    foreach (Slice s in Slices)
                    {
                        if (x >= s.tl_x && x <= s.br_x && y >= s.tl_y && y <= s.br_y)
                            fabric[x, y]++;

                    }
                    if (fabric[x, y] >= 2)
                        n++;
                }


            return n;
        }

        public int Nooverlap()
        {
            int n;
            int m=0;
            int o=0;

            foreach (Slice t in Slices)
            {
                n = 0;
                foreach (Slice s in Slices)
                {
                    if ((s.id != t.id) && // should not be same slice
                        ((t.tl_x >= s.tl_x && t.tl_x <= s.br_x && t.tl_y >= s.tl_y && t.tl_y <= s.br_y) ||  // check if topleft coordinates of slice t is inside any other slice 
                        (t.br_x >= s.tl_x && t.br_x <= s.br_x && t.br_y >= s.tl_y && t.br_y <= s.br_y) ||   // check if bottomright coordinatse of slice t is inside any other slice
                        (t.tl_x >= s.tl_x && t.tl_x <= s.br_x && t.br_y >= s.tl_y && t.br_y <= s.br_y) ||   // check if bottomleft coordinates of slice t is inside any other slice
                        (t.br_x >= s.tl_x && t.br_x <= s.br_x && t.tl_y >= s.tl_y && t.tl_y <= s.br_y) ||   // check if topright coordinates of slice t is inside any other slice
                        (s.tl_x >= t.tl_x && s.tl_x <= t.br_x && s.tl_y >= t.tl_y && s.tl_y <= t.br_y) ||   // check once if any other slices tl is totally inside t slice
                        (s.br_x >= t.tl_x && s.br_x <= t.br_x && s.br_y >= t.tl_y && s.br_y <= t.br_y) ||   // check once if any other slices br is totally inside t slice
                        (s.tl_x >= t.tl_x && s.tl_x <= t.br_x && s.br_y >= t.tl_y && s.br_y <= t.br_y) ||   // check once if any other slices bl is totally inside t slice
                        (s.br_x >= t.tl_x && s.br_x <= t.br_x && s.tl_y >= t.tl_y && s.tl_y <= t.br_y) ||   // check once if any other slices tr is totally inside t slice
                        (s.tl_x >= t.tl_x && s.tl_x <= t.br_x && t.tl_y >= s.tl_y && t.tl_y <= s.br_y) ||   // check if crossed s over t
                        (t.tl_x >= s.tl_x && t.tl_x <= s.br_x && s.tl_y >= t.tl_y && s.tl_y <= t.br_y)))    // check if crossed t over s
                        n++;

                }
                if (n == 0)
                {
                    m = t.id;
                    o++;
                }


            }

            if (o == 1)
                return m;
            else
                return -1;           
        }


        private bool Fabricsize()
        {
            int x=0;
            int y=0;
            foreach( Slice i in Slices)
            {
                if (x < i.br_x)
                    x = i.br_x;

                if (y < i.br_y)
                    y = i.br_y;

            }

            fabric_width = x;
            fabric_height = y;

            return true;
        }


        public bool Readfromfile(string filename)
        {

            string text = System.IO.File.ReadAllText(filename);

            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = System.IO.File.ReadAllLines(filename);

            foreach (string line in lines)
            {


                string[] digits = Regex.Split(line, @"\D+");

                if(digits.Length == 6)
                {
                    int id, x, y, w, h;

                    int.TryParse(digits[1], out id);
                    int.TryParse(digits[2], out x);
                    int.TryParse(digits[3], out y);
                    int.TryParse(digits[4], out w);
                    int.TryParse(digits[5], out h);

                    Slices.Add(new Slice(id, x+1, y+1, w, h)); // x and y coordinates are calculated by +1
                }

                 


            }
            if (Slices.Count > 0)
                return true;
            else
                return false;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            SliceKeeper s = new SliceKeeper();
            long n = 0;
            if (s.Readfromfile("input.txt"))
                //n = s.Overlap();
            //Console.WriteLine("The fabric's size is {0} x {1}, and {2} square inches are within two or more overlapping slices.", s.fabric_width, s.fabric_height, n);
            n = s.Nooverlap();
            Console.WriteLine("The slice with id {0}, has no overlap.", n);
        }
    }
}
