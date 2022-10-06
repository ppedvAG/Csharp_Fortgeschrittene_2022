namespace ppedv.Fuhrparkverwaltung.Model
{
    public class Garage
    {
        public int Id { get; set; }
        public string Ort { get; set; } = string.Empty;
        public ICollection<Auto> Autos { get; set; } = new HashSet<Auto>();
    }
}