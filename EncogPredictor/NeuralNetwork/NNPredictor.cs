using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using SqlObjectWrapper.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using static EncogPredictor.NeuralNetwork.PredictorParameters;

namespace EncogPredictor.NeuralNetwork
{
    [Serializable]
    public class NNPredictor
    {
        public double MaxError { get; set; } = 0.005;

        public NNPredictor()
        {

        }

        public NNPredictor(IEnumerable<StockHistory> trainingSet, PredictorParameters parameters)
        {
            Parameters = parameters;
            Network = GenerateNeuralNetwork();
            var encogTrainingSet = GenerateTrainingSet(trainingSet);
            Train(encogTrainingSet);
        }

        public DataNormalizer Normalizer { get; set; }
        public PredictorParameters Parameters { get; set; }
        public BasicNetwork Network { get; set; }

        public double[][] Predict(IEnumerable<StockHistory> input)
        {
            var inputs = GetRawData(input, Parameters.InputColumns);
            var normalInputs = Normalizer.NormalizeInputs(inputs);
            var nnInputs = normalInputs.Aggregate((x,y) => x.Concat(y).ToArray());

            var nnOutput = new double[Parameters.LeadWindowSize * Parameters.OutputColumns.Length];
            Network.Compute(nnInputs, nnOutput);

            var predictedOutput = new double[Parameters.LeadWindowSize][];

            for(int i=0;i<Parameters.LeadWindowSize; i++)
            {
                var outputRow = new double[Parameters.OutputColumns.Length];
                Array.Copy(nnOutput, i * Parameters.OutputColumns.Length, outputRow, 0, Parameters.OutputColumns.Length);
                var rawOutputRow = Normalizer.DenormalizeOutput(outputRow);
                predictedOutput[i] = rawOutputRow;
            }

            return predictedOutput;
        }

        private void Train(IMLDataSet trainingSet)
        {
            ITrain training = new ResilientPropagation(Network, trainingSet);
            var epoch = 0;

            do
            {
                training.Iteration();
                epoch++;
            }
            while (training.Error > MaxError && epoch != 1000);
            
        }

        private BasicNetwork GenerateNeuralNetwork()
        {
            var inputsCount = Parameters.LagWindowSize * Parameters.InputColumns.Length;
            var outputsCount = Parameters.LeadWindowSize * Parameters.OutputColumns.Length;

            var network = new BasicNetwork();

            network.AddLayer(new BasicLayer(new ActivationLinear(), true, inputsCount));
            network.AddLayer(new BasicLayer(inputsCount * 6));
            network.AddLayer(new BasicLayer(inputsCount * 3));
            network.AddLayer(new BasicLayer(outputsCount * 3));
            network.AddLayer(new BasicLayer(new ActivationTANH(),false,outputsCount));
            network.Structure.FinalizeStructure();
            network.Reset();

            return network;
        }

        private IMLDataSet GenerateTrainingSet(IEnumerable<StockHistory> trainingSet)
        {
            var inputs = GetRawData(trainingSet, Parameters.InputColumns);
            var outputs = GetRawData(trainingSet, Parameters.OutputColumns);

            Normalizer = new DataNormalizer(inputs, outputs, -0.9, 0.9);

            var normalInputs = Normalizer.NormalizeInputs(inputs);
            var normalOutputs = Normalizer.NormalizeOutputs(outputs);

            var encogTraingSet = TrainingSetBuilder.GenerateTrainingSet(normalInputs, normalOutputs, Parameters.LeadWindowSize, Parameters.LagWindowSize);

            return encogTraingSet;
        }

        public static double[][] GetRawData(IEnumerable<StockHistory> history, StockIndicators[] columns)
        {
            var rawData = new double[history.Count()][];
            for (int i = 0; i < history.Count(); i++)
            {
                var dataRow = new double[columns.Length];

                var historyDay = history.ElementAt(i);
                for (int j = 0; j < columns.Length; j++)
                {
                    var column = columns[j];
                    double value = 0;
                    switch (column)
                    {
                        case StockIndicators.Close:
                            value = historyDay.Close;
                            break;
                        case StockIndicators.Open:
                            value = historyDay.Open;
                            break;
                        case StockIndicators.High:
                            value = historyDay.High;
                            break;
                        case StockIndicators.Low:
                            value = historyDay.Low;
                            break;
                        case StockIndicators.Volume:
                            value = historyDay.Volume;
                            break;
                    }
                    dataRow[j] = value;
                }
                rawData[i] = dataRow;
            }

            return rawData;
        }
    }
}
