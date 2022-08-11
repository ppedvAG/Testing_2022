using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ppedv.CarManager.Model;
using System.Reflection;

namespace ppedv.CarManager.Data.EfCore.Tests
{
    public class EfContextTests
    {
        [Fact]
        public void Can_create_db()
        {
            var con = new EfContext();
            con.Database.EnsureDeleted();

            //Assert.True(con.Database.EnsureCreated());
            con.Database.EnsureCreated().Should().BeTrue();
        }

        [Fact]
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