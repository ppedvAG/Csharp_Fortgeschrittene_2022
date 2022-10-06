using Moq;
using ppedv.Fuhrparkverwaltung.Model;
using ppedv.Fuhrparkverwaltung.Model.Contracts;

namespace ppedv.Fuhrparkverwaltung.Logic.FuhrparkService.Tests
{
    public class FuhrparkManagerTests
    {
        [Fact]
        public void GetGarageWithFastestCars_no_garages()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Garage>()).Returns(() =>
            {
                return new List<Garage>();
            });
            var fpm = new FuhrparkManager(mock.Object);

            var result = fpm.GetGarageWithFastestCars();
            Assert.Null(result);

            mock.Verify(x => x.GetAll<Garage>(), Times.Exactly(1));
            mock.Verify(x => x.SaveAll(), Times.Never);
        }


        [Fact]
        public void GetGarageWithFastestCars_3_garages_number_2_wins_moq()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Garage>()).Returns(() =>
            {
                var g1 = new Garage() { Ort = "G1" };
                g1.Autos.Add(new Auto() { Leistung = 500 });

                var g2 = new Garage() { Ort = "G2" };
                g2.Autos.Add(new Auto() { Leistung = 300 });
                g2.Autos.Add(new Auto() { Leistung = 400 });

                var g3 = new Garage() { Ort = "G3" };
                g3.Autos.Add(new Auto() { Leistung = 200 });

                return new[] { g1, g2, g3 };
            });
            var fpm = new FuhrparkManager(mock.Object);

            var result = fpm.GetGarageWithFastestCars();

            Assert.Equal("G2", result.Ort);
        }

        [Fact]
        public void GetGarageWithFastestCars_3_garages_number_2_wins()
        {
            IRepository repo = new TestRepo();
            var fpm = new FuhrparkManager(repo);

            var result = fpm.GetGarageWithFastestCars();

            Assert.Equal("G2", result.Ort);
        }
    }

    class TestRepo : IRepository
    {
        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(int id) where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            if (typeof(T) == typeof(Garage))
            {
                var g1 = new Garage() { Ort = "G1" };
                g1.Autos.Add(new Auto() { Leistung = 500 });

                var g2 = new Garage() { Ort = "G2" };
                g2.Autos.Add(new Auto() { Leistung = 300 });
                g2.Autos.Add(new Auto() { Leistung = 400 });

                var g3 = new Garage() { Ort = "G3" };
                g3.Autos.Add(new Auto() { Leistung = 200 });

                return new[] { g1, g2, g3 }.Cast<T>();
            }

            throw new NotImplementedException();
        }

        public T GetById<T>(int id) where T : class
        {
            throw new NotImplementedException();
        }

        public void SaveAll()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}