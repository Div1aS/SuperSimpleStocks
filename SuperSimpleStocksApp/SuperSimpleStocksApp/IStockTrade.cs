namespace SuperSimpleStocksApp
{
    public interface IStockTrade
    {
        decimal GetCalculatedPrice(string symbol);
        decimal GetGBCEAllShareIndex();
        void MakeTrade(TradeParams trade);
        decimal GetPrice(string symbol);
    }
}
