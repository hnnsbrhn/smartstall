using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using smartstall.Models;
using smartstall.Services;

namespace smartstall.Controllers
{
    public class HomeController : Controller
    {
        Ammoniak amonniak = new Ammoniak();
        Luftfeuchtigkeit luftfeuchtigkeit = new Luftfeuchtigkeit();
        Temperatur temperatur = new Temperatur();
        private readonly DbReadService _readService;

        public HomeController(DbReadService readService)
        {
            _readService = readService;
        }

        public ActionResult Index()
        {
            ViewBag.ammoniakWerteTagAktuell = amonniak.AmmoniakAktuellListeTag;
            ViewBag.aktuellerWertAmmoniak = amonniak.AktuellerWertAmmoniak;

            ViewBag.luftfeuchtigkeitWerteTagAktuell = luftfeuchtigkeit.LuftfeuchtigkeitAktuellListeTag;
            ViewBag.aktuellerWertLuftfeuchtigkeit = luftfeuchtigkeit.AktuellerWertLuftfeuchtigkeit;

            ViewBag.temperaturWerteTagAktuell = temperatur.TemperaturAktuellListeTag;
            ViewBag.aktuellerWertTemperatur = temperatur.AktuellerWertTemperatur;

            return View();
        }

        public ActionResult GetAmmoniak()
        {
            //Dictionary<string, string> ammoniakWerte = new Dictionary<string, string>();                       

            ////get Dictionary with all times and ammoniak values for a speciffic day
            //// "ammoniak" can be replaced with "luftfeuchtigkeit" and "temperatur" for the other diagrams
            //Dictionary<DateTime, string> ammoniakTag = _readService.GetDataForDay(DateTime.Now, "ammoniak");
            //List<string> ammoniakValuesDay = new List<string>();
            //foreach (string ammoniakValueDay in ammoniakTag.Values)
            //{
            //    ammoniakValuesDay.Add(ammoniakValueDay);
            //}
            ////create full string to replace "amonniak.AmmoniakAktuellListeTag" with
            //string ammoniakStringDay = "[" + string.Join(", ", ammoniakValuesDay) + "]";
            ////ammoniakWerte.Add("ammoniakWerteTagAktuell", amonniak.AmmoniakAktuellListeTag);
            //ammoniakWerte.Add("ammoniakWerteTagAktuell", ammoniakStringDay);                        
            ////todo: average values
            //ammoniakWerte.Add("ammoniakWerteTagDurchschnitt", amonniak.AmmoniakDurschnittListeTag);


            ////same thing for week (dictionary contains all data from last 7 days)
            //Dictionary<DateTime, string> ammoniakWoche = _readService.GetDataForWeek(DateTime.Now, "ammoniak");            
            //List<string> ammoniakValuesWeek = new List<string>();
            //foreach (string ammoniakValue in ammoniakWoche.Values)
            //{
            //    ammoniakValuesWeek.Add(ammoniakValue);
            //}            
            //string ammoniakStringWeek = "[" + string.Join(", ", ammoniakValuesWeek) + "]";
            ////ammoniakWerte.Add("ammoiakWerteWocheAktuell", amonniak.AmmoniakAktuellWoche);
            //ammoniakWerte.Add("ammoniakWerteWocheAktuell", ammoniakStringWeek);
            ////todo: average values
            //ammoniakWerte.Add("ammoniakWerteWocheDurchschnitt", amonniak.AmmoniakDurchschnittWoche);
            //return Json(ammoniakWerte);

            // Änderungen Hannes

            Dictionary<string, string> ammoniakData = new Dictionary<string, string>();
            ammoniakData.Add("AmmoniakAktuellListeTag", amonniak.AmmoniakAktuellListeTag);
            ammoniakData.Add("AmmoniakAktuellListeWoche", amonniak.AmmoniakAktuellListeWoche);
            ammoniakData.Add("aktuellerWertAmmnoniak", amonniak.AktuellerWertAmmoniak);

            return Json(ammoniakData);
        }

        public ActionResult GetLuftfeuchtigkeit()
        {
            Dictionary<string, string> luftfeuchtigkeitWerte = new Dictionary<string, string>();
            luftfeuchtigkeitWerte.Add("luftfeuchtigkeitWerteTagAktuell", luftfeuchtigkeit.LuftfeuchtigkeitAktuellListeTag);
            luftfeuchtigkeitWerte.Add("luftfeuchtigkeitWerteWocheAktuell", luftfeuchtigkeit.LuftfeuchtigkeitAktuellWoche);
            luftfeuchtigkeitWerte.Add("aktuellerWertLuftfeuchtigkeit", luftfeuchtigkeit.AktuellerWertLuftfeuchtigkeit);

            return Json(luftfeuchtigkeitWerte);
        }

        public ActionResult GetTemperatur()
        {
            Dictionary<string, string> temperaturWerte = new Dictionary<string, string>();
            temperaturWerte.Add("temperaturWerteTagAktuell", temperatur.TemperaturAktuellListeTag);
            temperaturWerte.Add("temperaturWerteWocheAktuell", temperatur.TemperaturAktuellWoche);
            temperaturWerte.Add("aktuellerWertTemperatur", temperatur.AktuellerWertTemperatur);

            return Json(temperaturWerte);
        }
    }
}