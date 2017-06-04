using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NetworkElements
{
    [Serializable]
    public class Neuron
    {
        public double InputSignal { get; set; } 
        public double OutputSignal { get; set; }
        public double InputSignalShift { get; set; }
        public double SignalError { get; set; }
        public double LearningSpeed { get; set; }

        public List<NeuronConnection> InputConnections;
        public List<NeuronConnection> OutputConnections;

        public Neuron()
        {
            InputConnections = new List<NeuronConnection>();
            OutputConnections = new List<NeuronConnection>();
            InputSignal = 0;
            OutputSignal = 0;
            LearningSpeed = 1;
            SignalError = 0;
        }
    }
}
