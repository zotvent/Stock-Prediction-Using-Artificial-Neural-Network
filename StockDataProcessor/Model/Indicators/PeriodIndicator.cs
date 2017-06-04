using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDataProcessor.Model.Indicators
{
    public abstract class PeriodIndicator : IndexIndicator
    {
        public class PeriodIndicatorParameters : IndexIndicatorParameters
        {
            public int Period { get; set; }
        }

        public override void Init(BaseIndicatorParameters parameters)
        {
            base.Init(parameters);
            var castParams = (PeriodIndicatorParameters)parameters;
            Period = castParams.Period;
        }

        public PeriodIndicator() : base()
        {
            
        }

        public int Period { get; set; }

        public PeriodIndicator(StockHistoryExtendedList historyList, int index, int period) : base(historyList, index)
        {
            Period = period;
        }

        public override bool HasParameters(BaseIndicatorParameters parameters)
        {
            var parameter = (PeriodIndicatorParameters)parameters;
            return Period == parameter.Period;
        }
    }
}
