namespace Efin.OptionsGo.Models.Tests
{
    public class PortfolioTest
    {

        public class AddOrder
        {
            [Fact]
            public void AddOneOrder()
            {
                //Arrange
                var p = new Portfolio();
                Assert.Empty(p.Orders);

                //Act
                Order? o = p.AddOrder("LF @950 x 1");
                Order? x = p.Orders.FirstOrDefault();

                //Assert
                Assert.NotNull(o);
                Assert.Single(p.Orders);
                Assert.Equal("S50X99", x.Symbol);

            }           

        }
        public class ProfitLoss
        {
            [Fact]
            public void LongFu()
            {
                //Arrange
                var p = new Portfolio();
                p.AddOrder("LF @975 x 1");
                p.Index = 977.0;
   
                //Act
                var pl = p.ProfitLoss;

                //Assert
                Assert.Equal(2.0,pl);
            }

            [Fact]
            public void ShortFu()
            {
                //Arrange
                var p = new Portfolio();
                p.AddOrder("SF @975 x 1");
                p.Index = 977.0;
   
                //Act
                var pl = p.ProfitLoss;

                //Assert
                Assert.Equal(-2.0,pl);
            }

            [Fact]
            public void ShortFuAndTaskPL()
            {
                //Arrange
                var p = new Portfolio();
                p.AddOrder("SF @975 x 2");
                p.AddOrder("LF @950 x 2");
                p.Index = 950.0;
   
                //Act
                var pl = p.ProfitLoss;

                //Assert
                Assert.Equal(50.0,pl);
            }

        }
    }
}
