namespace Efin.OptionsGo.Tests
{
    public class OrderTest
    {
        public class FromTest
        {
            [Fact]
            public void BasicLongFu()
            {
                //Arrange
                var text = "LF @950 x 1";
                //Act
                Order? o = Order.Fromtext(text);
                //Assert
                Assert.NotNull(o);
                Assert.Equal(expected: 950.0, actual: o.Price);
                Assert.Equal(expected: "S50X99", actual: o.Symbol);
                Assert.Equal(expected: OrderSide.Long, actual: o.Side);
                Assert.Equal(expected: 1, actual: o.Contracts);

            }

            [Fact]
            public void BasicShortFu()
            {
                //Arrange
                var text = "SF @950.5 x 1";
                //Act
                Order? o = Order.Fromtext(text);
                //Assert
                Assert.NotNull(o);
                Assert.Equal(expected: 950.5, actual: o.Price);
                Assert.Equal(expected: "S50X99", actual: o.Symbol);
                Assert.Equal(expected: OrderSide.Short, actual: o.Side);
                Assert.Equal(expected: 1, actual: o.Contracts);

            }

            [Fact]
            public void EmptyText_ReturnNull()
            {
                //Arrange
                var text = "";

                //Act
                Order? o = Order.Fromtext(text);

                //Assert
                Assert.Null(o);

            }

            public static IEnumerable<object[]> GetInvalidOders()
            {
                using StreamReader reader = new StreamReader("InvalidOrders.txt");
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Process the line
                    line = line.Trim().Replace("\"","").Replace("\\t","\t");
                    yield return new object[] { line };
                }
            }

            [Theory]
            [MemberData(nameof(GetInvalidOders))]
            public void InvalidText_ReturnNull(string text)
            {
                //Act
                Order? o = Order.Fromtext(text);

                //Assert
                Assert.Null(o);
            }

            [Fact]
            public void InvalidSyntax_ReturnNull()
            {
                //Arrange
                var text = "XX @500 x 1";

                //Act
                Order? o = Order.Fromtext(text);

                //Assert
                Assert.Null(o);

            }

            [Fact]
            public void MultipleSpaces()
            {
                //Arrange
                var text = "LF  @500  x  1";

                //Act
                Order? o = Order.Fromtext(text);

                //Assert
                Assert.NotNull(o);
                Assert.Equal(expected: 500, actual: o.Price);
                Assert.Equal(expected: "S50X99", actual: o.Symbol);
                Assert.Equal(expected: OrderSide.Long, actual: o.Side);
                Assert.Equal(expected: 1, actual: o.Contracts);

            }

            [Theory]
            [InlineData(950.01,950.0)]
            [InlineData(950.45,950.5)]
            [InlineData(950.55,950.6)]
            [InlineData(950.65,950.7)]
            public void ExtraPrecisions_RoundToOneDigi(double price,double priceAns)
            {
                //Arrange
                var text = $"LF @{price} x  1";

                //Act
                Order? o = Order.Fromtext(text);

                //Assert
                Assert.NotNull(o);
                Assert.Equal(expected: priceAns, actual: o.Price);
                Assert.Equal(expected: "S50X99", actual: o.Symbol);
                Assert.Equal(expected: OrderSide.Long, actual: o.Side);
                Assert.Equal(expected: 1, actual: o.Contracts);

            }
        }
        
    }
}