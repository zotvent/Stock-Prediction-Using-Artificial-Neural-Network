using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NetworkElements
{
    [Serializable]
    public class NeuronNetwork
    {
        public delegate double ActivationFunctionDelegate(double input);
        public delegate double ActivationFunctionDerivativeDelegate(double input);

        public List<NeuronLayer> HiddenLayers;

        public NeuronLayer InputLayer;

        public NeuronLayer OutputLayer;

        public int InputCount
        {
            get
            {
                return InputLayer.Neurons.Count;
            }
        }
        public int OutputCount
        {
            get
            {
                return OutputLayer.Neurons.Count;
            }
        }

        public int[] HiddenLayersNeuronCount
        {
            get
            {
                return HiddenLayers.Select(x => x.Neurons.Count).ToArray();
            }
        }

        private double _learningSpeed = 0;
        public double LearningSpeed
        {
            get
            {
                return _learningSpeed;
            }
            set
            {
                _learningSpeed = value;
                foreach (var neuron in this.NetworkNeurons)
                {
                    neuron.LearningSpeed = _learningSpeed;
                }
            }
        }

        public List<NeuronLayer> NetworkLayers
        {
            get
            {
                var layers = new List<NeuronLayer>();
                layers.Add(InputLayer);
                layers.AddRange(HiddenLayers);
                layers.Add(OutputLayer);
                return layers;
            }
        }

        public List<Neuron> NetworkNeurons
        {
            get
            {
                var neurons = new List<Neuron>();
                neurons = NetworkLayers.Aggregate<NeuronLayer, List<Neuron>>(
                    neurons,
                    (neuronsList, nextLayer) =>
                    neuronsList.Concat(nextLayer.Neurons).ToList()
                );
                return neurons;
            }
        }

        public ActivationFunctionDelegate ActivationFunction { get; set; }
        public ActivationFunctionDerivativeDelegate ActivationFunctionDerivative { get; set; }

        public NeuronNetwork()
        {
            HiddenLayers = new List<NeuronLayer>();
        }
    }
}
