using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WarehouseRandomizerProblem
{
    class Program
    {

        static void Main(string[] args)
        {
            List<int> reservedNumbers = new List<int>();
            WarehouseRandomizer randomizer = new WarehouseRandomizer();

            try
            {
                long tick = DateTime.Now.Ticks;
                for (int i = 1000000; i < 10000000; i += 1)
                {
                    if(i!=1234567) reservedNumbers.Add(i);
                }

                int randomInt = randomizer.FetchRandomNumber(1000000, 10000000, reservedNumbers);
                Console.WriteLine(randomInt);
                reservedNumbers.Add(randomInt);
                Console.WriteLine(reservedNumbers.GroupBy(x => x)
                             .Where(g => g.Count() > 1).Count() > 0);

                Console.WriteLine("Elapsed time: " + TimeSpan.FromTicks(DateTime.Now.Ticks - tick).TotalSeconds);
            }
            catch (OutOfNumbersException ex)
            {
                Console.WriteLine(ex.Message);
            }          
            Console.ReadKey();
        }
    }
}
