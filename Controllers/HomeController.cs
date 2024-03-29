﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly DbReadService _readService;

        public HomeController(DbReadService readService)
        {
            _readService = readService;
        }

        public ActionResult Index()
        {
            //ViewBag.ammoniakWerteTagAktuell = amonniak.AmmoniakAktuellListeTag;
            //ViewBag.aktuellerWertAmmoniak = amonniak.AktuellerWertAmmoniak;

            ViewBag.test = getAmmoniakFunction();

            return View();
        }

        public ActionResult Hauptseite()
        {
            return View();
        }

        public ActionResult GetAmmoniak()
        {
            return getAmmoniakFunction();
        }

        public ActionResult GetLuftfeuchtigkeit()
        {

            Dictionary<string, Dictionary<DateTime, string>> luftfeuchtigkeitWerte = new Dictionary<string, Dictionary<DateTime, string>>();

            //get Dictionary with all times and ammoniak values for a speciffic day
            // "ammoniak" can be replaced with "luftfeuchtigkeit" and "temperatur" for the other diagrams
            DateTime i = DateTime.Parse("2023-06-15 14:05:47");
            //Dictionary<DateTime, string> luftfeuchtigkeitTag = _readService.GetDataForDay(i, "luftfeuchtigkeit");
            Dictionary<DateTime, string> luftfeuchtigkeitTag = LuftfeuchtigkeitfunktionTag();

            //List<string> ammoniakValuesDay = new List<string>();
            //foreach (string ammoniakValueDay in ammoniakTag.Values)
            //{
            //    ammoniakValuesDay.Add(ammoniakValueDay);
            //}
            //create full string to replace "amonniak.AmmoniakAktuellListeTag" with
            //string ammoniakStringDay = "[" + string.Join(", ", ammoniakValuesDay) + "]";
            //ammoniakWerte.Add("ammoniakWerteTagAktuell", amonniak.AmmoniakAktuellListeTag);
            luftfeuchtigkeitWerte.Add("luftfeuchtigkeitWerteTagAktuell", luftfeuchtigkeitTag);
            //todo: average values
            luftfeuchtigkeitWerte.Add("luftfeuchtigkeitkWerteTagDurchschnitt", null);


            //same thing for week (dictionary contains all data from last 7 days)
            //Dictionary<DateTime, string> luftfeuchtigkeitWoche = _readService.GetDataForWeek(DateTime.Now, "luftfeuchtigkeit");


            Dictionary<DateTime, string> luftfeuchtigkeitWoche = LuftfeuchtigkeitfunktionWoche();


            //List<string> ammoniakValuesWeek = new List<string>();
            //foreach (string ammoniakValue in ammoniakWoche.Values)
            //{
            //    ammoniakValuesWeek.Add(ammoniakValue);
            //}
            //string ammoniakStringWeek = "[" + string.Join(", ", ammoniakValuesWeek) + "]";
            //ammoniakWerte.Add("ammoiakWerteWocheAktuell", amonniak.AmmoniakAktuellWoche);
            luftfeuchtigkeitWerte.Add("luftfeuchtigkeitWerteWocheAktuell", luftfeuchtigkeitWoche);
            //todo: average values
            luftfeuchtigkeitWerte.Add("luftfeuchtigkeitWerteWocheDurchschnitt", null);
            return Json(luftfeuchtigkeitWerte);
        }

        public ActionResult GetTemperatur()
        {
            Dictionary<string, Dictionary<DateTime, string>> temperaturWerte = new Dictionary<string, Dictionary<DateTime, string>>();

            //get Dictionary with all times and ammoniak values for a speciffic day
            // "ammoniak" can be replaced with "luftfeuchtigkeit" and "temperatur" for the other diagrams
            DateTime i = DateTime.Parse("2023-06-15 14:05:47");
            //Dictionary<DateTime, string> temperaturTag = _readService.GetDataForDay(i, "temperatur");
            Dictionary<DateTime, string> temperaturTag = TemperaturfunktionTag();

            //List<string> ammoniakValuesDay = new List<string>();
            //foreach (string ammoniakValueDay in ammoniakTag.Values)
            //{
            //    ammoniakValuesDay.Add(ammoniakValueDay);
            //}
            //create full string to replace "amonniak.AmmoniakAktuellListeTag" with
            //string ammoniakStringDay = "[" + string.Join(", ", ammoniakValuesDay) + "]";
            //ammoniakWerte.Add("ammoniakWerteTagAktuell", amonniak.AmmoniakAktuellListeTag);
            temperaturWerte.Add("temperaturWerteTagAktuell", temperaturTag);
            //todo: average values
            temperaturWerte.Add("temperaturWerteTagDurchschnitt", null);


            //same thing for week (dictionary contains all data from last 7 days)
            //Dictionary<DateTime, string> ammoniakWoche = _readService.GetDataForWeek(DateTime.Now, "ammoniak");


            Dictionary<DateTime, string> temperaturWoche = TemperaturfunktionWoche();


            //List<string> ammoniakValuesWeek = new List<string>();
            //foreach (string ammoniakValue in ammoniakWoche.Values)
            //{
            //    ammoniakValuesWeek.Add(ammoniakValue);
            //}
            //string ammoniakStringWeek = "[" + string.Join(", ", ammoniakValuesWeek) + "]";
            //ammoniakWerte.Add("ammoiakWerteWocheAktuell", amonniak.AmmoniakAktuellWoche);
            temperaturWerte.Add("temperaturWerteWocheAktuell", temperaturWoche);
            //todo: average values
            temperaturWerte.Add("temperaturWerteWocheDurchschnitt", null);
            return Json(temperaturWerte);
        }

        public JsonResult getAmmoniakFunction()
        {
            Dictionary<string, Dictionary<DateTime, string>> ammoniakWerte = new Dictionary<string, Dictionary<DateTime, string>>();

            //get Dictionary with all times and ammoniak values for a speciffic day
            // "ammoniak" can be replaced with "luftfeuchtigkeit" and "temperatur" for the other diagrams
            DateTime i = DateTime.Parse("2023-06-15 14:05:47");
            //Dictionary<DateTime, string> ammoniakTag = _readService.GetDataForDay(i, "ammoniak");
            Dictionary<DateTime, string> ammoniakTag = AmmoniakfunktionTag();

            //List<string> ammoniakValuesDay = new List<string>();
            //foreach (string ammoniakValueDay in ammoniakTag.Values)
            //{
            //    ammoniakValuesDay.Add(ammoniakValueDay);
            //}
            //create full string to replace "amonniak.AmmoniakAktuellListeTag" with
            //string ammoniakStringDay = "[" + string.Join(", ", ammoniakValuesDay) + "]";
            //ammoniakWerte.Add("ammoniakWerteTagAktuell", amonniak.AmmoniakAktuellListeTag);
            ammoniakWerte.Add("ammoniakWerteTagAktuell", ammoniakTag);
            //todo: average values
            ammoniakWerte.Add("ammoniakWerteTagDurchschnitt", null);


            //same thing for week (dictionary contains all data from last 7 days)
            //Dictionary<DateTime, string> ammoniakWoche = _readService.GetDataForWeek(DateTime.Now, "ammoniak");


            Dictionary<DateTime, string> ammoniakWoche = AmmoniakfunktionWoche();


            //List<string> ammoniakValuesWeek = new List<string>();
            //foreach (string ammoniakValue in ammoniakWoche.Values)
            //{
            //    ammoniakValuesWeek.Add(ammoniakValue);
            //}
            //string ammoniakStringWeek = "[" + string.Join(", ", ammoniakValuesWeek) + "]";
            //ammoniakWerte.Add("ammoiakWerteWocheAktuell", amonniak.AmmoniakAktuellWoche);
            ammoniakWerte.Add("ammoniakWerteWocheAktuell", ammoniakWoche);
            //todo: average values
            ammoniakWerte.Add("ammoniakWerteWocheDurchschnitt", null);
            return Json(ammoniakWerte);

            // Änderungen Hannes
        }

        public Dictionary<DateTime, string> LuftfeuchtigkeitfunktionTag()
        {
            Dictionary<DateTime, string> ammoniak = new Dictionary<DateTime, string>();


            ammoniak.Add(new DateTime(2023, 12, 22, 00, 00, 00), "42");
            ammoniak.Add(new DateTime(2023, 12, 22, 02, 58, 00), "40");
            ammoniak.Add(new DateTime(2023, 12, 22, 03, 58, 00), "41");
            ammoniak.Add(new DateTime(2023, 12, 22, 05, 58, 00), "43");
            ammoniak.Add(new DateTime(2023, 12, 22, 07, 58, 00), "42");
            ammoniak.Add(new DateTime(2023, 12, 22, 10, 58, 00), "45");
            ammoniak.Add(new DateTime(2023, 12, 22, 11, 58, 00), "46");
            ammoniak.Add(new DateTime(2023, 12, 22, 12, 58, 00), "50");
            ammoniak.Add(new DateTime(2023, 12, 22, 14, 58, 00), "52");
            ammoniak.Add(new DateTime(2023, 12, 22, 16, 58, 00), "56");
            ammoniak.Add(new DateTime(2023, 12, 22, 17, 58, 00), "62");
            ammoniak.Add(new DateTime(2023, 12, 22, 19, 58, 00), "54");
            ammoniak.Add(new DateTime(2023, 12, 22, 23, 58, 00), "61");


            return ammoniak;
        }

        public Dictionary<DateTime, string> LuftfeuchtigkeitfunktionWoche()
        {
            Dictionary<DateTime, string> ammoniak = new Dictionary<DateTime, string>();

            ammoniak.Add(new DateTime(2023, 12, 22, 00, 00, 00), "45");
            ammoniak.Add(new DateTime(2023, 12, 22, 10, 58, 00), "45");
            ammoniak.Add(new DateTime(2023, 12, 22, 15, 58, 00), "42");
            ammoniak.Add(new DateTime(2023, 12, 22, 20, 58, 00), "40");
            ammoniak.Add(new DateTime(2023, 12, 23, 10, 58, 00), "43");
            ammoniak.Add(new DateTime(2023, 12, 23, 11, 58, 00), "46");
            ammoniak.Add(new DateTime(2023, 12, 23, 15, 58, 00), "44");
            ammoniak.Add(new DateTime(2023, 12, 23, 20, 58, 00), "42");
            ammoniak.Add(new DateTime(2023, 12, 24, 10, 58, 00), "40");
            ammoniak.Add(new DateTime(2023, 12, 24, 11, 58, 00), "43");
            ammoniak.Add(new DateTime(2023, 12, 24, 15, 58, 00), "42");
            ammoniak.Add(new DateTime(2023, 12, 24, 20, 58, 00), "41");
            ammoniak.Add(new DateTime(2023, 12, 25, 10, 58, 00), "41");
            ammoniak.Add(new DateTime(2023, 12, 25, 20, 58, 00), "43");
            ammoniak.Add(new DateTime(2023, 12, 25, 21, 58, 00), "40");
            ammoniak.Add(new DateTime(2023, 12, 25, 22, 58, 00), "43");
            ammoniak.Add(new DateTime(2023, 12, 26, 05, 58, 00), "42");
            ammoniak.Add(new DateTime(2023, 12, 26, 10, 58, 00), "45");
            ammoniak.Add(new DateTime(2023, 12, 26, 11, 58, 00), "46");
            ammoniak.Add(new DateTime(2023, 12, 26, 15, 58, 00), "50");
            ammoniak.Add(new DateTime(2023, 12, 27, 10, 58, 00), "52");
            ammoniak.Add(new DateTime(2023, 12, 27, 20, 58, 00), "48");
            ammoniak.Add(new DateTime(2023, 12, 27, 21, 58, 00), "54");
            ammoniak.Add(new DateTime(2023, 12, 28, 10, 58, 00), "56");
            ammoniak.Add(new DateTime(2023, 12, 28, 15, 58, 00), "62");
            ammoniak.Add(new DateTime(2023, 12, 28, 23, 58, 00), "61");


            return ammoniak;
        }

        public Dictionary<DateTime, string> TemperaturfunktionTag()
        {
            Dictionary<DateTime, string> ammoniak = new Dictionary<DateTime, string>();

            ammoniak.Add(new DateTime(2023, 12, 22, 00, 00, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 22, 03, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 22, 05, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 22, 07, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 22, 10, 58, 00), "20");
            ammoniak.Add(new DateTime(2023, 12, 22, 11, 58, 00), "21");
            ammoniak.Add(new DateTime(2023, 12, 22, 12, 58, 00), "22");
            ammoniak.Add(new DateTime(2023, 12, 22, 14, 58, 00), "24");
            ammoniak.Add(new DateTime(2023, 12, 22, 16, 58, 00), "25");
            ammoniak.Add(new DateTime(2023, 12, 22, 17, 58, 00), "26");
            ammoniak.Add(new DateTime(2023, 12, 22, 19, 58, 00), "24");
            ammoniak.Add(new DateTime(2023, 12, 22, 22, 58, 00), "24");
            ammoniak.Add(new DateTime(2023, 12, 22, 23, 58, 00), "23");

            return ammoniak;
        }

        public Dictionary<DateTime, string> TemperaturfunktionWoche()
        {
            Dictionary<DateTime, string> ammoniak = new Dictionary<DateTime, string>();

            ammoniak.Add(new DateTime(2023, 12, 22, 00, 00, 00), "22");
            ammoniak.Add(new DateTime(2023, 12, 22, 10, 58, 00), "22");
            ammoniak.Add(new DateTime(2023, 12, 22, 15, 58, 00), "21");
            ammoniak.Add(new DateTime(2023, 12, 22, 20, 58, 00), "22");
            ammoniak.Add(new DateTime(2023, 12, 23, 10, 58, 00), "21");
            ammoniak.Add(new DateTime(2023, 12, 23, 11, 58, 00), "23");
            ammoniak.Add(new DateTime(2023, 12, 23, 15, 58, 00), "23");
            ammoniak.Add(new DateTime(2023, 12, 23, 20, 58, 00), "23");
            ammoniak.Add(new DateTime(2023, 12, 24, 10, 58, 00), "22");
            ammoniak.Add(new DateTime(2023, 12, 24, 11, 58, 00), "24");
            ammoniak.Add(new DateTime(2023, 12, 24, 15, 58, 00), "24");
            ammoniak.Add(new DateTime(2023, 12, 24, 20, 58, 00), "26");
            ammoniak.Add(new DateTime(2023, 12, 25, 10, 58, 00), "23");
            ammoniak.Add(new DateTime(2023, 12, 25, 20, 58, 00), "25");
            ammoniak.Add(new DateTime(2023, 12, 25, 21, 58, 00), "21");
            ammoniak.Add(new DateTime(2023, 12, 25, 22, 58, 00), "25");
            ammoniak.Add(new DateTime(2023, 12, 26, 05, 58, 00), "22");
            ammoniak.Add(new DateTime(2023, 12, 26, 10, 58, 00), "20");
            ammoniak.Add(new DateTime(2023, 12, 26, 15, 58, 00), "21");
            ammoniak.Add(new DateTime(2023, 12, 26, 16, 58, 00), "22");
            ammoniak.Add(new DateTime(2023, 12, 27, 10, 58, 00), "24");
            ammoniak.Add(new DateTime(2023, 12, 27, 20, 58, 00), "23");
            ammoniak.Add(new DateTime(2023, 12, 27, 22, 58, 00), "24");
            ammoniak.Add(new DateTime(2023, 12, 28, 10, 58, 00), "25");
            ammoniak.Add(new DateTime(2023, 12, 28, 15, 58, 00), "25");
            ammoniak.Add(new DateTime(2023, 12, 28, 23, 58, 00), "23");

            return ammoniak;
        }

        public Dictionary<DateTime, string> AmmoniakfunktionTag()
        {
            Dictionary<DateTime, string> ammoniak = new Dictionary<DateTime, string>();

            ammoniak.Add(new DateTime(2023, 12, 22, 00, 00, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 22, 03, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 22, 05, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 22, 07, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 22, 10, 58, 00), "20");
            ammoniak.Add(new DateTime(2023, 12, 22, 11, 58, 00), "20");
            ammoniak.Add(new DateTime(2023, 12, 22, 12, 58, 00), "21");
            ammoniak.Add(new DateTime(2023, 12, 22, 14, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 22, 15, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 22, 16, 58, 00), "17");
            ammoniak.Add(new DateTime(2023, 12, 22, 17, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 22, 22, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 22, 23, 58, 00), "18");

            return ammoniak;
        }

        public Dictionary<DateTime, string> AmmoniakfunktionWoche()
        {
            Dictionary<DateTime, string> ammoniak = new Dictionary<DateTime, string>();

            ammoniak.Add(new DateTime(2023, 12, 22, 00, 00, 00), "17");
            ammoniak.Add(new DateTime(2023, 12, 22, 10, 58, 00), "20");
            ammoniak.Add(new DateTime(2023, 12, 22, 20, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 22, 21, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 23, 10, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 23, 11, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 23, 15, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 23, 20, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 24, 10, 58, 00), "17");
            ammoniak.Add(new DateTime(2023, 12, 24, 11, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 24, 15, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 24, 20, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 25, 10, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 25, 20, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 25, 21, 58, 00), "17");
            ammoniak.Add(new DateTime(2023, 12, 25, 22, 58, 00), "20");
            ammoniak.Add(new DateTime(2023, 12, 26, 05, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 26, 10, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 26, 11, 58, 00), "21");
            ammoniak.Add(new DateTime(2023, 12, 26, 15, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 27, 10, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 27, 20, 58, 00), "18");
            ammoniak.Add(new DateTime(2023, 12, 27, 21, 58, 00), "19");
            ammoniak.Add(new DateTime(2023, 12, 28, 10, 58, 00), "20");
            ammoniak.Add(new DateTime(2023, 12, 28, 15, 58, 00), "21");
            ammoniak.Add(new DateTime(2023, 12, 28, 23, 58, 00), "18");


            return ammoniak;
        }
    }
}