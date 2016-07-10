using System;
using System.Collections.Generic;

namespace SuperSimpleStocksApp
{
    public class ConcreteStockFactory : StockFactory
    {
        private readonly Lazy<Dictionary<Enums.StockType, Stock>> _stocks =
            new Lazy<Dictionary<Enums.StockType, Stock>>(() =>
            {
                Dictionary<Enums.StockType, Stock> stocks = new Dictionary<Enums.StockType, Stock>
                {
                    {Enums.StockType.Common, new CommonStock()},
                    {Enums.StockType.Preferred, new PreferredStock()}
                };
                return stocks;
            });

        public override Stock GetStock(StockInitParams stockParams, IStockTrade stockTrade)
        {
            var stock = _stocks.Value[stockParams.Type];
            stock.SetStockProperties(stockParams.Symbol, stockParams.LastDividend, stockParams.FixedDividend,
                    stockParams.ParValue, stockTrade);
            return stock;
        }
    }
}
