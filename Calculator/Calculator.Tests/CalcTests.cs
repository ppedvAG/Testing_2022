using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Calculator.Tests
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        public void Sum_2_and_3_results_5()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(2, 3);

            //Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Sum_n2_and_n5_results_n7()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(-2, -5);

            //Assert
            Assert.AreEqual(-7, result);
        }

        [TestMethod]
        [DataRow(2, 3, 5)]
        [DataRow(-2, -3, -5)]
        [DataRow(1, 1, 2)]
        [DataRow(1000, 1400, 2400)]
        [DataRow(0, 0, 0)]
        public void Sum(int a, int b, int exp)
        {
            var calc = new Calc();

            var result = calc.Sum(a, b);

            Assert.AreEqual(exp, result);
        }

        [TestMethod]
        public void Sum_MAX_and_1_throws_OverflowException()
        {
            var calc = new Calc();

            Assert.ThrowsException<OverflowException>(() => calc.Sum(int.MaxValue-1, 2));
            
        }

        [TestMethod]
        public void Sum_MIN_and_n1_throws_OverflowException()
        {
            var calc = new Calc();

            Assert.ThrowsException<OverflowException>(() => calc.Sum(int.MinValue, -1));
        }
    }
}