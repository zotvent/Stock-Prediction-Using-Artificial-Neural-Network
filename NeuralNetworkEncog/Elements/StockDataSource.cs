using Encog.ML.Data.Versatile.Sources;
using NeuralNetworkEncog.Extensions;
using SqlObjectWrapper.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkEncog.Elements
{
    public class StockDataSource : IVersatileDataSource
    {
        public StockDataSource(IEnumerable<StockHistory> set)
        {
            DataSet = set;

            RawData = new double[set.Count()][];
            for(var i=0; i< set.Count(); i++)
            {
                var row = set.ElementAt(i);

                RawData[i] = row.GetValuesArray();
            }
            
        }

        public IEnumerable<StockHistory> DataSet { get; set; }

        public int currentRow = 0;

        public void Close()
        {
            return;
        }

        public int ColumnIndex(string name)
        {
            return 0;
        }

        public double[][] RawData { get; private set; }

        public string[] ReadLine()
        {
            if (currentRow + 1 >= RawData.Count())
                return null;
            var element = RawData[currentRow];

            var row = element.Select(x => x.ToString("G")).ToArray();

            currentRow++;
            return row;
        }

        public void Rewind()
        {
            currentRow = 0;
        }
    }
}
