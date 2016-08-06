using System;
using System.Collections.Generic;
using System.Linq;

namespace WarehouseRandomizerProblem
{
    /// <summary>
    /// Randomizer for warehouse random non-repeating integers problem
    /// </summary>
    public class WarehouseRandomizer
    {
        /// <summary>
        /// Get random integer within specific range which is not already reserved.
        /// </summary>
        /// <param name="minValue">Minimal value</param>
        /// <param name="maxValue">Maximal value</param>
        /// <param name="reservedNumbers">Already reserved numbers</param>
        /// <exception cref="OutOfNumbersException">Wyrzucany gdy nie mamy już żadnych numerów do przypisania</exception>
        /// <returns>Random non reserved integer within specific range</returns>
        public int FetchRandomNumber(int minValue,int maxValue, List<int> reservedNumbers)
        {
            if(reservedNumbers.Count > 0)
            {
                int pivotNumber = GetMedian(minValue, maxValue);

                bool shouldGoLeft = new Random().Next(2) == 1;

                List<int> leftNodeNumbers = reservedNumbers
                    .Where((number) => number <= pivotNumber).ToList();

                List<int> rightNodeNumbers = reservedNumbers
                    .Where((number) => number > pivotNumber).ToList();

                if (leftNodeNumbers.Count == rightNodeNumbers.Count)
                {
                    if (IsNodeFull(leftNodeNumbers, minValue, pivotNumber) && IsNodeFull(rightNodeNumbers, pivotNumber, maxValue))
                    {
                        throw new OutOfNumbersException("There is no available numbers.");
                    }
                    else
                    {
                        if (IsNodeFull(leftNodeNumbers, minValue, pivotNumber) || IsNodeFull(rightNodeNumbers, pivotNumber, maxValue))
                        {
                            shouldGoLeft = !IsNodeFull(leftNodeNumbers, minValue, pivotNumber);
                        }
                    }
                }
                else
                {
                    shouldGoLeft = leftNodeNumbers.Count < rightNodeNumbers.Count;
                }

                if(shouldGoLeft)
                {
                    reservedNumbers = leftNodeNumbers;
                    maxValue = pivotNumber;
                }
                else
                {
                    reservedNumbers = rightNodeNumbers;
                    minValue = pivotNumber + 1;
                }

                return FetchRandomNumber(minValue, maxValue, reservedNumbers);
            }
            else
            {
                int random = new Random().Next(minValue, maxValue + 1);
                return random;
            }
        }

        /// <summary>
        /// Return median of integer list
        /// </summary>
        /// <param name="minValue">minimum integer</param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private int GetMedian(int minValue, int maxValue)
        {
            return (maxValue + (minValue - 1)) / 2;
        }

        /// <summary>
        /// Determine if there is no free numbers left in specific node
        /// </summary>
        /// <returns>True if there is no free numbers left in node</returns>
        private bool IsNodeFull(List<int> node,int minValue, int maxValue)
        {
            return node.Count == maxValue - (minValue-1);
        }
    }
}
