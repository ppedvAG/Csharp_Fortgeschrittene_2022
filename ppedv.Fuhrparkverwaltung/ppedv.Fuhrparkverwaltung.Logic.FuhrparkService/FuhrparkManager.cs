using ppedv.Fuhrparkverwaltung.Model;
using ppedv.Fuhrparkverwaltung.Model.Contracts;

namespace ppedv.Fuhrparkverwaltung.Logic.FuhrparkService
{
    public class FuhrparkManager
    {
        public IRepository Repository { get; }

        public FuhrparkManager(IRepository repository)
        {
            Repository = repository;
        }

        public Garage GetGarageWithFastestCars()
        {
            return Repository.GetAll<Garage>()
                             .OrderByDescending(x => x.Autos.Sum(y => y.Leistung))
                             .FirstOrDefault();
        }

    }
}