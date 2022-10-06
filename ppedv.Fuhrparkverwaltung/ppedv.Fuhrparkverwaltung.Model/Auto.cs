namespace ppedv.Fuhrparkverwaltung.Model
{
    public class Auto
    {
        public int Id { get; set; }
        public string Hersteller { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string? Farbe { get; set; }
        public int Leistung { get; set; }
        public DateTime Baujahr { get; set; }
        public virtual Garage? Garage { get; set; }
    }
}