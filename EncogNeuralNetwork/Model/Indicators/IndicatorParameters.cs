using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EncogNeuralNetwork.Model.Indicators.IndicatorParam;

namespace EncogNeuralNetwork.Model.Indicators
{
    public class IndicatorParameters : List<IndicatorParam>
    {
        public T GetParam<T>(ParameterType type) where T : IComparable 
        {
            var parameter = (IndicatorParam<T>)this.FirstOrDefault(x => x.Type == type);
            if (parameter == null)
                return default(T);
            return parameter.CastValue;
        }

        public bool Equals(IndicatorParameters second)
        {
            var equals = false;
            if(Count == second.Count)
            {
                equals = true;
                foreach(var parameter in second)
                {
                    var search = this.Where(x => x.Equals(parameter));
                    if(search.Count() != 1)
                    {
                        equals = false;
                        break;
                    }
                }
            }
            return equals;
        }
    }
}
