using ppedv.CarManager.Model;

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
            var car = new Car() { Manufacturer = "Baudi" };

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
    }
}