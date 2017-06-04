using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork.NetworkElements;
using NeuralNetwork.Utils;
using static NeuralNetwork.NetworkElements.NeuronNetwork;

namespace NeuralNetwork.NetworkController
{
   public class NetworkBuilder
   {
       public double MinWeight { get; set; } = -0.5;
        public double MaxWeight { get; set; } = 0.5;
        public double MinShift { get; set; } = -0.5;
        public double MaxShift { get; set; } = 0.5;

        public NeuronNetwork InitNetwork(int inputCount, int outputCount,int hiddenLayersCount,
            int hiddenNeuronsCount, double learningSpeed, 
            ActivationFunctionDelegate activationFunction, ActivationFunctionDerivativeDelegate activationFunctionDerivative)
        {
            var network = new NeuronNetwork
            {
                InputLayer = LayerBuilder.BuildLayer(inputCount,learningSpeed)
            };
            network.ActivationFunction = activationFunction;
            network.ActivationFunctionDerivative = activationFunctionDerivative;

            var previousLayer = network.InputLayer;
            var hiddenLayers = new List<NeuronLayer>();

            for (int i = 0; i < hiddenLayersCount; i++)
            {
                var nextLayer = LayerBuilder.BuildLayer(hiddenNeuronsCount, learningSpeed, previousLayer);
                hiddenLayers.Add(nextLayer);
                previousLayer = nextLayer;
            }

            network.HiddenLayers = hiddenLayers;
            network.OutputLayer = LayerBuilder.BuildLayer(outputCount, learningSpeed, previousLayer);
            return network;
        }

        public void RandomizeNetwork(NeuronNetwork network)
        {
            LayerBuilder.RandomizeWeights(network.InputLayer, MinWeight, MaxWeight);
            foreach (var layer in network.HiddenLayers)
            {
                LayerBuilder.RandomizeWeights(layer, MinWeight, MaxWeight);      
                LayerBuilder.RandomizeInputShifts(layer, MinShift, MaxShift);     
            }
            LayerBuilder.RandomizeInputShifts(network.OutputLayer, MinShift, MaxShift);
        }
    }
}
