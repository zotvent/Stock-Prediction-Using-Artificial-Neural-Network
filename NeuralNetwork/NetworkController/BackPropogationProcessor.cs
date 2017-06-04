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
    public class BackPropogationProcessor
    {
        private static void ProcessOutputLayerErrors(NeuronLayer layer, List<double> correctOutputs)
        {
            var neurons = layer.Neurons;
            if (neurons.Count != correctOutputs.Count)
                throw new DataMisalignedException("Layer outputs count is not the same as check data length.!");
            for(int i = 0; i< neurons.Count; i++)
            {
                neurons[i].SignalError = correctOutputs[i] - neurons[i].OutputSignal;
            }
        }

        private static void ProcessLayerConfiguration(NeuronLayer layer,
            ActivationFunctionDerivativeDelegate activationDerivative)
        {
            foreach (var neuron in layer.Neurons)
            {
                CorrectNeuronConfiguration(neuron,activationDerivative);
            }
        }

        private static void CorrectNeuronConfiguration(Neuron neuron, ActivationFunctionDerivativeDelegate  activationDerivative)
        {
            neuron.SignalError = neuron.SignalError * activationDerivative(neuron.InputSignal);
          
            neuron.InputSignalShift += neuron.LearningSpeed * neuron.SignalError;

            foreach (var inputConnection in neuron.InputConnections)
            {
                var sourceNeuron = inputConnection.Source;
                sourceNeuron.SignalError += neuron.SignalError * inputConnection.Weight;
                inputConnection.Weight += neuron.LearningSpeed * sourceNeuron.OutputSignal * neuron.SignalError;
                    
            }
        }

        public static void ProcessBackPropagation(NeuronNetwork network, List<double> correctOutputs)
        {
            var activationDerivative = network.ActivationFunctionDerivative;
            ProcessOutputLayerErrors(network.OutputLayer,correctOutputs);

            ProcessLayerConfiguration(network.OutputLayer,activationDerivative);
            var hiddenLayers = network.HiddenLayers;
            for (int i = hiddenLayers.Count - 1; i>= 0; i--)
            {
                ProcessLayerConfiguration(hiddenLayers[i], activationDerivative);
            }
        }
    }
}
