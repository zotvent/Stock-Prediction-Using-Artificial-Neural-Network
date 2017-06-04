using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDataProcessor.Model.Indicators.IndexIndicators
{
    public class DirectionalMovementIndicator : DirectionalIndicator
    {
        public DirectionalMovementIndicator(StockHistoryExtendedList historyList, int index, IndicatorDirection direction) : base(historyList, index, direction)
        {
        }

        public override string Name => "DirectionalMovement";

        public override double CalculateValue()
        {
            var prev = HistoryList[Index - 1];
            var curr = HistoryList[Index];

            var highDiff = curr.BaseObject.High - prev.BaseObject.High;
            var lowDiff = prev.BaseObject.Low - curr.BaseObject.Low;


            double movement = 0;
            if (Direction == IndicatorDirection.Positive)
            {
                movement = highDiff > 0 ? highDiff : 0;
            }
            else
            {
                movement = lowDiff > 0 ? lowDiff : 0;
            }

            return movement;
        }

        




    }
}
