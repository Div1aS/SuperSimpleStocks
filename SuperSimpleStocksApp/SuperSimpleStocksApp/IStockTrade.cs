namespace SuperSimpleStocksApp
{
    public interface IStockTrade
    {
        decimal GetCalculatedPrice(string symbol);
        double GetGBCEAllShareIndex();
        void MakeTrade(TradeParams trade);
        decimal GetPrice(string symbol);
    }
}
