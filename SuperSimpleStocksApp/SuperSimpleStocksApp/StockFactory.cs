namespace SuperSimpleStocksApp
{
    public abstract class StockFactory
    {
        public abstract Stock GetStock(StockInitParams stockParams, IStockTrade stockTrade);
    }
}
