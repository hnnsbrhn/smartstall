namespace smartstall.Models
{
    public class Datenpaket
    {
        public DateTime Timestamp { get; set; }
        public float Temperatur { get; set; }
        public float Luftdruck { get; set; }
        public float Luftfeuchtigkeit { get; set; }
        public float GasResistenz { get; set; }
        public float Ammoniak { get; set; }
    }
}