using FluentAssertions;
using NUnit.Framework;
using NSubstitute;
using SuperSimpleStocksApp;

namespace StocksTests
{
    [TestFixture]
    public class CommonStockShould
    {
        private StockFactory _stockFactory;
        private IStockTrade _stockTrades;
        private Stock _commonStock;
        private const string Symbol = "TEA";
        private TradeParams _tradeParams;

        [SetUp]
        public void Setup()
        {
            _stockFactory = new ConcreteStockFactory();
            _stockTrades = Substitute.For<IStockTrade>();
            _commonStock = new CreateStock().Get(_stockFactory, new StockInitParams { FixedDividend = 0, LastDividend = 10, ParValue = 100, Symbol = Symbol, Type = Enums.StockType.Common }, _stockTrades);
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
            _commonStock.Symbol.Should().Be(Symbol);
            _commonStock.FixedDividend.HasValue.Should().BeTrue();
            _commonStock.FixedDividend.Value.Should().Be(0M);
            _commonStock.ParValue.Should().Be(100M);
            _commonStock.Type.Should().Be(Enums.StockType.Common);
        }

        [Test]
        public void SetPrice()
        {
            //Act
            _stockTrades.GetPrice(_tradeParams.Symbol).Returns(140m);
            //Assert
            _stockTrades.GetPrice(Symbol).Should().Be(140);
        }

        [Test]
        public void DividendYieldReturnResult()
        {
            //Act
            _stockTrades.GetPrice(_tradeParams.Symbol).Returns(_tradeParams.TradePrice);
            var dividendYield = _commonStock.GetCalculateDividendYield();
            //Assert
            dividendYield.Should().Be(0.1M);
        }

        [Test]
        public void CalculatePERatioReturnResult()
        {
            //Act
            _stockTrades.GetPrice(_tradeParams.Symbol).Returns(_tradeParams.TradePrice);
            var PERatio = _commonStock.GetCalculatePERatio();
            //Assert
            PERatio.Should().Be(10);
        }


    }
}
