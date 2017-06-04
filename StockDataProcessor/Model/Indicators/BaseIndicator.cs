using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDataProcessor.Model.Indicators
{
    public abstract class BaseIndicator
    {
        public abstract class BaseIndicatorParameters
        {
            public StockHistoryExtendedList HistoryList { get; set; }
        }

        public virtual void Init(BaseIndicatorParameters parameters)
        {
            HistoryList = parameters.HistoryList;
        }

        public abstract bool HasParameters(BaseIndicatorParameters parameters);

        public BaseIndicator()
        {
        }

        public abstract string Name { get; }

        private double? _value;

        public void UpdateValue()
        {
            _value = CalculateValue();
        }

        public double Value
        {
            get
            {
                if (!_value.HasValue)
                {
                    UpdateValue();
                }
                return _value.Value;
            }
        }

        public abstract double CalculateValue();

        public StockHistoryExtendedList HistoryList { get; set; }

        public BaseIndicator(StockHistoryExtendedList historyList)
        {
            HistoryList = historyList;
        }

        
    }
}
