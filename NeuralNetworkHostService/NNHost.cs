using NeuralNetworkHostService.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHostService
{
    public class NNHost
    {
        public List<NeuronNetworkContainer> RunningNetworks { get; private set; }

        public NNHost()
        {
            RunningNetworks = new List<NeuronNetworkContainer>();
        }
    }
}
