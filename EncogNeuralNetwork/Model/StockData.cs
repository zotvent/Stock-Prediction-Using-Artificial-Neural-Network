using EncogNeuralNetwork.Model.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EncogNeuralNetwork.Model.Indicators.StockIndicator;

namespace EncogNeuralNetwork.Model
{
    public class StockData
    {
        public long DayNumber { get; set; }

        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Volume { get; set; }

        public List<StockIndicator> Indicators { get; set; } = new List<StockIndicator>();

        public double GetStandard(IndicatorType type)
        {
            double value = 0;
            switch (type)
            {
                case StockIndicator.IndicatorType.Open:
                    value = Open;
                    break;
                case StockIndicator.IndicatorType.Close:
                    value = Close;
                    break;
                case StockIndicator.IndicatorType.High:
                    value =  High;
                    break;
                case StockIndicator.IndicatorType.Low:
                    value = Low;
                    break;
                case StockIndicator.IndicatorType.Volume:
                    value = Volume;
                    break;
            }
            return value;
        }
        
        public StockIndicator IndicatorOrDefault(StockIndicator search)
        {
            return Indicators.FirstOrDefault(x => x.EqualParameters(search));
        }

        public DateTime Date
        {
            get
            {
                var date = new DateTime(DayNumber* TimeSpan.TicksPerDay);
                return date;
            }
            set
            {
                DayNumber = value.Ticks / TimeSpan.TicksPerDay;
            }
        }
    }
}
