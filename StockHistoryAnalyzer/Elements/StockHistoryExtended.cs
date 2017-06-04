using SqlObjectWrapper.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockHistoryAnalyzer.Elements
{
    public class StockHistoryExtended
    {

        public double Open => GetIndicator("Open").Value;
        public double Close => GetIndicator("Close").Value;
        public double High => GetIndicator("High").Value;
        public double Low => GetIndicator("Low").Value;
        public double Volume => GetIndicator("Volume").Value;

        public StockHistoryExtended(StockHistory history)
        {
            Indicators = new List<StockIndicator>{
                new StockIndicator("Open", history.Open),
                new StockIndicator("Close", history.Close),
                new StockIndicator("High", history.High),
                new StockIndicator("Low", history.Low),
                new StockIndicator("Volume", history.Volume),
            };
        }

        public List<StockIndicator> Indicators;

        public double? GetIndicator(string name)
        {
            return Indicators.FirstOrDefault(x => x.Name == name)?.Value;
        }

        public IEnumerable<StockIndicator> SelectIndicators(string[] names)
        {
            return Indicators.Where(x => names.Contains(x.Name));
        }
    }
}
