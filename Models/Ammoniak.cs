namespace smartstall.Models
{
    public class Ammoniak
    {
        public string AmmoniakAktuellListeTag { get; set; }
        public string AmmoniakDurschnittListeTag { get; set; }
        public string AmmoniakAktuellWoche { get; set; }
        public string AmmoniakDurchschnittWoche { get; set; }

        public Ammoniak()
        {
            AmmoniakAktuellListeTag = "[10, 8, 10, 9, 15, 10, 8, 10, 9, 10, 9, 15]";
            AmmoniakDurschnittListeTag = "[10, 8, 10, 9, 8, 10, 10, 8, 10, 9, 15, 16]";
            AmmoniakAktuellWoche = "[10, 8, 10, 9, 15, 16, 10]";
            AmmoniakDurchschnittWoche = "[10, 8, 10, 9, 15, 16, 9]";
        }
    }
}