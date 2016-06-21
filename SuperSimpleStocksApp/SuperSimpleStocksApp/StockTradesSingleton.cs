using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperSimpleStocksApp
{
    public sealed class StockTradesSingleton : IStockTrade
    {
        private static readonly StockTradesSingleton instance = new StockTradesSingleton();

        static StockTradesSingleton()
        {
        }

        private StockTradesSingleton()
        {
        }

        // ReSharper disable once ConvertToAutoProperty
        public static StockTradesSingleton Instance => instance;

        private static List<StockTrade> StoredStockTrades;
        public const double RecordsInPast15Minutes = -15;

        public decimal GetCalculatedPrice(string symbol)
        {
            var tradeSum = 0M;
            var tradeQuantity = 0M;
            foreach (var stockTrade in StoredStockTrades.Where(stockTrade => stockTrade.TradePrice != 0
            && stockTrade.Quantity != 0
            && stockTrade.TimeStamp >= DateTime.Now.AddMinutes(RecordsInPast15Minutes)
            && stockTrade.Symbol == symbol))
            {
                tradeSum += stockTrade.TradePrice + stockTrade.Quantity;
                tradeQuantity += stockTrade.Quantity;
            }
            if (tradeSum == 0 || tradeQuantity == 0)
            {
                return 0;
            }
            return tradeSum / tradeQuantity;
        }

        private decimal NthRoot(decimal tradePrices, int n)
        {
            return (decimal)Math.Pow(decimal.ToDouble(tradePrices), 1.0 / n);
        }

        // ReSharper disable once InconsistentNaming
        public decimal GetGBCEAllShareIndex()
        {
            var tradeMultiplication = 0M;
            var count = 0;
            foreach (var stockTrade in StoredStockTrades.Where(stockTrade => stockTrade.TradePrice != 0
            && stockTrade.Quantity != 0))
            {
                tradeMultiplication *= stockTrade.TradePrice;
                count += 1;
            }
            return NthRoot(tradeMultiplication, count);
        }

        public void MakeTrade(TradeParams trade)
        {
            StoredStockTrades.Add(new StockTrade
            {
                Quantity = trade.Quantity,
                TradePrice = trade.TradePrice,
                Type = trade.Type,
                TimeStamp = DateTime.Now
            });
        }

        public decimal GetPrice(string symbol)
        {
            var prices =
                StoredStockTrades.OrderByDescending(s => s.TimeStamp)
                    .Where(s => s.Symbol.Equals(symbol)).ToList();
            if ( !prices.Any())
            {
                return 0;
            }
            var price = prices.FirstOrDefault();
            return price?.TradePrice ?? 0;
        }
    }
}
