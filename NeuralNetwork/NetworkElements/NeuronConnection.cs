using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NetworkElements
{
    [Serializable]
    public class NeuronConnection
    {
        public Neuron Source { get; set; }
        public Neuron Target { get; set; }
        public double Weight { get; set; }

        public NeuronConnection()
        {
            Weight = 0;
        }

        public NeuronConnection(Neuron source, Neuron target, double weight = 1)
        {
            this.Source = source;
            this.Target = target;
            this.Weight = weight;
        }
    }
}
