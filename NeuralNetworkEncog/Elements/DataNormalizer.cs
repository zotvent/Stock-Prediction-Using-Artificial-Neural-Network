using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkEncog.Elements
{
    [Serializable]
    public class DataNormalizer
    {
        public DataNormalizer()
        {

        }

        public DataNormalizer(double[][] inputs, double[][] outputs, double normalMin = -1, double normalMax = 1)
        {
            InputNormalizationData = CreateNormalizationContext(inputs, normalMin, normalMax);
            OutputNormalizationData = CreateNormalizationContext(outputs, normalMin, normalMax);
        }

        private static NormalizationData[] CreateNormalizationContext(double[][] data, double normalMin, double normalMax)
        {
            var dataCount = data.Min(x => x.Count());

            var normalizationData = new NormalizationData[dataCount];
            for (int i = 0; i < dataCount; i++)
            {
                var maxValue = data.Max(x => x[i]);
                var minValue = data.Min(x => x[i]);

                var normalData = new NormalizationData
                {
                    MaxValue = maxValue,
                    MinValue = minValue,
                    NormalMax = normalMax,
                    NormalMin = normalMin
                };
                normalizationData[i] = normalData;
            }

            return normalizationData;
        }

        public NormalizationData[] InputNormalizationData { get; set; }
        public NormalizationData[] OutputNormalizationData { get; set; }

        

        public double[] NormalizeInput(double[] input)
        {
            var normalInput = new double[input.Count()];
            for(int i= 0; i < input.Count(); i++)
            {
                normalInput[i] = InputNormalizationData[i].Normalize(input[i]);
            }
            return normalInput;
        }

        public double[] DenormalizeOutput(double[] output)
        {
            var normalOutput = new double[output.Count()];
            for (int i = 0; i < output.Count(); i++)
            {
                normalOutput[i] = OutputNormalizationData[i].Denormalize(output[i]);
            }
            return normalOutput;
        }
    }
}
