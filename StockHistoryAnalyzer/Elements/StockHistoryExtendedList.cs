using SqlObjectWrapper.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockHistoryAnalyzer.Elements
{
    public class StockHistoryExtendedList : List<StockHistoryExtended>
    {
        public List<List<StockIndicator>> GetIndicators(int startIndex, int endIndex, string[] indicators)
        {
            for(int i= startIndex; i<= endIndex;i++)
            {

            }
        }

        public StockHistoryExtendedList(List<StockHistory> list):base()
        {
            this.AddRange(list.Select(x => new StockHistoryExtended(x)));
        }

        public double CalcIndicator(int index, string name)
        {
            var realValue = this[index].GetIndicator(name);
            double indicatorValue = 0;

            if (realValue.HasValue)
                indicatorValue = realValue.Value;
            else
            {
                var basicName = name;
                var parameters = "";
                var param = new List<string>();
                var parametersIndex = name.IndexOf("(");

                if (parametersIndex != -1)
                {
                    basicName = name.Substring(0, parametersIndex);
                    parameters = name.Substring(parametersIndex);
                    parameters = parameters.Trim('(', ')');
                    param.AddRange(parameters.Split(','));
                }

                if (basicName == "RSI")
                {
                    indicatorValue = RSI(index, int.Parse(param[0]));
                }
                if (basicName == "SMA")
                {
                    indicatorValue = SMA(index, int.Parse(param[0]));
                }
                if (basicName == "EMA")
                {
                    indicatorValue = EMA(index, int.Parse(param[0]));
                }
                if (basicName == "MACD")
                {
                    indicatorValue = MACD(index, int.Parse(param[0]),int.Parse(param[1]));
                }
                if (basicName == "SIGNAL")
                {
                    indicatorValue = SIGNAL(index, int.Parse(param[0]), int.Parse(param[1]), int.Parse(param[2]));
                }
                if (basicName == "STOCH")
                {
                    indicatorValue = STOCH(index, int.Parse(param[0]));
                }

                var indicator = new StockIndicator(name, indicatorValue);
                this[index].Indicators.Add(indicator);
            }
            return indicatorValue;
        }

        private double STOCH(int index, int period)
        {
            var close = this[index].Close;
            var range = GetRange(index - period + 1, period);
            var minLow = range.Min(x => x.Low);
            var maxHigh = range.Max(x => x.High);

            var stoch = (close - minLow) / (maxHigh - minLow);
            return stoch;
        }

        private double SIGNAL(int index, int p1, int p2, int period)
        {
            double sum = 0;
            for (int i = index - period + 1; i <= index; i++)
            {
                sum += CalcIndicator(index, $"MACD({p1},{p2})");
            }
            var sma = sum / period;
            return sma;
        }

        private double MACD(int index, int p1, int p2)
        {
            var ema1 = CalcIndicator(index, $"EMA({p1})");
            var ema2 = CalcIndicator(index, $"EMA({p2})");
            return ema1 - ema2;
        }

        private double SMA(int index, int period)
        {
            double sum = 0;
            for (int i = index - period + 1; i <= index; i++)
            {
                sum += this[i].Close;
            }
            var sma = sum / period;
            return sma;
        }

        private double EMA(int index ,int period)
        {
            double ema = 0;
            var startIndex = index - period;
            if (startIndex <= 0)
            {
                ema = CalcIndicator(index, $"SMA({period})");
            }
            else
            {
                double prevEma = CalcIndicator(index-1, $"EMA({period})");
                double currVal = this[index].Close;
                ema = (currVal - prevEma) * (2 / (period + 1)) + prevEma;
            }
            return ema;
        }

        public double RSI(int index, int period)
        {
            double posSum = 0;
            double negSum = 0;

            int posCount = 0;
            int negCount = 0;

            for(int i= index - period + 1; i <= index; i++ )
            {
                var closeDiff = this[i].Close - this[i - 1].Close;
                if (closeDiff > 0)
                {
                    posSum += closeDiff;
                    posCount++;
                }
                else
                {
                    negSum -= closeDiff;
                    negCount++;
                }
            }
            

            var rsi = 1 - 1 / (1 + (posSum/posCount) / (negSum/negCount));
            return rsi;
        }
    }
}
