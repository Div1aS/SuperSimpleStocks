namespace SuperSimpleStocksApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StockFactory stockFactory = new CommonStockFactory();
            var stockTrades = StockTradesSingleton.Instance;
            var commonStock = new CreateStock().Get(stockFactory, new StockInitParams { FixedDividend = 0, LastDividend = 0, ParValue = 100, Symbol = "TEA" }, stockTrades);
        }
    }
}
