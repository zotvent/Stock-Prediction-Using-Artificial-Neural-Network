using NeuralNetwork.NetworkElements;
using SqlObjectWrapper;
using SqlObjectWrapper.EntityModel;
using SqlObjectWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHostService.Containers
{
    public class NetworkInstanceContainer
    {
        public int InstanceId { get; private set; }
        public int NetworkId { get; private set; }
        public NeuronNetwork Network { get; private set; }
        //Adding new instance to the network from NeuronNetwork object
        public NetworkInstanceContainer(int networkId, NeuronNetwork network)
        {
            var context = DbContext.GetModel();
            var parent = context.NeuronNetworkModels.Find(networkId);
            if (parent != null)
            {
                NetworkId = networkId;
                Network = network;
                var blob = BlobConverter<NeuronNetwork>.Encode(network);
                var newInstance = new NetworkInstance()
                {
                    Data = blob,
                    DateCreated = DateTime.Now
                };
                parent.NetworkInstances.Add(newInstance);
                context.SaveChanges();
                InstanceId = newInstance.Id;
            }
            else
            {
                throw new IndexOutOfRangeException("Network with such id does not exist");
            }
        }

        //Loading existing instance
        public NetworkInstanceContainer(int id)
        {
            var dbInstance = DbContext.GetModel().NetworkInstances.Find(id);
            if (dbInstance != null)
            {
                InstanceId = id;
                NetworkId = dbInstance.NetworkModelId;
                Network = BlobConverter<NeuronNetwork>.Decode(dbInstance.Data);
            }
        }

        public NetworkInstance Instance
        {
            get
            {
                var context = DbContext.GetModel();
                var instance = context.NetworkInstances.Find(InstanceId);
                return instance;
            }
        }
        

        
        //Saving edited instance to db
        public NetworkInstanceContainer SaveInstance()
        {
            var context = DbContext.GetModel();
            var parentNetwork = context.NeuronNetworkModels.Find(NetworkId);
            NetworkInstanceContainer newInstanceContainer = null;
            if (parentNetwork != null)
            {
                newInstanceContainer = new NetworkInstanceContainer(NetworkId, Network);
            }
            return newInstanceContainer;
        }
    }
}
