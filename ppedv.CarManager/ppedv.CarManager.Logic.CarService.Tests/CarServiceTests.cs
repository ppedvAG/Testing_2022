using FluentAssertions;
using ppedv.CarManager.Model;

namespace ppedv.CarManager.Logic.CarService.Tests
{
    public class CarServiceTests
    {
        [Fact]
        public void SetNewColor_black_should_be_ok()
        {
            var carService = new CarService();
            var car = new Car();

            carService.SetNewColor(car, "black");

            car.Color.Should().Be("black");
        }

        [Fact]
        public void SetNewColor_pink_should_throw_ArgumentEx()
        {
            var carService = new CarService();
            var car = new Car();

            var act = () => carService.SetNewColor(car, "pink");

            act.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData(null)]
        public void SetNewColor_color_null_or_empty_or_whitespace_should_throw_ArgumentNullEx(string color)
        {
            var carService = new CarService();
            var car = new Car();

            var act = () => carService.SetNewColor(car, color);

            act.Should().Throw<ArgumentNullException>().WithMessage("*newcolor*");
        }

        [Fact]
        public void SetNewColor_car_null_should_throw_ArgumentNullEx()
        {
            var carService = new CarService();

            var act = () => carService.SetNewColor(null, "black");

            act.Should().Throw<ArgumentNullException>().WithMessage("*car*");
        }
    }
}