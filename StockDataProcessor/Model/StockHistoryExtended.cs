using SqlObjectWrapper.EntityModel;
using System.Collections.Generic;
using System.Linq;
using StockDataProcessor.Model;
using System;
using StockDataProcessor.Model.Indicators.IndexIndicators;
using static StockDataProcessor.Model.Indicators.IndexIndicators.StandardIndicator;
using StockDataProcessor.Model.Indicators;

namespace StockDataProcessor.Model
{
    public class StockHistoryExtended
    {
        public enum IndicatorType
        {
            Open,Close,High,Low,Volume,
            HigherClose,LowerClose,
            PositiveDirectionalMovement,NegativeDirectionalMovement,
            TrueRange,
            SimpleMovingAverage
        }

        public StockHistoryExtended(StockHistory obj)
        {
            BaseObject = obj;
        }

        public StockHistory BaseObject { get; }

        public List<BaseIndicator> Indicators { get; } = new List<BaseIndicator>();

    }
}
