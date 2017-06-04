using SqlObjectWrapper;
using SqlObjectWrapper.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHostService.Containers
{
    public class NeuronNetworkContainer
    {
        public int NetworkId;

        public NeuronNetworkModel Network => DbContext.GetModel().NeuronNetworkModels.Find(NetworkId);

        public List<NetworkInstanceContainer> RunningInstances { get; private set; }

        //Load existing neural network
        public NeuronNetworkContainer(int id)
        {
            NetworkId = id;
            RunningInstances = new List<NetworkInstanceContainer>();
        }
        
    }
}
