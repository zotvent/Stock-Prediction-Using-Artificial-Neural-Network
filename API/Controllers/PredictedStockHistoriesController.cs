using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using SqlObjectWrapper.EntityModel;
using API.Models;
using EncogPredictor.NeuralNetwork;
using SqlObjectWrapper.Utils;

namespace API.Controllers
{
    public class PredictedStockHistoriesController : ApiController
    {
        private NeuronNetworkDBEntities db = new NeuronNetworkDBEntities();

        // GET: api/PredictedStockHistories
        public IQueryable<PredictedStockHistory> GetPredictedStockHistories()
        {
            return db.PredictedStockHistories;
        }

        // GET: api/PredictedStockHistories/5
        [ResponseType(typeof(PredictedStockHistory))]
        public IHttpActionResult GetPredictedStockHistory(int id)
        {
            PredictedStockHistory predictedStockHistory = db.PredictedStockHistories.Find(id);
            if (predictedStockHistory == null)
            {
                return NotFound();
            }

            return Ok(predictedStockHistory);
        }

        [HttpPost]
        [ResponseType(typeof(double))]
        [Route("api/PredictedStockHistories/MakePrediction")]
        public IHttpActionResult MakePrediction(PredictionSeed seed)
        {
            NNPredictor predictor = BlobConverter<NNPredictor>.Decode(db.Stocks.Find(seed.id).Data);
            List<StockHistory> history = new List<StockHistory>();
            StockHistory firstDay = new StockHistory
            {
                Close = seed.day1.Close,
                Volume = seed.day1.Volume
            };
            history.Add(firstDay);
            StockHistory secondDay = new StockHistory
            {
                Close = seed.day2.Close,
                Volume = seed.day2.Volume
            };
            history.Add(secondDay);
            StockHistory thirdDay = new StockHistory
            {
                Close = seed.day3.Close,
                Volume = seed.day3.Volume
            };
            history.Add(thirdDay);

            var output = predictor.Predict(history)[0][0];
            return Ok(output);
        }

        [HttpGet]
        [Route("api/PredictedStockHistories/for/{id}")]
        [ResponseType(typeof(double[][]))]
        public IHttpActionResult GetPredictedStockHistoryForStockWith(int id)
        {
            var predictedStockHistory = (
                from st in db.PredictedStockHistories
                where st.StockId == id select st
            ).ToList();
            double[][] response = new double[predictedStockHistory.Count][];
            for (int i = 0; i < predictedStockHistory.Count; i++)
            {
                response[i] = new double[2];
                response[i][0] = Convert.ToInt64((predictedStockHistory[i].Date - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                response[i][1] = predictedStockHistory[i].Close;
            }
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // POST: api/PredictedStockHistories
        [ResponseType(typeof(PredictedStockHistory))]
        public IHttpActionResult PostPredictedStockHistory(PredictedStockHistory predictedStockHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PredictedStockHistories.Add(predictedStockHistory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = predictedStockHistory.Id }, predictedStockHistory);
        }

        // DELETE: api/PredictedStockHistories/5
        [ResponseType(typeof(PredictedStockHistory))]
        public IHttpActionResult DeletePredictedStockHistory(int id)
        {
            PredictedStockHistory predictedStockHistory = db.PredictedStockHistories.Find(id);
            if (predictedStockHistory == null)
            {
                return NotFound();
            }

            db.PredictedStockHistories.Remove(predictedStockHistory);
            db.SaveChanges();

            return Ok(predictedStockHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PredictedStockHistoryExists(int id)
        {
            return db.PredictedStockHistories.Count(e => e.Id == id) > 0;
        }
    }
}