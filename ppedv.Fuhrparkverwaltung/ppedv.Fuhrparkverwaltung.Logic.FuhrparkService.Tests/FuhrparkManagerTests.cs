using System.Linq.Expressions;

namespace ppedv.Fuhrparkverwaltung.Logic.FuhrparkService.Tests
{
    public class FuhrparkManagerTests
    {
        [Theory]
        [InlineData(1949, 73)] // Bobby Farrell, niederländischer Tänzer (Boney M.)
        [InlineData(1954, 68)] // Helmut Zierl, deutscher Schauspieler
        [InlineData(1970, 52)]// Corinna May, deutsche Sängerin
        public void GetAge_06_10_2022(int birthYear, int ageToDay)
        {
            var today = new DateTime(2022, 10, 06);
            var bdate = new DateTime(birthYear, 10, 06);
            var service = new FuhrparkManager();

            var age = service.GetAge(bdate, today);

            Assert.Equal(age, ageToDay);
        }
    }
}