using Microsoft.QualityTools.Testing.Fakes;

namespace TDDBank.Tests
{
    public class OpeninghoursTEsts
    {
        [Theory]
        [InlineData(2022, 8, 08, 10, 30, true)]//mo
        [InlineData(2022, 8, 08, 10, 29, false)]//mo
        [InlineData(2022, 8, 08, 10, 31, true)] //mo
        [InlineData(2022, 8, 08, 18, 59, true)] //mo
        [InlineData(2022, 8, 08, 19, 00, false)] //mo
        [InlineData(2022, 8, 13, 13, 0, true)] //sa
        [InlineData(2022, 8, 13, 16, 0, false)] //sa
        [InlineData(2022, 8, 14, 20, 0, false)] //so
        //[InlineData(2022, 1, 1, 20, 0, false)] //so
        public void OpeningHours_IsOpen(int y, int M, int d, int h, int m, bool result)
        {
            var dt = new DateTime(y, M, d, h, m, 0);
            var oh = new Openinghours();

            Assert.Equal(result, oh.IsOpen(dt));
        }
        [Fact]
        public void IsWeekend()
        {
            var oh = new Openinghours();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 8, 8);//mo
                Assert.False(oh.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 8, 9);//di
                Assert.False(oh.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 8, 10);//mi
                Assert.False(oh.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 8, 11);//do
                Assert.False(oh.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 8, 12);//fr
                Assert.False(oh.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 8, 13);//sa
                Assert.True(oh.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2022, 8, 14);//so
                Assert.True(oh.IsWeekend());
            }
        }

        [Fact]
        public void IsFileOk()
        {
            var oh = new Openinghours();

            using (ShimsContext.Create())
            {
                System.IO.Fakes.ShimStreamReader.ConstructorString = (x, y) => { };
                System.IO.Fakes.ShimStreamReader.AllInstances.ReadToEnd = x => "";
                Assert.False(oh.IsFileOk());

                System.IO.Fakes.ShimStreamReader.AllInstances.ReadToEnd = x => "TEST TEXT";
                Assert.True(oh.IsFileOk());
            }
        }


    }
}
