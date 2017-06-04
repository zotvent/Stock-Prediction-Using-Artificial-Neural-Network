using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDataProcessor.Model.Indicators.IndexIndicators
{
    public class TrueRangeIndicator : IndexIndicator
    {
        public TrueRangeIndicator(StockHistoryExtendedList historyList, int index) : base(historyList, index)
        {
        }

        public TrueRangeIndicator() : base()
        {

        }

        public override string Name => "TrueRange";

        public override double CalculateValue()
        {
            var current = HistoryList[Index];
            var previous = HistoryList[Index - 1];

            var firstRange = Math.Abs(current.BaseObject.High - previous.BaseObject.High);
            var secondRange = Math.Abs(current.BaseObject.High - previous.BaseObject.Close);
            var thirdRange = Math.Abs(current.BaseObject.Low - previous.BaseObject.Low);

            var maxRange = Math.Max(Math.Max(firstRange, secondRange), thirdRange);

            return maxRange;
        }
    }
}
