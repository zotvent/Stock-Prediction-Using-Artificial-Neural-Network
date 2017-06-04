using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NetworkElements
{
    [Serializable]
    public class NeuronLayer
    {   
        public List<Neuron> Neurons { get; set; }
        public NeuronLayer InputLayer { get; set; }
        public NeuronLayer OutputLayer { get; set; }

        public NeuronLayer()
        {
            Neurons = new List<Neuron>();
        }

        public NeuronLayer(NeuronLayer inputLayer) : this()
        {
            this.InputLayer = inputLayer;
        }
    }
}
