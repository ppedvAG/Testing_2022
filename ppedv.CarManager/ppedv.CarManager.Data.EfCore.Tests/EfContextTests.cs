using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ppedv.CarManager.Model;
using System.Reflection;

namespace ppedv.CarManager.Data.EfCore.Tests
{
    public class EfContextTests : IDisposable
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void Can_create_db()
        {
            var con = new EfContext("Server=(localdb)\\mssqllocaldb;Database=Cars_CreateTest;Trusted_Connection=true");
            con.Database.EnsureDeleted();

            con.Database.EnsureCreated().Should().BeTrue();

            con.Database.EnsureDeleted();
        }

        public EfContextTests()
        {
            //init
            using var con = new EfContext();
            con.Database.EnsureCreated();
        }


        public void Dispose()
        {
            //cleanup
            //using var con = new EfContext();
            //con.Database.EnsureDeleted();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Can_add_and_read_Car()
        {
            var car = new Car() { Manufacturer = $"Baudi_{Guid.NewGuid()}" };

            using (var con = new EfContext())
            {
                con.Cars.Add(car);
                con.SaveChanges().Should().Be(1);
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                loaded.Should().NotBeNull();
                loaded.Manufacturer.Should().Be(car.Manufacturer);
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Can_update_Car()
        {
            var car = new Car() { Manufacturer = $"Baudi_{Guid.NewGuid()}" };
            var newManu = $"Bolvo_{Guid.NewGuid()}";

            using (var con = new EfContext())
            {
                con.Cars.Add(car);
                con.SaveChanges().Should().Be(1);
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                loaded.Manufacturer = newManu;
                con.SaveChanges().Should().Be(1);
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                loaded.Manufacturer.Should().Be(newManu);
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Can_delete_Car()
        {
            var car = new Car() { Manufacturer = $"Baudi_{Guid.NewGuid()}" };

            using (var con = new EfContext())
            {
                con.Cars.Add(car);
                con.SaveChanges().Should().Be(1);
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                con.Remove(loaded);
                con.SaveChanges().Should().Be(1);

            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                loaded.Should().BeNull();
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Can_add_and_read_Car_with_AutoFixture_and_FluentAssertions()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id)));
            var car = fix.Create<Car>();

            using (var con = new EfContext())
            {
                con.Cars.Add(car);
                con.SaveChanges().Should().Be(2);
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                loaded.Should().NotBeNull();
                loaded.Should().BeEquivalentTo(car, cfg => cfg.IgnoringCyclicReferences());
            }
        }


        [Fact]
        [Trait("Category", "Integration")]
        public void Deleting_a_car_should_not_delete_the_garage()
        {
            var car = new Car() { Manufacturer = "A1" };
            var garage = new Garage() { Name = "G1" };
            car.Garage = garage;

            using (var con = new EfContext())
            {
                con.Add(car);
                con.SaveChanges().Should().Be(2);
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                con.Remove(loaded);
                con.SaveChanges().Should().Be(1);
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Garage>(garage.Id);
                loaded.Should().NotBeNull();
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Deleting_a_garage_should_not_delete_the_car()
        {
            var car = new Car() { Manufacturer = "A1" };
            var garage = new Garage() { Name = "G1" };
            car.Garage = garage;

            using (var con = new EfContext())
            {
                con.Add(car);
                con.SaveChanges().Should().Be(2);
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Garage>(garage.Id);
                con.Remove(loaded);
                con.SaveChanges().Should().Be(1);
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                loaded.Should().NotBeNull();
                loaded.Garage.Should().BeNull();
            }
        }

    }

    internal class PropertyNameOmitter : ISpecimenBuilder
    {
        private readonly IEnumerable<string> names;

        internal PropertyNameOmitter(params string[] names)
        {
            this.names = names;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;
            if (propInfo != null && names.Contains(propInfo.Name))
                return new OmitSpecimen();

            return new NoSpecimen();
        }
    }
}