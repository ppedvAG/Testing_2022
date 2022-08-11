using AutoFixture;
using AutoFixture.Kernel;
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

            Assert.True(con.Database.EnsureCreated());
        }

        [Fact]
        public void Can_add_and_read_Car()
        {
            var car = new Car() { Manufacturer = $"Baudi_{Guid.NewGuid()}" };

            using (var con = new EfContext())
            {
                con.Cars.Add(car);
                Assert.Equal(1, con.SaveChanges());
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                Assert.NotNull(loaded);
                Assert.Equal(car.Manufacturer, loaded.Manufacturer);
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
                Assert.Equal(1, con.SaveChanges());
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                loaded.Manufacturer = newManu;
                Assert.Equal(1, con.SaveChanges());
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                Assert.Equal(newManu, loaded.Manufacturer);
            }
        }

        [Fact]
        public void Can_delete_Car()
        {
            var car = new Car() { Manufacturer = $"Baudi_{Guid.NewGuid()}" };

            using (var con = new EfContext())
            {
                con.Cars.Add(car);
                Assert.Equal(1, con.SaveChanges());
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                con.Remove(loaded);
                Assert.Equal(1, con.SaveChanges());
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                Assert.Null(loaded);
            }
        }

        [Fact]
        public void Can_add_and_read_Car_with_AutoFixture()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id)));
            var car = fix.Create<Car>();

            using (var con = new EfContext())
            {
                con.Cars.Add(car);
                Assert.Equal(2, con.SaveChanges());
            }

            using (var con = new EfContext())
            {
                var loaded = con.Find<Car>(car.Id);
                Assert.NotNull(loaded);
                Assert.Equal(car.Manufacturer, loaded.Manufacturer);
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