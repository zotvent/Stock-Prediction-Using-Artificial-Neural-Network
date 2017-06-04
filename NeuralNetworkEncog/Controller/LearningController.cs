using Encog;
using Encog.ML;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.ML.Data.Versatile;
using Encog.ML.Data.Versatile.Columns;
using Encog.ML.Data.Versatile.Normalizers.Strategy;
using Encog.ML.Factory;
using Encog.ML.Model;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Training;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.Util.CSV;
using NeuralNetworkEncog.Elements;
using NeuralNetworkEncog.Extensions;
using SqlObjectWrapper.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkEncog.Controller
{
    public class LearningController
    {
        public const int WindowSize = 30;
        public const int TrainStart = WindowSize;
        public const int TrainEnd = 259;
        public const int EvaluateStart = 260;

        public const double MaxError = 0.01;





        public static NeuralNetworkInstance LearnNeuralNetwork(IEnumerable<StockHistory> learningSet, NetworkParameters parameters)
        {
            var source = new StockDataSource(learningSet);

            var inputColumns = parameters.InputColumns;
            var outputColumns = parameters.OutputColumns;

            var data = BuildDataSet(source, inputColumns, outputColumns);






            data.LeadWindowSize = parameters.LeadWindowSize;
            data.LagWindowSize = parameters.LagWindowSize;



            var model = new EncogModel(data);
            model.Report = new ConsoleStatusReportable();
            model.SelectMethod(data, MLMethodFactory.TypeFeedforward);

            data.Normalize();

            model.HoldBackValidation(0.3, false, 1001);
            model.SelectTrainingType(data);

            var bestMethod = (IMLRegression)model.Crossvalidate(10, false);

            model.HoldBackValidation(0.3, true, 1001);
            model.SelectTrainingType(data);
            bestMethod = (IMLRegression)model.Crossvalidate(10, true);

            var normalizer = GenerateNormalizer(source, inputColumns, outputColumns);

            source.Close();

            var neuralNetwork = new NeuralNetworkInstance()
            {
                Parameters = parameters,
                Normalizer = normalizer,
                Method = bestMethod
            };
            return neuralNetwork;
        }

        public static VersatileMLDataSet BuildDataSet(StockDataSource source, int[] inputColumns, int[] outputColumns)
        {
            var format = new CSVFormat(',', ',');
            var data = new VersatileMLDataSet(source);
            data.NormHelper.Format = format;
            //data.NormHelper.NormStrategy = new BasicNormalizationStrategy(-1, 1, -1, 1);

            var date = data.DefineSourceColumn("Date", 0, ColumnType.Ignore);
            var open = data.DefineSourceColumn("Open", 1, ColumnType.Continuous);
            var high = data.DefineSourceColumn("High", 2, ColumnType.Continuous);
            var low = data.DefineSourceColumn("Low", 3, ColumnType.Continuous);
            var close = data.DefineSourceColumn("Close", 4, ColumnType.Continuous);
            var volume = data.DefineSourceColumn("Volume", 5, ColumnType.Continuous);

            var columns = new List<ColumnDefinition>() { date, open, high, low, close, volume };

            data.Analyze();
            foreach (var i in inputColumns)
            {
                data.DefineInput(columns[i]);
            }
            foreach (var i in outputColumns)
            {
                data.DefineOutput(columns[i]);
            }

            return data;
        }

        public static DataNormalizer GenerateNormalizer(StockDataSource source, int[] inputColumns, int[] outputColumns)
        {
            var inputDataSet = source.RawData.Select(x =>
            {
                var row = new double[inputColumns.Count()];

                for (int i = 0; i < inputColumns.Count(); i++)
                {
                    row[i] = x[inputColumns[i]];
                }
                return row;
            }).ToArray();

            var outputDataSet = source.RawData.Select(x =>
            {
                var row = new double[outputColumns.Count()];

                for (int i = 0; i < outputColumns.Count(); i++)
                {
                    row[i] = x[outputColumns[i]];
                }
                return row;
            }).ToArray();

            var normalizer = new DataNormalizer(inputDataSet, outputDataSet);

            return normalizer;
        }
    }
}
