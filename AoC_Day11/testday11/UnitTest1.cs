using Microsoft.VisualStudio.TestTools.UnitTesting;
using Program;

namespace testday11
{
    [TestClass]
    public class UnitTest1
    {
       
        [DataTestMethod]
        [DataRow(122, 79, 57, -5)]
        [DataRow(217, 196, 39, 0)]
        [DataRow(101, 153, 71, 4)]
        public void TestFuelCell(int x, int y, int sn, int pl)
        {
            Grid grid = new Grid(sn);

            Assert.AreEqual(pl, grid.Cells[x-1, y-1].powerlevel);

            //Here are some more example power levels:

            //Fuel cell at  122,79, grid serial number 57: power level -5.
            //Fuel cell at 217,196, grid serial number 39: power level  0.
            //Fuel cell at 101,153, grid serial number 71: power level  4.


        }

        //[DataTestMethod]
        //[DataRow(18, 33, 45, 29)]
        //[DataRow(42, 21, 61, 30)]
        //public void TestFuelGroup(int sn, int x, int y, int pl)
        //{
        //    int foundx = 0;
        //    int foundy = 0;
        //    Grid grid = new Grid(sn);

        //    int power = grid.SearchHighestPower(out foundx, out foundy);

        //    Assert.AreEqual(grid.Square.total, pl);
        //    Assert.AreEqual(foundx, x);
        //    Assert.AreEqual(foundy, y);
        //    Assert.AreEqual(power, pl);

        //    // For grid serial number 18, the largest total 3x3 square has a top - 
        //    // left corner of 33,45(with a total power of 29); these fuel cells appear in the middle of this 5x5 region:

        //    //-2 - 4   4   4   4
        //    //- 4   4   4   4 - 5
        //    // 4   3   3   4 - 4
        //    // 1   1   2   4 - 3
        //    //- 1   0   2 - 5 - 2

        //    // For grid serial number 42, the largest 3x3 square's top-
        //    //left is 21,61 (with a total power of 30); they are in the middle of this region:

        //    //- 3   4   2   2   2
        //    //- 4   4   3   3   4
        //    // - 5   3   3   4 - 4
        //    //4   3   3   4 - 3
        //    //3   3   3 - 5 - 1


        //}

        [DataTestMethod]
        [DataRow(18, 90, 269, 16, 113)]
        [DataRow(42, 232, 251, 12, 119)]
        public void Quest(int sn, int x, int y, int size, int power)
        {
            int foundx = 0;
            int foundy = 0;
            int foundsize; 
            Grid grid = new Grid(sn);
            int foundpower = grid.SearchHighestPower(out foundx, out foundy, out foundsize);

            Assert.AreEqual(x, foundx);
            Assert.AreEqual(y, foundy);
            Assert.AreEqual(size, foundsize);
            Assert.AreEqual(power, foundpower);
        }

        [DataTestMethod]
        [DataRow(6548)]
        public void Quest2(int sn)
        {
            int foundx = 0;
            int foundy = 0;
            int foundsize;
            Grid grid = new Grid(sn);
            int foundpower = grid.SearchHighestPower(out foundx, out foundy, out foundsize);

        }

    }
}
