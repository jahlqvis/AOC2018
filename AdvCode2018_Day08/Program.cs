using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvCode2018_Day08
{
    public class checkSumCalculator
    {
        private int[] int_arr;
        private List<int> metadataList;

        public checkSumCalculator(string input)
        {
            string[] str_array = input.Split(' ').ToArray();

            int_arr = Array.ConvertAll(str_array, Int32.Parse);

            metadataList = new List<int>();
        }

        public int getChecksum()
        {
            int checksum = 0;
            if (metadataList.Count > 0)
                foreach (var metadata in metadataList)
                    checksum = checksum + metadata;

            return checksum;
        }

        public int Node(int index)
        {
            try
            {
                int numOfChildren = int_arr[index];
                int numOfMetadata = int_arr[++index];

                if (numOfChildren > 0)
                {
                    int childCounter = 1;
                    while (childCounter <= numOfChildren)
                    {
                        index = Node(++index);
                        childCounter++;
                    }
                }

                if (numOfMetadata > 0)
                {
                    for (int i = 0; i < numOfMetadata; i++)
                    {
                        metadataList.Add(int_arr[++index]);
                    }

                }
            }
            catch (IndexOutOfRangeException e)
            {
                
                Console.WriteLine("The license file data is corrupt!");
                return -1;
            }
            catch (Exception e)
            {
                // something else gone wrong
                throw e;
            }

            return index;
        }

        public int Node2(int index, out int value)
        {
            try
            {
                int numOfChildren = int_arr[index];
                int numOfMetadata = int_arr[++index];
                Dictionary<int, int> childValues = new Dictionary<int, int>();
                int sumOfChildValues=0;
                

                if (numOfChildren > 0)
                {
                    int childCounter = 1;
                    int childValue = 0;
                    while (childCounter <= numOfChildren)
                    {
                        index = Node2(++index, out childValue);
                        childValues.Add(childCounter, childValue);
                        childCounter++;
                    }
                }

                if (numOfMetadata > 0)
                {
                    if (numOfChildren == 0)
                    {
                        for (int i = 0; i < numOfMetadata; i++)
                        {
                            sumOfChildValues = sumOfChildValues + int_arr[++index];
                        }

                        value = sumOfChildValues;
                        return index;
                    }
                    else
                    {
                        int temp;
                        for (int i = 1; i <= numOfMetadata; i++)
                        {
                            temp = 0;
                            if (childValues.TryGetValue(int_arr[++index], out temp))
                                sumOfChildValues = sumOfChildValues + temp;
                        }

                        value = sumOfChildValues;
                        return index;
                    }
                }
                else
                {
                    value = 0;
                    return index;
                }
            }
            catch (IndexOutOfRangeException e)
            {

                Console.WriteLine("The license file data is corrupt!");
                value = -1;
                return -1;
            }
            catch (Exception e)
            {
                // something else gone wrong
                throw e;
            }

            
        }
    }

    class Program
    {
      
        static void Main(string[] args)
        {
            string str = System.IO.File.ReadAllText(@"Resources\input.txt");

            checkSumCalculator sc = new checkSumCalculator(str);

            int lastIndex = sc.Node(0);
            int checksum = sc.getChecksum();

            Console.WriteLine("The checksum of the license file is: {0}", checksum);
            Console.Read();

            int value = 0;
            int lastIndex2 = sc.Node2(0, out value);

            Console.WriteLine("The root value is: {0}: ", value);
            Console.Read();
        }

        

    }

}
