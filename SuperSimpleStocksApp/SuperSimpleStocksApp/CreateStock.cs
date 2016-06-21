namespace SuperSimpleStocksApp
{
    public class CreateStock
    {
        public Stock Get(StockFactory factory, StockInitParams stockParams, IStockTrade stockTrade)
        {
            var stock = factory.GetStock(stockParams, stockTrade);
            return stock;
        }
    }
}
