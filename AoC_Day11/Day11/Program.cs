using System;
using System.Diagnostics;

namespace Program
{
    public class Grid
    {
        private int serialnumber;
        const int gridsize = 300;
        public FuelCell[,] Cells;
        public FuelCellSquare Square;
        public long elapsed_ms;



        public int SearchHighestPowerSquare(out int x_found, out int y_found, ref int size)
        {
            int squaresizeMax = 300;
            int squaresizeMin = 1;

            x_found = -1;
            y_found = -1;

            int max = -1;

            if (size == 3) {
                squaresizeMax = 3;
                squaresizeMin = 3;
            }
            var sw = new Stopwatch();

            Square = new FuelCellSquare();

            try
            {
                for (int s=squaresizeMin;s<=squaresizeMax;s++)
                {
                    sw.Start();

                    int squaresx = gridsize - s + 1;
                    int squaresy = squaresx;

                   
                    for (int y = 0; y < squaresy; y++)
                    {
                        for (int x = 0; x < squaresx; x++)
                        {
                            Square.total = 0;

                            for (int x1 = 0; x1 < s; x1++)
                                for (int y1 = 0; y1 < s; y1++)
                                    Square.total += Cells[x + x1, y + y1].powerlevel;


                            if (Square.total > max)
                            {
                                max = Square.total;
                                x_found = x + 1;
                                y_found = y + 1;
                                size = s;

                            }
                        }
                    }

                    sw.Stop();
                    elapsed_ms = sw.ElapsedMilliseconds;
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

        }

        public class FuelCell
        {
            public int x;
            public int y;
            public int rackId;
            public int powerlevel;
            public int serial;
        }

        public class FuelCellSquare
        {
            public int total;

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
            int s = 3;
            Grid grid = new Grid(sn);
            power = grid.SearchHighestPowerSquare(out x, out y, ref s);

            Console.WriteLine("Hello, the serial number is {0} and grid size 300x300. Highest powerlevel {1} found at grid coordinates {2}, {3}.", sn, power, x, y);
            Console.WriteLine("");
            Console.ReadLine();

            return;
        }
    }



}
