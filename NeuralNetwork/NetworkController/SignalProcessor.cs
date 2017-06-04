using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork.NetworkElements;
using static NeuralNetwork.NetworkElements.NeuronNetwork;

namespace NeuralNetwork.NetworkController
{
    public static class SignalProcessor
    {

        private static void ProcessNeuronInputs(Neuron neuron, ActivationFunctionDelegate activationFunction)
        {
            var sumInput = neuron.InputSignalShift + neuron.InputSignal;
            neuron.OutputSignal = activationFunction(sumInput);
            foreach (var outputConnection in neuron.OutputConnections)
            {
                outputConnection.Target.InputSignal += neuron.OutputSignal * outputConnection.Weight;
            }
        }

        private static void ProcessLayerInputs(NeuronLayer layer, ActivationFunctionDelegate activationFunction)
        {
            foreach (var neuron in layer.Neurons)
            {
                ProcessNeuronInputs(neuron, activationFunction);
            }
        }

        private static void PassInputs(NeuronLayer layer, List<double> inputs)
        {
            var neurons = layer.Neurons;

            if (inputs.Count != neurons.Count)
                throw new InvalidDataException("Inputs data is wrong formatted!");

            for (int i = 0; i < neurons.Count; i++)
            {
                neurons[i].InputSignal = inputs[i];
                neurons[i].OutputSignal = inputs[i];
            }
        }
        

        public static List<double> ProcessInputSignals(NeuronNetwork network, List<double> inputSignals)
        {
            var activationFunction = network.ActivationFunction;

            PassInputs(network.InputLayer,inputSignals);

            ProcessLayerInputs(network.InputLayer, activationFunction);

            foreach (var layer in network.HiddenLayers)
            {
                ProcessLayerInputs(layer, activationFunction);
            }

            ProcessLayerInputs(network.OutputLayer, activationFunction);

            return network.OutputLayer.Neurons.Select(n => n.OutputSignal).ToList();
        }

        public static void ResetNetwork(NeuronNetwork network)
        {
            ResetLayer(network.InputLayer);
            ResetLayer(network.OutputLayer);
            foreach (var layer in network.HiddenLayers)
            {
                ResetLayer(layer);
            }
        }

        private static void ResetLayer(NeuronLayer layer)
        {
            foreach (var neuron in layer.Neurons)
            {
                ResetNeuron(neuron);
            }
        }

        private static void ResetNeuron(Neuron neuron)
        {
            neuron.InputSignal = 0;
            neuron.OutputSignal = 0;
            neuron.SignalError = 0;
        }
    }
}
