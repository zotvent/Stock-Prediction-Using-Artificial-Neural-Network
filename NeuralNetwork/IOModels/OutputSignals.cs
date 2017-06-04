using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.IOModels
{
    public class OutputSignals : List<double>
    {
        public double[] Array
        {
            get
            {
                return ToArray();
            }
        }
    }
}
