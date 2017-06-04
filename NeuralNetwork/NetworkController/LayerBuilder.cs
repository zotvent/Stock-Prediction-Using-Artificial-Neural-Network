using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork.NetworkElements;
using NeuralNetwork.Utils;

namespace NeuralNetwork.NetworkController
{
    class LayerBuilder
    {
        public static NeuronLayer BuildLayer(int neuronCount,double learningSpeed, NeuronLayer source = null)
        {
            var layer = new NeuronLayer(source);
            
            if (source != null)
            {
                source.OutputLayer = layer;
                for (var i = 0; i < neuronCount; i++)
                {
                    var targetNeuron = new Neuron()
                    {
                        LearningSpeed = learningSpeed
                    };
                    foreach (var sourceNeuron in source.Neurons)
                    {
                        var neuronConnection = new NeuronConnection(sourceNeuron, targetNeuron);
                        targetNeuron.InputConnections.Add(neuronConnection);
                        sourceNeuron.OutputConnections.Add(neuronConnection);
                    }
                    layer.Neurons.Add(targetNeuron);
                }
            }
            else
            {
                for (var i = 0; i < neuronCount; i++)
                {
                    var targetNeuron = new Neuron()
                    {
                        LearningSpeed = learningSpeed
                    };
                    layer.Neurons.Add(targetNeuron);
                }

            }
            return layer;
        }

        public static void RandomizeWeights(NeuronLayer layer, double min, double max)
        {
            var neurons = layer.Neurons;
            foreach (var neuron in neurons)
            {
                foreach (var connection in neuron.OutputConnections)
                {
                    connection.Weight = Randomizer.GetRandomDouble(min, max);
                } 
            }
        }

        public static void RandomizeInputShifts(NeuronLayer layer, double min, double max)
        {
            var neurons = layer.Neurons;
            foreach (var neuron in neurons)
            {
                neuron.InputSignalShift = Randomizer.GetRandomDouble(min, max);
            }
        }
    }
}