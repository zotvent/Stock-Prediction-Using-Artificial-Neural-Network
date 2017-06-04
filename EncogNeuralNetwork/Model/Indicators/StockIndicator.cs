using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncogNeuralNetwork.Model.Indicators
{
    public class StockIndicator : IComparable
    {
        public enum IndicatorType
        {
            Open,Close,High,Low,Volume,
            StochasticOscillator,
            SimpleMovingAverage,
            MoneyFlow,
            MoneyFlowIndex
        }

        public static IndicatorType[] StandardTypes = { IndicatorType.Open, IndicatorType.Close, IndicatorType.High, IndicatorType.Low, IndicatorType.Volume };

        public StockIndicator()
        {

        }

        public IndicatorType Type {get;set;}

        public IndicatorParameters Parameters { get; set; }

        public double Value { get; set; }

        public bool EqualParameters(StockIndicator obj)
        {
            return Parameters.Equals(obj.Parameters);
        }

        public int CompareTo(object obj)
        {
            var compare = -1;
            if (GetType().Equals(obj))
            {
                if (EqualParameters((StockIndicator)obj))
                    compare = 0;
            }
            return compare;
        }
    }
}
