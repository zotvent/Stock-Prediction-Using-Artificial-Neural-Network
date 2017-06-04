using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDataProcessor.Model.Indicators
{
    public abstract class IndexIndicator : BaseIndicator
    {
        public class IndexIndicatorParameters : BaseIndicatorParameters
        {
            public int Index { get; set; }
        }

        public override void Init(BaseIndicatorParameters parameters)
        {
            base.Init(parameters);
            var castParameters = (IndexIndicatorParameters)parameters;
            Index = castParameters.Index;
        }

        public IndexIndicator() : base()
        {
            
        }

        public int Index { get; set; }

        public IndexIndicator(StockHistoryExtendedList historyList, int index) : base(historyList)
        {
            Index = index;
        }

        
    }
}
