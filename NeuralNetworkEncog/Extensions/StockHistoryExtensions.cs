using SqlObjectWrapper.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkEncog.Extensions
{
    public static class StockHistoryExtensions
    {
        public static double[] GetValuesArray(this StockHistory history)
        {
            var values = new double[6];
            values[0] = history.Date.Ticks / TimeSpan.TicksPerDay;
            values[1] = history.Open;
            values[2] = history.High;
            values[3] = history.Low;
            values[4] = history.Close;
            values[5] = history.Volume;

            return values;
        }
    }
}
