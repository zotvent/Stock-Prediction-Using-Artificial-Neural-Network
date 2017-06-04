using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NetworkElements
{
    public class NetworkParameters
    {
        public int InputCount
        {
            get
            {
                return _network.InputLayer.Neurons.Count;
            }
        }
        public int OutputCount
        {
            get
            {
                return _network.OutputLayer.Neurons.Count;
            }
        }
    
        public int[] HiddenLayersNeuronCount
        {
            get
            {
                return _network.HiddenLayers.Select(x => x.Neurons.Count).ToArray();
            }
        }
        public double LearningSpeed
        {
            get
            {
                double ls = 0;
                if(_network.NetworkLayers.Count > 0)
                    if(_network.NetworkLayers[0].Neurons.Count > 0)
                    {
                        ls = _network.NetworkLayers[0].Neurons[0].LearningSpeed;
                    }
                return ls;
            }
            set
            {
                foreach (var layer in _network.NetworkLayers)
                {
                    foreach(var neuron in layer.Neurons)
                    {
                        neuron.LearningSpeed = value;
                    }
                }
            }
        }

        private NeuronNetwork _network;

        public NetworkParameters(NeuronNetwork network)
        {
            this._network = network;
        }
    }
}
