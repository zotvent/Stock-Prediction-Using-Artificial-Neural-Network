using Encog.ML.Data;
using Encog.ML.Data.Market.Loader;
using EncogNeuralNetwork.Training.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncogNeuralNetwork.Training
{
    public class EncogTrainingData
    {
        public IMLDataSet TrainingSet { get; set; }
        public IMLDataSet ValidationSet { get; set; }
        
    }
}
