using EncogNeuralNetwork.Model.Indicators;
using SqlObjectWrapper.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EncogNeuralNetwork.Model.Indicators.IndicatorParam;
using static EncogNeuralNetwork.Model.Indicators.StockIndicator;

namespace EncogNeuralNetwork.Model
{
    public class StockDataSet : List<StockData>
    {

        public StockDataSet(IEnumerable<StockHistory> data) : base()
        {
            base.AddRange(data.Select(x => new StockData()
            {
                Open = x.Open,
                Close = x.Close,
                High = x.High,
                Low = x.Low,
                Volume = x.Volume,
                Date = x.Date
            }));
        }

        public StockDataSet(List<StockData> list) : base(list)
        {
            
        }

        public StockIndicator CalcIndicator(int index, StockIndicator indicatorData)
        {
            var type = indicatorData.Type;
            var parameters = indicatorData.Parameters;

            var element = this[index];

            var indicator = element.IndicatorOrDefault(indicatorData);

            if (indicator == null)
            {
                indicator = new StockIndicator
                {
                    Type = type,
                    Parameters = parameters
                };

                if (StockIndicator.StandardTypes.Contains(type))
                {
                    indicator.Value = this[index].GetStandard(type);
                }
                else
                {
                    indicator = CalcComplexIndicator(index, indicator);
                }

                element.Indicators.Add(indicator);
                
            }
            return indicator;

        }

        private StockIndicator CalcComplexIndicator(int index, StockIndicator indicator)
        {
            var parameters = indicator.Parameters;
            var direction = parameters.GetParam<bool>(ParameterType.Direction);
            var period = parameters.GetParam<int>(ParameterType.Period);
            var childParams = parameters.GetParam<StockIndicator>(ParameterType.ChildIndicator);

            double value = 0;
            switch(indicator.Type)
            {
                case IndicatorType.MoneyFlow:
                    value = RawMoneyFlow(index, direction);
                    break;
                case IndicatorType.MoneyFlowIndex:
                    value = MoneyFlowIndex(index, period);
                    break;
                case IndicatorType.SimpleMovingAverage:
                    value = SimpleMovingAverage(index, period,childParams);
                    break;
            }

            indicator.Value = value;
            return indicator;
        }

        public double TypicalPrice(int index)
        {
            var curr = this[index];
            var tPrice = (curr.High + curr.Close + curr.Low) / 3;
            return tPrice;
        }

        public double MoneyFlowIndex(int index, int period)
        {
            var startIndex = index - period + 1;
            double positiveMF = 0;
            double negativeMF = 0;

            var positiveMfIndicator = new StockIndicator
            {
                Type = IndicatorType.MoneyFlow,
                Parameters = new IndicatorParameters
                {
                    new IndicatorParam<bool>(ParameterType.Direction,true)
                }
            };
            var negativeMfIndicator = new StockIndicator
            {
                Type = IndicatorType.MoneyFlow,
                Parameters = new IndicatorParameters
                {
                    new IndicatorParam<bool>(ParameterType.Direction,false)
                }
            };

            for (int i = startIndex; i <= index; i++)
            {
                positiveMF += CalcIndicator(i, positiveMfIndicator).Value;
                negativeMF += CalcIndicator(i, negativeMfIndicator).Value;
            }

            var mfIndex = 1.0 - 1.0 / (1.0 + positiveMF / negativeMF);

            return mfIndex;
        }

        public double RawMoneyFlow(int index, bool direction)
        {
            var currElem = this[index];
            var prev = TypicalPrice(index - 1);
            var curr = TypicalPrice(index);

            var diff = curr - prev;

            var value = direction ^ diff > 0 ? Math.Abs(diff) : 0;
            value = value * currElem.Volume;
            return value;
        }
        

        public double SimpleMovingAverage(int index, int period, StockIndicator childIndicator)
        {
            var startIndex = index - period + 1;
            double sum = 0;
            
            for(int i= startIndex; i<= index; i++)
            {
                var value = CalcIndicator(i,childIndicator).Value;
                sum += value;
            }

            var avg = sum / period;
            return avg;
        }
    }
}
