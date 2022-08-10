namespace Calculator.Tests_xUnit
{
    public class CalcTest_xUnit
    {
        [Fact]
        public void Sum_2_and_3_results_5()
        {
            var calc = new Calc();

            var result = calc.Sum(2, 3);

            Assert.Equal(5, result);
        }

        [Theory]
        [InlineData(2, 3, 5)]
        [InlineData(-2, -3, -5)]
        [InlineData(1, 1, 2)]
        [InlineData(1000, 1400, 2400)]
        [InlineData(0, 0, 0)]
        public void Sum(int a, int b, int exp)
        {
            var calc = new Calc();

            var result = calc.Sum(a, b);

            Assert.Equal(exp, result);
        }
    }
}