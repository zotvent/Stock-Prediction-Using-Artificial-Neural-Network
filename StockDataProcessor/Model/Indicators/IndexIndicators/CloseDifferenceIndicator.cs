using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDataProcessor.Model.Indicators.IndexIndicators
{
    class CloseDifferenceIndicator : DirectionalIndicator
    {

        public CloseDifferenceIndicator(StockHistoryExtendedList historyList, int index, IndicatorDirection direction) : base(historyList, index, direction)
        {
        }

        public override string Name => "CloseDifference";

        public override double CalculateValue()
        {
            var current = HistoryList[Index];
            var previous = HistoryList[Index - 1];

            var diff = current.BaseObject.Close - previous.BaseObject.Close;
            var higher = Direction == IndicatorDirection.Positive;

            diff = higher ? diff : -diff;
            diff = diff < 0 ? 0 : diff;
            return diff;
        }

        public override bool HasParameters(BaseIndicatorParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
