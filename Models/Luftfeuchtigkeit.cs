﻿
namespace smartstall.Models
{
    public class Luftfeuchtigkeit
    {
        public string LuftfeuchtigkeitAktuellListeTag { get; set; }
        public string LuftfeuchtigkeitAktuellWoche { get; set; }
        public string AktuellerWertLuftfeuchtigkeit { get; set; }

        public Luftfeuchtigkeit()
        {
            LuftfeuchtigkeitAktuellListeTag = "[{ \"x\": 0.844, \"y\": 12 }, { \"x\": 1.124, \"y\": 19.75 }, { \"x\": 6.78, \"y\": 1 }, { \"x\": 6.8, \"y\": 1 }, { \"x\": 6.9, \"y\": 3 }, { \"x\": 7.1, \"y\": 5 }, { \"x\": 7.2, \"y\": 6 }, { \"x\": 15.78, \"y\": 14 }]";
            LuftfeuchtigkeitAktuellWoche = "[{ \"x\": 1.124, \"y\": 24.78 }, { \"x\": 1.543, \"y\": 17.75 }, { \"x\": 2.453, \"y\": 15.75 }, { \"x\": 5.124, \"y\": 13.75 }, { \"x\": 6.78, \"y\": 1 }, { \"x\": 6.8, \"y\": 1 }, { \"x\": 6.9, \"y\": 3 }]";
            AktuellerWertLuftfeuchtigkeit = "15";
        }
    }
}