using System;
using System.Collections.Generic;

namespace Program
{
    public class Grid
    {
        private int serialnumber;
        const int gridsize = 300;
        public FuelCell[,] Cells;
        public int[,] Squares;
        public List<Tuple<int, long>> ElapsedTime;
        public Dictionary<int, int[,]> SquaresList;


        public int SearchHighestPower(out int x_found, out int y_found, out int size)
        {

            x_found = -1;
            y_found = -1;
            size = -1;
            int max = -1;

            try
            {

                for (int s = 1; s <= 300; s++)
                {
                    double r = 300 - s + 1;

                    double squaresy = Math.Ceiling(r);
                    double squaresx = squaresy;

                    Squares = new int[(int)squaresx, (int)squaresy];

                    for (int y = 0; y < squaresy; y++)
                    {
                        for (int x = 0; x < squaresx; x++)
                        {
                            int total = 0;

                            if(s==1)
                            {
                                total = Cells[x, y].powerlevel;
                                Squares[x, y] = total;


                            }
                            else if (s%2==0) // even
                            {
                                int s1 = s / 2;

                                int[,] temp;
                                SquaresList.TryGetValue(s1, out temp);

                                total += temp[x, y];
                                total += temp[x + s1, y];
                                total += temp[x + s1, y + s1];
                                total += temp[x, y + s1];

                                Squares[x, y] = total;
                            } 
                            else if(s>1 && s%2!=0) // odd
                            {
                                int s1 = (s - 1)/2;
                                int s2 = (s + 1)/2;
                                int s0 = 1;

                                int[,] temp1;
                                int[,] temp2;
                                int[,] temp0;
                                 
                                SquaresList.TryGetValue(s1, out temp1);
                                SquaresList.TryGetValue(s2, out temp2);
                                SquaresList.TryGetValue(s0, out temp0);

                                total += temp2[x, y];
                                total += temp1[x + s2, y];
                                total += temp1[x, y + s2];
                                total += temp2[x + s1, y + s1];

                                int m = (s - 1) / 2;

                                total -= temp0[x + m, y + m];

                                Squares[x, y] = total;
                            }


                            if (total > max)
                            {
                                max = total;
                                x_found = x + 1;
                                y_found = y + 1;
                                size = s;
                            }
                        }
                    }

                    SquaresList.Add(s, Squares);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("index out of bounds!");
                throw e;
            }

            return max;
        }

        public Grid(int serial)
        {
            Cells = new FuelCell[gridsize, gridsize];


            serialnumber = serial;
            for (int y = 1; y <= 300; y++)
                for (int x = 1; x <= 300; x++)
                {
                    Cells[x - 1, y - 1] = new FuelCell();

                    Cells[x - 1, y - 1].x = x;
                    Cells[x - 1, y - 1].y = y;
                    Cells[x - 1, y - 1].serial = serial;

                    //The power level in a given fuel cell can be found through the following process:
                    //Find the fuel cell's rack ID, which is its X coordinate plus 10.
                    Cells[x - 1, y - 1].rackId = Cells[x - 1, y - 1].x + 10;

                    //Begin with a power level of the rack ID times the Y coordinate.
                    Cells[x - 1, y - 1].powerlevel = Cells[x - 1, y - 1].rackId * Cells[x - 1, y - 1].y;

                    //Increase the power level by the value of the grid serial number(your puzzle input)
                    Cells[x - 1, y - 1].powerlevel = Cells[x - 1, y - 1].powerlevel + Cells[x - 1, y - 1].serial;

                    //Set the power level to itself multiplied by the rack ID.
                    Cells[x - 1, y - 1].powerlevel = Cells[x - 1, y - 1].powerlevel * Cells[x - 1, y - 1].rackId;

                    //Keep only the hundreds digit of the power level(so 12345 becomes 3; numbers with no hundreds digit become 0).
                    int hundreds = (int)Math.Abs(Cells[x - 1, y - 1].powerlevel / 100 % 10);

                    //Subtract 5 from the power level.
                    Cells[x - 1, y - 1].powerlevel = hundreds - 5;
                }

            SquaresList = new Dictionary<int, int[,]>();

    }

        public class FuelCell
        {
            public int x;
            public int y;
            public int rackId;
            public int powerlevel;
            public int serial;
        }


    }

    public class Program
    {
        public static void Main(string[] args)
        {
            int power;
            int sn = 6548;
            int x;
            int y;
            int s;
            Grid grid = new Grid(sn);
            power = grid.SearchHighestPower(out x, out y, out s);

            Console.WriteLine("Hello, the serial number is {0} and grid size 300x300. Highest powerlevel {1} found at grid coordinates {2}, {3} and square size {4}.", sn, power, x, y, s);
            Console.WriteLine("");
            Console.ReadLine();

            return;
        }
    }



}
