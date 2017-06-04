using System;

namespace EncogPredictor.NeuralNetwork
{
    [Serializable]
    public class PredictorParameters
    {
        public enum StockIndicators
        {
            Open,
            Close,
            High,
            Low,
            Volume
        }

        public PredictorParameters()
        { }

        public StockIndicators[] InputColumns { get; set; }
        public StockIndicators[] OutputColumns { get; set; }

        public int LagWindowSize { get; set; }
        public int LeadWindowSize { get; set; }
    }
}
