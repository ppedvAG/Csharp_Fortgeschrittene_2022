namespace Calculator.Tests
{
    public class CalcTests
    {
        [Fact]
        public void Sum_2_and_3_results_5()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(2, 3);

            //Assert
            Assert.Equal(5, result);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(0, 0, 0)]
        [InlineData(-3, -4, -7)]
        [InlineData(-2, 5, 3)]
        public void Sum_all_ok(int a, int b, int exp)
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(a, b);

            //Assert
            Assert.Equal(exp, result);
        }


        [Fact]
        public void Sum_MAX_and_1_throws_Exception()
        {
            var calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }


        [Fact]
        public void IsWeekend()
        {
            var calc = new Calc();

            Assert.False(calc.IsWeekend(new DateTime(2022, 10, 03)));
            Assert.False(calc.IsWeekend(new DateTime(2022, 10, 04)));
            Assert.False(calc.IsWeekend(new DateTime(2022, 10, 05)));
            Assert.False(calc.IsWeekend(new DateTime(2022, 10, 06)));
            Assert.False(calc.IsWeekend(new DateTime(2022, 10, 07)));
            Assert.True(calc.IsWeekend(new DateTime(2022, 10, 08)));
            Assert.True(calc.IsWeekend(new DateTime(2022, 10, 09)));
        }
    }
}