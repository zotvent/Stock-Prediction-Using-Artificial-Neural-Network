using System;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using SqlObjectWrapper.EntityModel;

namespace API.Controllers
{
    public class StockHistoriesController : ApiController
    {
        private NeuronNetworkDBEntities db = new NeuronNetworkDBEntities();

        // GET: api/StockHistories
        public IQueryable<StockHistory> GetStocksHistory()
        {
            return db.StocksHistory;
        }

        // GET: api/StockHistories/5
        [ResponseType(typeof(StockHistory))]
        public IHttpActionResult GetStockHistory(int id)
        {
            StockHistory stockHistory = db.StocksHistory.Find(id);
            if (stockHistory == null)
            {
                return NotFound();
            }

            return Ok(stockHistory);
        }

        [HttpGet]
        [Route("api/StockHistories/for/{id}")]
        [ResponseType(typeof(double[][]))]
        public IHttpActionResult GetStockHistoryForStockWith(int id)
        {
            var stockHistory = (from st in db.StocksHistory where st.StockId == id select st).ToList();
            double[][] response = new double[stockHistory.Count][];
            for (int i = 0; i < stockHistory.Count; i++)
            {
                response[i] = new double[2];
                response[i][0] = Convert.ToInt64((stockHistory[i].Date - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                response[i][1] = stockHistory[i].Close;
            }
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        
        // POST: api/StockHistories
        [ResponseType(typeof(StockHistory))]
        public IHttpActionResult PostStockHistory(StockHistory stockHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StocksHistory.Add(stockHistory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stockHistory.Id }, stockHistory);
        }

        // DELETE: api/StockHistories/5
        [ResponseType(typeof(StockHistory))]
        public IHttpActionResult DeleteStockHistory(int id)
        {
            StockHistory stockHistory = db.StocksHistory.Find(id);
            if (stockHistory == null)
            {
                return NotFound();
            }

            db.StocksHistory.Remove(stockHistory);
            db.SaveChanges();

            return Ok(stockHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockHistoryExists(int id)
        {
            return db.StocksHistory.Count(e => e.Id == id) > 0;
        }
    }
}