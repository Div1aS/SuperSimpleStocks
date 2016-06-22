using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
            _storedStockTrades = new List<StockTrade>();
        }

        // ReSharper disable once ConvertToAutoProperty
        public static StockTradesSingleton Instance => instance;

        private static List<StockTrade> _storedStockTrades;
        public const double RecordsInPast15Minutes = -15;

        public decimal GetCalculatedPrice(string symbol)
        {
            var tradeSum = 0M;
            var tradeQuantity = 0M;
            foreach (var stockTrade in _storedStockTrades.Where(stockTrade => stockTrade.TradePrice != 0
            && stockTrade.Quantity != 0
            && stockTrade.TimeStamp >= DateTime.Now.AddMinutes(RecordsInPast15Minutes)
            && stockTrade.Symbol == symbol))
            {
                tradeSum += stockTrade.TradePrice * stockTrade.Quantity;
                tradeQuantity += stockTrade.Quantity;
            }
            if (tradeSum == 0 || tradeQuantity == 0)
            {
                return 0;
            }
            return tradeSum / tradeQuantity;
        }

        private double NthRoot(double tradePrices, int n)
        {
            return Math.Pow(tradePrices, 1.0 / n);
        }

        // ReSharper disable once InconsistentNaming
        public double GetGBCEAllShareIndex()
        {
            double tradeMultiplication = 0;
            var count = 0;
            foreach (var stockTrade in _storedStockTrades.Where(stockTrade => stockTrade.TradePrice != 0
            && stockTrade.Quantity != 0))
            {
                if (tradeMultiplication == 0)
                {
                    tradeMultiplication = (double)stockTrade.TradePrice;
                }
                else
                {
                    if (stockTrade.TradePrice != 0)
                    {
                        tradeMultiplication *= decimal.ToDouble(stockTrade.TradePrice);
                    }
                }
                count += 1;
            }
            return NthRoot(tradeMultiplication, count);
        }

        public void MakeTrade(TradeParams trade)
        {
            _storedStockTrades.Add(new StockTrade
            {
                Quantity = trade.Quantity,
                TradePrice = trade.TradePrice,
                Type = trade.Type,
                Symbol = trade.Symbol,
                TimeStamp = DateTime.Now
            });
        }

        public decimal GetPrice(string symbol)
        {
            var prices =
                _storedStockTrades.OrderByDescending(s => s.TimeStamp)
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
