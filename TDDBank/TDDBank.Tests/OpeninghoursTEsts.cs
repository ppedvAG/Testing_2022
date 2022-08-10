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
        public void OpeningHours_IsOpen(int y, int M, int d, int h, int m, bool result)
        {
            var dt = new DateTime(y, M, d, h, m, 0);
            var oh = new Openinghours();

            Assert.Equal(result, oh.IsOpen(dt));
        }

    }
}
