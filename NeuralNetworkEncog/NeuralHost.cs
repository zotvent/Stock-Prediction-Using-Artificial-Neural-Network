using NeuralNetworkEncog.Controller;
using NeuralNetworkEncog.Elements;
using SqlObjectWrapper.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkEncog
{
    public class NeuralHost
    {
        public static NeuralNetworkInstance LoadNetwork(byte[] blob)
        {
            var network = NeuralNetworkInstance.Deserialize(blob);
            return network;
        }

        public static NeuralNetworkInstance CreateNetwork(IEnumerable<StockHistory> history, NetworkParameters parameters)
        {
            var network = LearningController.LearnNeuralNetwork(history, parameters);
            return network;
        }
    }
}
