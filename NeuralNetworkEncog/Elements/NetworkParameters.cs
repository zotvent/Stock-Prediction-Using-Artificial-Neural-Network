
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkEncog.Elements
{
    [Serializable]
    public class NetworkParameters
    {
        public NetworkParameters()
        { }

        public int[] InputColumns { get; set; }
        public int[] OutputColumns { get; set; }

        public int LagWindowSize { get; set; }
        public int LeadWindowSize { get; set; }
    }
}
