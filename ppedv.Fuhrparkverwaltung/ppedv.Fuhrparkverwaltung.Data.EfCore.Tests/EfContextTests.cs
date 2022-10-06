using ppedv.Fuhrparkverwaltung.Model;

namespace ppedv.Fuhrparkverwaltung.Data.EfCore.Tests
{
    public class EfContextTests
    {
        [Fact]
        public void EfContext_can_create_DB()
        {
            var testConString = "Server=(localdb)\\mssqllocaldb;Database=Fuhrpark_CreateTest;Trusted_Connection=true";
            var con = new EfContext(testConString);
            con.Database.EnsureDeleted();

            var result = con.Database.EnsureCreated();

            Assert.True(result);
        }

        [Fact]
        public void EfContext_can_add_Auto()
        {
            var testAuto = new Auto() { Farbe = "gelb", Leistung = 17, Hersteller = "Baudi", Model = "M99" };

            using (var con = new EfContext())
            {
                con.Autos.Add(testAuto);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Autos.Find(testAuto.Id);
                Assert.Equal(testAuto.Farbe, loaded.Farbe);
            }
        }
    }
}