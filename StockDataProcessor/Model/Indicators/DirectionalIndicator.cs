using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDataProcessor.Model.Indicators
{
    public abstract class DirectionalIndicator : IndexIndicator
    {
        public enum IndicatorDirection
        {
            Positive,
            Negative
        }

        public class DirectionIndicatorParameters : IndexIndicatorParameters
        {
            public IndicatorDirection Direction { get; set; }
        }
        
        public IndicatorDirection Direction { get; set; }

        public override void Init(BaseIndicatorParameters parameters)
        {
            base.Init(parameters);
            var castParameters = (DirectionIndicatorParameters)parameters;
            Direction = castParameters.Direction;
        }

        public DirectionalIndicator() : base()
        {

        }

        public DirectionalIndicator(StockHistoryExtendedList historyList, int index, IndicatorDirection direction) : base(historyList, index)
        {
            Direction = direction;
        }

        public override bool HasParameters(BaseIndicatorParameters parameters)
        {
            var parameter = (DirectionIndicatorParameters)parameters;
            return Direction == parameter.Direction;
        }
    }
}
