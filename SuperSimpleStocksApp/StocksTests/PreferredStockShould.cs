using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SuperSimpleStocksApp;

namespace StocksTests
{
    [TestFixture]
    public class PreferredStockShould
    {
        private StockFactory _stockFactory;
        private IStockTrade _stockTrades;
        private Stock _preferedStock;
        private const string Symbol = "GIN";
        private TradeParams _tradeParams;

        [SetUp]
        public void Setup()
        {
            _stockFactory = new ConcreteStockFactory();
            _stockTrades = Substitute.For<IStockTrade>();
            _preferedStock = new CreateStock().Get(_stockFactory, new StockInitParams { FixedDividend = 2, LastDividend = 10, ParValue = 100, Symbol = Symbol, Type = Enums.StockType.Preferred }, _stockTrades);
            _tradeParams = new TradeParams
            {
                Quantity = 100,
                Symbol = Symbol,
                TradePrice = 100,
                Type = Enums.TradeType.Sell
            };
        }

        [Test]
        public void InitializeStockCorrectly()
        {
            //Assert
            _preferedStock.Symbol.Should().Be(Symbol);
            _preferedStock.FixedDividend.HasValue.Should().BeTrue();
            _preferedStock.FixedDividend.Value.Should().Be(2M);
            _preferedStock.ParValue.Should().Be(100M);
            _preferedStock.Type.Should().Be(Enums.StockType.Preferred);
        }

        [Test]
        public void SetPrice()
        {
            //Act
            _stockTrades.GetPrice(_tradeParams.Symbol).Returns(_tradeParams.TradePrice);
            //Assert
            _stockTrades.GetPrice(Symbol).Should().Be(100);
        }

        [Test]
        public void DividendYieldReturnResult()
        {
            //Act
            _stockTrades.GetPrice(_tradeParams.Symbol).Returns(_tradeParams.TradePrice);
            var dividendYield = _preferedStock.GetCalculateDividendYield();
            //Assert
            dividendYield.Should().Be(2M);
        }

        [Test]
        public void CalculatePERatioReturnResult()
        {
            //Act
            _stockTrades.GetPrice(_tradeParams.Symbol).Returns(_tradeParams.TradePrice);
            var PERatio = _preferedStock.GetCalculatePERatio();
            //Assert
            PERatio.Should().Be(10);
        }
    }
}
