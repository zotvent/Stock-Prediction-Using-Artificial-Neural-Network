using SqlObjectWrapper.EntityModel;
using StockDataProcessor.Model.Indicators;
using StockDataProcessor.Model.Indicators.IndexIndicators;
using System;
using System.Collections.Generic;
using System.Linq;
using static StockDataProcessor.Model.Indicators.BaseIndicator;
using static StockDataProcessor.Model.StockHistoryExtended;

namespace StockDataProcessor.Model
{
    public partial class StockHistoryExtendedList : List<StockHistoryExtended>
    {
       
        public StockHistoryExtendedList() : base()
        {

        }

        public StockHistoryExtendedList(List<StockHistory> list) : this()
        {
            AddRange(list.Select(x => new StockHistoryExtended(x)));
        }

        public static readonly IndicatorType[] StandardIndicators = { IndicatorType.Open, IndicatorType.Close, IndicatorType.High, IndicatorType.Low, IndicatorType.Volume };
        

        
    }
}
