using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlObjectWrapper
{
    public static class DbContext
    {
        public static EntityModel.NeuronNetworkDBEntities GetModel() => new EntityModel.NeuronNetworkDBEntities();
    }
}
