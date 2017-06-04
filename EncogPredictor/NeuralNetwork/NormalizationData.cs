using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncogPredictor.NeuralNetwork
{
    [Serializable]
    public class NormalizationData
    {
        public NormalizationData()
        {

        }

        public double NormalMax { get; set; }
        public double NormalMin { get; set; }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        private double ValueRange => MaxValue - MinValue;
        private double NormalRange => NormalMax - NormalMin;

        public double Normalize(double value)
        {
            var rangedValue = (value - MinValue) / ValueRange;
            var normalValue = rangedValue * NormalRange + NormalMin;

            return normalValue;
        }

        public double Denormalize(double normalValue)
        {
            var rangedValue = (normalValue - NormalMin) / NormalRange;
            var value = rangedValue * ValueRange + MinValue;

            return value;
        }
    }
}
