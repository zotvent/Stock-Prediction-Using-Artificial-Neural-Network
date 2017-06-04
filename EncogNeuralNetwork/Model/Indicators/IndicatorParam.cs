using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncogNeuralNetwork.Model.Indicators
{
    public abstract class IndicatorParam : IComparable
    {

        public enum ParameterType
        {
            Direction,
            Period,
            ChildIndicator
        }

        public ParameterType Type { get; set; }

        public IndicatorParam(ParameterType type)
        {
            Type = type;
        }

        public object Value { get; set; }

        public abstract bool Equals(IndicatorParam second);

        public int CompareTo(object obj)
        {
            var compare = -1;
            if(GetType().Equals(obj))
            {
                if (Equals((IndicatorParam)obj))
                    compare = 0;
            }
            return compare;
        }
    }

    public class IndicatorParam<T> : IndicatorParam where T : IComparable
    {
        public IndicatorParam(ParameterType type,T value) : base(type)
        {
            CastValue = value;
        }

        public T CastValue
        {
            get { return (T)Value; }
            set { Value = value; }
        }

        public override bool Equals(IndicatorParam second)
        {
            return Type == second.Type && CastValue.CompareTo(second.Value) == 0;
        }
    }
}
