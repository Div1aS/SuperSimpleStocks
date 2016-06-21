namespace SuperSimpleStocksApp
{
    public class ConcreteStockFactory : StockFactory
    {
        public override Stock GetStock(StockInitParams stockParams, IStockTrade stockTrade)
        {
            if (stockParams.Type == Enums.StockType.Common)
            {
                return new CommonStock(stockParams.Symbol, stockParams.LastDividend, stockParams.FixedDividend,
                    stockParams.ParValue, stockTrade);
            }
            return new PreferredStock(stockParams.Symbol, stockParams.LastDividend, stockParams.FixedDividend,
                    stockParams.ParValue, stockTrade);
        }
    }
}
