using FluentAssertions;
using Moq;
using ppedv.CarManager.Model;
using ppedv.CarManager.Model.Contracts;

namespace ppedv.CarManager.Logic.CarService.Tests
{
    public class CarServiceTests
    {

        [Fact]
        public void GetGarageWithLeastCars_empty_repo_should_return_null()
        {
            var mock = new Mock<IRepository>();
            var carService = new CarService(mock.Object);

            var result = carService.GetGarageWithLeastCars();

            result.Should().BeNull();
        }


        [Fact]
        public void GetGarageWithLeastCars_repo_with_3_garages_should_return_the_number_two__TestRepo()
        {
            var carService = new CarService(new TestRepo());

            var result = carService.GetGarageWithLeastCars();

            result.Name.Should().Be("G2");
        }

        [Fact]
        public void GetGarageWithLeastCars_repo_with_3_garages_should_return_the_number_two__Moq()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Garage>()).Returns(() =>
            {
                var g1 = new Garage() { Name = "G1" };
                g1.Cars.Add(new Car());
                g1.Cars.Add(new Car());

                var g2 = new Garage() { Name = "G2" };
                g2.Cars.Add(new Car());

                var g3 = new Garage() { Name = "G3" };
                g3.Cars.Add(new Car());
                g3.Cars.Add(new Car());
                g3.Cars.Add(new Car());

                return new[] { g1, g2, g3 };
            });
            var carService = new CarService(mock.Object);

            var result = carService.GetGarageWithLeastCars();

            result.Name.Should().Be("G2");
        }

        [Fact]
        public void GetGarageWithLeastCars_repo_with_2_garages_with_same_amount_of_card_should_return_the_one_with_most_doors()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Garage>()).Returns(() =>
            {
                var g1 = new Garage() { Name = "G1" };
                g1.Cars.Add(new Car() { Doors = 5 });
                g1.Cars.Add(new Car() { Doors = 2 });

                var g2 = new Garage() { Name = "G2" };
                g2.Cars.Add(new Car() { Doors = 4 });
                g2.Cars.Add(new Car() { Doors = 4 });

                return new[] { g1, g2 };
            });
            var carService = new CarService(mock.Object);

            var result = carService.GetGarageWithLeastCars();

            result.Name.Should().Be("G2");
        }

        [Fact]
        public void GetRandomGarage_returns_G7()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetById<Garage>(It.IsAny<int>()))
                .Returns(new Garage() { Name = "G7" });

            var carService = new CarService(mock.Object);

            var result = carService.GetRandomGarage();

            result.Name.Should().Be("G7");
        }





        [Fact]
        public void SetNewColor_black_should_be_ok()
        {
            var carService = new CarService(null);
            var car = new Car();

            carService.SetNewColor(car, "black");

            car.Color.Should().Be("black");
        }

        [Fact]
        public void SetNewColor_pink_should_throw_ArgumentEx()
        {
            var carService = new CarService(null);
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
            var carService = new CarService(null);
            var car = new Car();

            var act = () => carService.SetNewColor(car, color);

            act.Should().Throw<ArgumentNullException>().WithMessage("*newcolor*");
        }

        [Fact]
        public void SetNewColor_car_null_should_throw_ArgumentNullEx()
        {
            var carService = new CarService(null);

            var act = () => carService.SetNewColor(null, "black");

            act.Should().Throw<ArgumentNullException>().WithMessage("*car*");
        }
    }


    public class TestRepo : IRepository
    {
        public void Add<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            if (typeof(T) == typeof(Garage))
            {
                var g1 = new Garage() { Name = "G1" };
                g1.Cars.Add(new Car());
                g1.Cars.Add(new Car());

                var g2 = new Garage() { Name = "G2" };
                g2.Cars.Add(new Car());

                var g3 = new Garage() { Name = "G3" };
                g3.Cars.Add(new Car());
                g3.Cars.Add(new Car());
                g3.Cars.Add(new Car());

                return new[] { g1, g2, g3 }.Cast<T>();
            }

            throw new NotImplementedException();
        }

        public T GetById<T>(int id) where T : Entity
        {
            throw new NotImplementedException();
        }

        public int SaveAll()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}