using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StockDataProcessor.Model.StockHistoryExtended;

namespace StockDataProcessor.Model.Indicators.IndexIndicators
{
    public class StandardIndicator : IndexIndicator
    {
        public class StandardIndicatorParameters : IndexIndicatorParameters
        {
            public IndicatorType Type { get; set; }
        }

        public override void Init(BaseIndicatorParameters parameters)
        {
            base.Init(parameters);
            var castParameters = (StandardIndicatorParameters)parameters;
            Type = castParameters.Type;
        }

        public StandardIndicator() :base()
        {

        }

        public StandardIndicator(StockHistoryExtendedList historyList, int index, IndicatorType type) : base(historyList, index)
        {
            Type = type;
        }

        public IndicatorType Type { get; set; }

        public override string Name => Enum.GetName(typeof(IndicatorType), Type);

        public override double CalculateValue()
        {
            var curr = HistoryList[Index];
            double value = 0;
            switch(Type)
            {
                case IndicatorType.Open:
                    value = curr.BaseObject.Open;
                    break;
                case IndicatorType.Close:
                    value = curr.BaseObject.Close;
                    break;
                case IndicatorType.High:
                    value = curr.BaseObject.High;
                    break;
                case IndicatorType.Low:
                    value = curr.BaseObject.Low;
                    break;
                case IndicatorType.Volume:
                    value = curr.BaseObject.Volume;
                    break;
            }
            return value;
        }

        public override bool HasParameters(BaseIndicatorParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
