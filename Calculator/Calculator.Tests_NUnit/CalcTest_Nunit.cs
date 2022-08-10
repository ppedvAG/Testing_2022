namespace Calculator.Tests_NUnit
{
    public class CalcTest_Nunit
    {
        [Test]
        public void Sum_2_and_3_results_5()
        {
            var calc = new Calc();

            var result = calc.Sum(2, 3);

            Assert.AreEqual(5, result);
        }


        [Test]
        [TestCase(2, 3, 5)]
        [TestCase(-2, -3, -5)]
        [TestCase(1, 1, 2)]
        [TestCase(1000, 1400, 2400)]
        [TestCase(0, 0, 0)]
        public void Sum(int a, int b, int exp)
        {
            var calc = new Calc();

            var result = calc.Sum(a, b);

            Assert.AreEqual(exp, result);
        }

    }
}