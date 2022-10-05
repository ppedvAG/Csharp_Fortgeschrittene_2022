namespace Calculator
{
    public class Calc
    {
        public int Sum(int a, int b)
        {
            return checked(a + b);
        }

        public bool IsWeekend(DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Sunday ||
                   dt.DayOfWeek == DayOfWeek.Saturday;
        }

        //public bool IsWeekend()
        //{
        //    return DateTime.Now.DayOfWeek == DayOfWeek.Sunday ||
        //           DateTime.Now.DayOfWeek == DayOfWeek.Saturday;
        //}
    }
}