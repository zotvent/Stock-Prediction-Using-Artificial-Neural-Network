using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Utils
{
    public static class Randomizer
    {
        private static readonly Random RandomObj = new Random();

        public static double GetRandomDouble(double min, double max)
        {
            var randomDouble = RandomObj.NextDouble();

            var randomValue = randomDouble * (max - min) + min;

            return randomValue;
        }
    }
}
