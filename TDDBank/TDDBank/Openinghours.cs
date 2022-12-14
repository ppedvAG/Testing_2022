namespace TDDBank
{
    public class Openinghours
    {
        public bool IsOpen(DateTime dt)
        {
            var start = new TimeSpan(10, 30, 0);
            var ende = new TimeSpan(19, 00, 0);
            var endeSa = new TimeSpan(14, 00, 0);


            //häßlich aber geht
            if (dt.DayOfWeek == DayOfWeek.Sunday) return false;
            else if (dt.DayOfWeek == DayOfWeek.Saturday && dt.TimeOfDay >= start && dt.TimeOfDay < endeSa)
                return true;
            else if (dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday && dt.TimeOfDay >= start && dt.TimeOfDay < ende)
                return true;

            return false;
        }

        public bool IsNowOpen()
        {
            return IsOpen(DateTime.Now);
        }

        public bool IsWeekend()
        {
            return DateTime.Now.DayOfWeek == DayOfWeek.Saturday ||
                   DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
        }

        public bool IsFileOk()
        {
            using (var sr = new StreamReader("b:\\xyz.txt"))
            {
                var lines = sr.ReadToEnd();
                return lines.Length > 0;
            }
        }
    }
}
