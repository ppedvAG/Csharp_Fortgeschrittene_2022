using ppedv.Fuhrparkverwaltung.Model;

namespace ppedv.Fuhrparkverwaltung.Logic.FuhrparkService
{
    public class FuhrparkManager
    {
        public int GetAgeOfCar(Auto auto)
        {
            if (auto == null)
                throw new ArgumentNullException("auto");

            return GetAge(auto.Baujahr);
        }

        public int GetAge(DateTime dt) => GetAge(dt, DateTime.Today);

        public int GetAge(DateTime dt, DateTime today)
        {
            // Calculate the age.
            var age = today.Year - dt.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (dt.Date > today.AddYears(-age)) age--;

            return age;
        }

    }
}