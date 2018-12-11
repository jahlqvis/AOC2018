using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdvCode2018_Day08;

namespace Test
{
    [TestClass]
    public class NodeTester
    {
        [TestMethod]
        public void NodeTest_goodExampleInput()
        {
            string str = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";

            checkSumCalculator csCalculator = new checkSumCalculator(str);

            int index = csCalculator.Node(0);
        
            int checksum = csCalculator.getChecksum();

            Assert.AreEqual(138, checksum);
        }

        [TestMethod]
        public void NodeTest_badExampleInput()
        {
            string str = "2 3 0 3 10 11 12 1 1 0 1 99 2 1"; // removed two last integers

            checkSumCalculator csCalculator = new checkSumCalculator(str);

            int index = csCalculator.Node(0);

            Assert.AreEqual(-1, index);

        }

        [TestMethod]
        public void Node2Test_goodExampleInput()
        {
            string str = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";

            checkSumCalculator csCalculator = new checkSumCalculator(str);

            int value = 0;
            int index = csCalculator.Node2(0, out value);

            Assert.AreEqual(66, value);
        }


    }
}
