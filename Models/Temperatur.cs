namespace smartstall.Models
{
    public class Temperatur
    {
        public string TemperaturAktuellListeTag { get; set; }
        public string TemperaturAktuellWoche { get; set; }

        public Temperatur()
        {
            TemperaturAktuellListeTag = "[10, 8, 10, 9, 15, 10, 8, 10, 9, 10, 9, 15]";
            TemperaturAktuellWoche = "[10, 8, 10, 9, 15, 16, 9]";
        }
    }
}