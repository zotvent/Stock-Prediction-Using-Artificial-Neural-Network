using System.Linq;
using Encog.ML.Data;
using Encog.ML.Data.Temporal;

namespace EncogPredictor.NeuralNetwork
{
    public class TrainingSetBuilder
    {
        public static IMLDataSet GenerateTrainingSet(double[][] inputData, double[][] outputData, int leadWindowSize, int lagWindowSize)
        {
            var temporalDataSet = new TemporalMLDataSet(lagWindowSize, leadWindowSize);

            int inputCount = inputData[0].Length;
            int outputCount = outputData[0].Length;

            for (int i = 0; i < inputCount; i++)
            {
                var desc = new TemporalDataDescription(TemporalDataDescription.Type.Raw, true, false);
                temporalDataSet.AddDescription(desc);
            }
            for (int i = 0; i < outputCount; i++)
            {
                var desc = new TemporalDataDescription(TemporalDataDescription.Type.Raw, false, true);
                temporalDataSet.AddDescription(desc);
            }
            

            for (int i=0;i < inputData.Length; i++)
            {
                var point = temporalDataSet.CreatePoint(i);
                point.Data = inputData[i].Concat(outputData[i]).ToArray();
            }

            temporalDataSet.Generate();

            return temporalDataSet;
        }
    }
}
