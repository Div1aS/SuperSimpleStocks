namespace SuperSimpleStocksApp
{
    public class CommonStockFactory : StockFactory
    {
        public override Stock GetStock(StockInitParams stockParams, IStockTrade stockTrade)
        {
            return new CommonStock(stockParams.Symbol, stockParams.LastDividend, stockParams.FixedDividend, stockParams.ParValue, stockTrade);
        }
    }
}
