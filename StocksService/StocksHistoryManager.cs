using SqlObjectWrapper;
using SqlObjectWrapper.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using EntityFramework.Extensions;
using System.Text;
using System.Threading.Tasks;
using StocksService.DataAdapter;
using System.IO;
using System.Globalization;

namespace StocksService
{
    public static class StocksHistoryManager
    {
        public static List<StockHistory> GetStocksHistory(string ticker,DateTime from,DateTime to)
        {
            using (var model = DbContext.GetModel())
            {

                var companyStocks = model.Stocks.FirstOrDefault(x => string.Equals(x.Ticker, ticker, StringComparison.InvariantCultureIgnoreCase));

                var requestedPeriod = companyStocks?.History.Where(x => x.Date >= from && x.Date <= to);

                return requestedPeriod.ToList();
            }
        }

        public static string WriteStocksHistory(string ticker,string name, string csv)
        {
            
            var model = DbContext.GetModel();
            var result = "";
            try
            {
                var begin = DateTime.Now;

                var stocksHistory = new List<StockHistory>();

                var companyStock = model.Stocks.FirstOrDefault(x=> x.Ticker == ticker);
                if (companyStock == null)
                {
                    companyStock = new Stock();
                    companyStock.Ticker = ticker;
                    companyStock.Name = name;
                    companyStock = model.Stocks.Add(companyStock);
                }
                
                

                var endStockSelect = (DateTime.Now - begin).TotalMilliseconds;
                begin = DateTime.Now;

                var reader = new StringReader(csv);
                    reader.ReadLine();

                var line = "";
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    var tokens = line.Split(',');
                    var historyObj = new StockHistory();
                    {
                        historyObj.StockId = companyStock.Id;
                        historyObj.Date = DateTime.ParseExact(tokens[0], "d-MMM-yy", CultureInfo.InvariantCulture);
                        historyObj.Open = double.Parse(tokens[1]);
                        historyObj.High = double.Parse(tokens[2]);
                        historyObj.Low = double.Parse(tokens[3]);
                        historyObj.Close = double.Parse(tokens[4]);
                        historyObj.Volume = double.Parse(tokens[5]);
                    };
                    stocksHistory.Add(historyObj);
                }
                model.StocksHistory.AddRange(stocksHistory);

                model.SaveChanges();
                var endSave = (DateTime.Now - begin).TotalMilliseconds;

                result = $"parse duration: {CsvConverter.duration} stockDuration: {endStockSelect} ms selectDuration:  ms Savingduration: {endSave} ms";

            }
            catch (Exception e)
            {
                result = $"error: {e.StackTrace}";
            }
            model.Dispose();
            return result;

        }
    }
}
