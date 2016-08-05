using System;

namespace WarehouseRandomizerProblem
{
    internal class OutOfNumbersException : Exception
    {
        public OutOfNumbersException(string message) : base(message)
        {
        }
    }
}