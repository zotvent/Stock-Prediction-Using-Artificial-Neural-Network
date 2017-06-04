using SqlObjectWrapper.EntityModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksService.DataAdapter
{
    public class CsvConverter
    {
        public static int duration = 0;
        public class CsvConfiguration
        {
            public bool HasTitleRow { get; set; } = true;
            public string DateFormat { get; set; } = "d-MMM-yy";
            public char Separator { get; set; } = ',';
        }
        public static Stock CsvToStock(string ticker, string name, string historyCsv,CsvConfiguration config = null)
        {
            var begin = DateTime.Now;
            if(config == null)
            {
                config = new CsvConfiguration();
            }
            
            var stockObj = new Stock()
            {
                Name = name,
                Ticker = ticker
            };

            
            var reader = new StringReader(historyCsv);
            if (config.HasTitleRow)
                reader.ReadLine();

            var line = "";
            while(!string.IsNullOrEmpty(line = reader.ReadLine()))
            { 
                var tokens = line.Split(config.Separator);
                var historyObj = new StockHistory()
                {
                    Date = DateTime.ParseExact(tokens[0], config.DateFormat, CultureInfo.InvariantCulture),
                    Open = double.Parse(tokens[1]),
                    High = double.Parse(tokens[2]),
                    Low = double.Parse(tokens[3]),
                    Close = double.Parse(tokens[4]),
                    Volume = double.Parse(tokens[5]),
                };
                stockObj.History.Add(historyObj);
            }
            duration = (DateTime.Now - begin).Milliseconds;
            return stockObj;
        }
    }
}
