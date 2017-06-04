using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockHistoryAnalyzer.Elements
{
    public class StockIndicator
    {
        public StockIndicator(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public double Value { get; set; }
    }
}
