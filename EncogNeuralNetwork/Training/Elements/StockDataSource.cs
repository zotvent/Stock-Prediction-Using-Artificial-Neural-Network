using Encog.ML.Data.Versatile.Sources;
using EncogNeuralNetwork.Model;
using EncogNeuralNetwork.Model.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncogNeuralNetwork.Training.Elements
{
    public class StockDataSource : IVersatileDataSource
    {
        public StockDataSource(IEnumerable<IEnumerable<StockIndicator>> set)
        {
            DataSet = set;
        }

        public IEnumerable<IEnumerable<StockIndicator>> DataSet { get; set; }

        public int currentRow = 0;

        public void Close()
        {
            return;
        }

        public int ColumnIndex(string name)
        {
            return 0;
        }

        public string[] ReadLine()
        {
            if (currentRow + 1 >= DataSet.Count())
                return null;
            var row = DataSet.ElementAt(currentRow).Select(x => x.Value.ToString("G")).ToArray();
            currentRow++;
            return row;
        }

        public void Rewind()
        {
            currentRow = 0;
        }
    }
}
