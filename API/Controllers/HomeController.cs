using API.Models;
using EncogPredictor.NeuralNetwork;
using SqlObjectWrapper.EntityModel;
using SqlObjectWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using static EncogPredictor.NeuralNetwork.PredictorParameters;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        private string login = "admin";
        private string password = "Qwerty123";
        private string sessionKey = "6527791619a98101190377270fe582f6";
        private string sessionValue = "d27e37d48814e1d511d34d03d9f7bed6";

        private NeuronNetworkDBEntities db = new NeuronNetworkDBEntities();
        private static StockIndicators[] inputColumns = new StockIndicators[] { StockIndicators.Close, StockIndicators.Volume };
        private static StockIndicators[] outputColumns = new StockIndicators[] { StockIndicators.Close };
        private PredictorParameters parameters = new PredictorParameters()
        {
            InputColumns = inputColumns,
            OutputColumns = outputColumns,
            LagWindowSize = 3,
            LeadWindowSize = 1
        };

        public ActionResult Index()
        {
            ViewBag.Title = "Stocks";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login";

            if ((string) Session[sessionKey] == sessionValue)
            {
                return RedirectToAction("Edit");
            }

            return View();
        }

        public ActionResult Edit()
        {
            ViewBag.Title = "Control Panel";

            if (Session[sessionKey] == null || (string) Session[sessionKey] != sessionValue)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult HandleLoginForm(string username, string pwd)
        {
            if (username == login && pwd == password)
            {
                Session[sessionKey] = sessionValue;
                return RedirectToAction("Edit");
            }
            else
            {
                ModelState.AddModelError("error", "Entered username or password incorrect");
                return View("Login");
            }
        }

        public ActionResult HandleCSVForm(NetworkSeed seed)
        {  
            var stream = seed.File.InputStream;
            List<StockHistory> history = parseCSV(stream);
            var historyForTrain = history.GetRange(0, (int)(history.Count * 0.8));
            #region hide
            historyForTrain = history;
#endregion
            NNPredictor predictor = new NNPredictor(historyForTrain, parameters);
            
            var stock = new Stock
            {
                Name = seed.Name,
                Ticker = seed.Ticker,
                Data = BlobConverter<NNPredictor>.Encode(predictor)
            };
            db.Stocks.Add(stock);
            db.SaveChanges();
            var stockId = stock.Id;

            db.StocksHistory.AddRange(history.Select(x => { x.StockId = stockId; return x; }));
            savePredictedStockHistory(history, stockId, predictor);
            db.SaveChanges();

            return RedirectToAction("Edit");
        }

        private void savePredictedStockHistory(List<StockHistory> history, int id, NNPredictor predictor)
        {
            int size = parameters.LagWindowSize;
            for (int i = 0; i < history.Count - size; i++)
            {
                var output = predictor.Predict(history.GetRange(i, size));
                var predicted = new PredictedStockHistory
                {
                    Date = history[i+size].Date,
                    Close = output[0][0],
                    StockId = id
                };
                db.PredictedStockHistories.Add(predicted);
            }       
        }

        private List<StockHistory> parseCSV(Stream stream)
        {
            var stockHistoryList = new List<StockHistory>();
            var datePattern = "yyyy-MM-dd";

            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == "Date,Open,High,Low,Close,Adj Close,Volume") continue;
                    var values = line.Split(',');

                    DateTime date;
                    Double open, high, low, close, volume;

                    DateTime.TryParseExact(values[0], datePattern, null, DateTimeStyles.None, out date);
                    Double.TryParse(values[1].Replace('.', ','), out open);
                    Double.TryParse(values[2].Replace('.', ','), out high);
                    Double.TryParse(values[3].Replace('.', ','), out low);
                    Double.TryParse(values[4].Replace('.', ','), out close);
                    Double.TryParse(values[6].Replace('.', ','), out volume);

                    StockHistory stockHistoryItem = new StockHistory();
                    stockHistoryItem.Date = date;
                    stockHistoryItem.Open = open;
                    stockHistoryItem.High = high;
                    stockHistoryItem.Low = low;
                    stockHistoryItem.Close = close;
                    stockHistoryItem.Volume = volume;

                    stockHistoryList.Add(stockHistoryItem);
                }
            }

            return stockHistoryList;
        }

        public ActionResult Add()
        {
            ViewBag.Title = "Add new company";

            if (Session[sessionKey] == null || (string) Session[sessionKey] != sessionValue)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult Chart(int id)
        {
            Stock stock = db.Stocks.Find(id);
            ViewBag.Title = stock.Name + " (" + stock.Ticker + ")";
            return View();
        }
    }
}
