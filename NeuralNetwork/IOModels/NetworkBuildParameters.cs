using ExtensionUtils.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.IOModels
{
    public class NetworkBuildParameters
    {
        public int InputCount { get; set; }
        public int OutputCount { get; set; }
        public int[] HiddenLayersConfig { get; set; }

        public ValueRange<double> WeightRange { get; set; }
    }
}
