using ppedv.Fuhrparkverwaltung.Model.Contracts;

namespace ppedv.Fuhrparkverwaltung.Data.EfCore
{
    public class EfRepository : IRepository
    {
        public EfRepository(string conString)
        {
            _context = new EfContext(conString);
        }

        private readonly EfContext _context;

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(int id) where T : class
        {
            var loaded = _context.Find<T>(id);
            if (loaded != null)
                _context.Remove(loaded);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : class
        {
            return _context.Find<T>(id);
        }

        public void SaveAll()
        {
            _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}
