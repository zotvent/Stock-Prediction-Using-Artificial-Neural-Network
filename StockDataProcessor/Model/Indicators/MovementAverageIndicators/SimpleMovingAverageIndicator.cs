using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StockDataProcessor.Model.Indicators.BaseIndicator;
using static StockDataProcessor.Model.Indicators.IndexIndicator;

namespace StockDataProcessor.Model.Indicators.MovementAverageIndicators
{
    public class SimpleMovingAverageIndicator<ElementIndicatorType> : PeriodIndicator where ElementIndicatorType : IndexIndicator, new()
    {
        public class MovingAverageIndicatorParameters : PeriodIndicatorParameters
        {
            public int ElementParameters { get; set; }
        }

        public SimpleMovingAverageIndicator(StockHistoryExtendedList historyList, int index, int period, IndexIndicatorParameters elementsParameters) : base(historyList, index, period)
        {
            ElementParameters = elementsParameters;
        }

        public SimpleMovingAverageIndicator() : base()
        {

        }

        public IndexIndicatorParameters ElementParameters { get; set; }

        public override string Name => "SimpleMovingAverage";

        public override double CalculateValue()
        {
            var startIndex = Index - Period + 1;
            double sum = 0;

            for(int i= startIndex; i<= Index; i++)
            {
                var curr = HistoryList[i];

                var existIndicator = curr.Indicators.FirstOrDefault(x =>
                {
                    return x.GetType() == typeof(ElementIndicatorType) && x.HasParameters(ElementParameters);
                });

                ElementParameters.Index = i;
                ElementParameters.HistoryList = HistoryList;

                var indicator = new ElementIndicatorType();
                indicator.Init(ElementParameters);

                var value = indicator.Value;
                sum += value;
            }

            double sma = sum / Period;

            return sma;
        }
    }
}
