using Encog.ML;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.ML.Data.Versatile;
using Encog.ML.Model;
using Encog.Util.Arrayutil;
using NeuralNetworkEncog.Extensions;
using SqlObjectWrapper.EntityModel;
using SqlObjectWrapper.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkEncog.Elements
{
    [Serializable]
    public class NeuralNetworkInstance
    {
        public NeuralNetworkInstance()
        { }

        public NetworkParameters Parameters { get; set; }
        public IMLRegression Method { get; set; }
        public DataNormalizer Normalizer { get; set; }

        public double[][] ProcessInput(IEnumerable<StockHistory> input)
        {
            var inputIndexses = Parameters.InputColumns;
            double[][] rawInput = new double[Parameters.LagWindowSize+1][];
            for(int i=0; i< Parameters.LagWindowSize + 1; i++)
            {
                rawInput[i] = new double[inputIndexses.Count()];
                for (int j = 0; j < inputIndexses.Count(); j++)
                {
                    rawInput[i][j] = input.ElementAt(i).GetValuesArray()[inputIndexses[j]];
                }
            }
            

            return rawInput;
        }

        

        private double[][] BuildOutputMatrix(IMLData outputVector)
        {
            var outputSlices = Parameters.LeadWindowSize;

            var matrix = new double[outputSlices][];
            for(int i=0; i< outputVector.Count; i++)
            {
                var slice = i / Parameters.OutputColumns.Count();
                var index = i % Parameters.OutputColumns.Count();
                matrix[slice] = new double[Parameters.OutputColumns.Count()];
                matrix[slice][index] = outputVector[i];
            }

            return matrix;
        }


        private double[][] DenormalizeOutput(double[][] normalOutputMatrix)
        {
            var outputData = new double[Parameters.LeadWindowSize][];
            for (int i = 0; i < Parameters.LeadWindowSize; i++)
            {
                var normalOutput = Normalizer.DenormalizeOutput(normalOutputMatrix[i]);
                outputData[i] = new double[Parameters.OutputColumns.Count()];
                for (int j = 0; j < Parameters.OutputColumns.Count(); j++)
                {
                    outputData[i][j] = normalOutput[j];
                }

            }
            return outputData;
        }

        public static byte[] Serialize(NeuralNetworkInstance obj)
        {
            var blob = BlobConverter<NeuralNetworkInstance>.Encode(obj);
            return blob;
        }

        public static NeuralNetworkInstance Deserialize(byte[] blob)
        {
            var obj = BlobConverter<NeuralNetworkInstance>.Decode(blob);
            return obj;
        }
    }
}
