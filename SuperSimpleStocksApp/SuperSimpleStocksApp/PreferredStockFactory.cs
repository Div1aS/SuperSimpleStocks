namespace SuperSimpleStocksApp
{
    public class PreferredStockFactory : StockFactory
    {
        public override Stock GetStock(StockInitParams stockParams, IStockTrade stockTrade)
        {
            return new PreferredStock(stockParams.Symbol, stockParams.LastDividend, stockParams.FixedDividend, stockParams.ParValue, stockTrade);
        }
    }
}
