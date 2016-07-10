using System.Collections.Generic;

namespace SuperSimpleStocksApp
{
    public class ConcreteStockFactory : StockFactory
    {
        private Dictionary<Enums.StockType, Stock> stocks = new Dictionary<Enums.StockType, Stock>();

        public ConcreteStockFactory()
        {
            stocks.Add(Enums.StockType.Common, new CommonStock());
            stocks.Add(Enums.StockType.Preferred, new PreferredStock());
        }

        public override Stock GetStock(StockInitParams stockParams, IStockTrade stockTrade)
        {
            var stock = stocks[stockParams.Type];
            stock.SetStockProperties(stockParams.Symbol, stockParams.LastDividend, stockParams.FixedDividend,
                    stockParams.ParValue, stockTrade);
            return stock;
        }
    }
}
