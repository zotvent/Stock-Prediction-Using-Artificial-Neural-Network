using NeuralNetwork.NetworkElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NetworkController
{
    public static class NetworkProcessingExtension
    {
        public static List<double> ProcessInput(this NeuronNetwork network, List<double> input)
        {
           var output = SignalProcessor.ProcessInputSignals(network, input);
           SignalProcessor.ResetNetwork(network);
           return output;
        }

        public static List<List<double>> ProcessInput(this NeuronNetwork network, List<List<double>> inputCollection)
        {
            var outputs = new List<List<double>>();
            foreach (var input in inputCollection)
            {
                outputs.Add(ProcessInput(network, input));
            }
            return outputs;
        }

        public static void ProcessLearning(this NeuronNetwork network, Tuple<List<double>,List<double>> learningPair)
        {
            SignalProcessor.ProcessInputSignals(network, learningPair.Item1);
            BackPropogationProcessor.ProcessBackPropagation(network, learningPair.Item2);
            SignalProcessor.ResetNetwork(network);
        }

        public static void ProcessLearning(this NeuronNetwork network, List<Tuple<List<double>, List<double>>> learningSet)
        {
            foreach (var learningPair in learningSet)
            {
                ProcessLearning(network, learningPair);
            }
        }
    }
}
