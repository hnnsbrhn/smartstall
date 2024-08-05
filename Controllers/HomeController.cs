using Microsoft.AspNetCore.Mvc;
using smartstall.Models;
using smartstall.Services;
using System.Timers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;

namespace smartstall.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbReadService _readService;
        static System.Timers.Timer timer;
        private bool istBelueftungAn = false;

        public HomeController(DbReadService readService)
        {
            _readService = readService;

            if(timer == null)
            {
                timer = new System.Timers.Timer(1000 * 60 * 3);
                timer.Elapsed += TimerElapsed;
                timer.AutoReset = true;
                timer.Start();
            }
        }

        public ActionResult Index()
        {       
            return View();
        }

        public ActionResult GetBelüftungUndBenachrichtigung()
        {
            Dictionary<string, bool> attributes = new Dictionary<string, bool>();
            attributes.Add("Belüftung", AttributesST.belüftung);
            attributes.Add("Benachrichtigung", AttributesST.benachrichtigung);
            return Json(attributes);
        }

        public ActionResult Checkpassword(string username, string password)
        {      
            bool isPasswordCorrect = (username == "smartstallUser" && password == "default");

            return Json(isPasswordCorrect);
        }

        public ActionResult SetAttributes(string attribute, bool flag)
        {
            if(attribute == "Belüftung")
            {
                AttributesST.belüftung = flag;
                return Json("erfolgreich");
            }
            else
            {
                AttributesST.benachrichtigung = flag;
                return Json("erfolgreich");
            }
        }

        public ActionResult Hauptseite()
        {
            return View();
        }

        public ActionResult GetAmmoniak()
        {
            return getAmmoniakFunction();
        }

        public ActionResult CheckUserData()
        {
            return Json("true");
        }

        public ActionResult GetLuftfeuchtigkeit()
        {

            Dictionary<string, Dictionary<DateTime, string>> luftfeuchtigkeitWerte = new Dictionary<string, Dictionary<DateTime, string>>();

            //get Dictionary with all times and ammoniak values for a speciffic day
            Dictionary<DateTime, string> luftfeuchtigkeitTag = _readService.GetDataForDay(DateTime.Now, "luftfeuchtigkeit");
            

           
            luftfeuchtigkeitWerte.Add("luftfeuchtigkeitWerteTagAktuell", luftfeuchtigkeitTag);
            luftfeuchtigkeitWerte.Add("luftfeuchtigkeitkWerteTagDurchschnitt", null);


            //same thing for week (dictionary contains all data from last 7 days)
            Dictionary<DateTime, string> luftfeuchtigkeitWoche = _readService.GetDataForWeek(DateTime.Now, "luftfeuchtigkeit");
            luftfeuchtigkeitWerte.Add("luftfeuchtigkeitWerteWocheAktuell", luftfeuchtigkeitWoche);
            luftfeuchtigkeitWerte.Add("luftfeuchtigkeitWerteWocheDurchschnitt", null);
            return Json(luftfeuchtigkeitWerte);
        }

        public ActionResult GetTemperatur()
        {
            Dictionary<string, Dictionary<DateTime, string>> temperaturWerte = new Dictionary<string, Dictionary<DateTime, string>>();

            //get Dictionary with all times and ammoniak values for a speciffic day
            Dictionary<DateTime, string> temperaturTag = _readService.GetDataForDay(DateTime.Now, "temperatur");
            temperaturWerte.Add("temperaturWerteTagAktuell", temperaturTag);
            //todo: average values
            temperaturWerte.Add("temperaturWerteTagDurchschnitt", null);


            //same thing for week (dictionary contains all data from last 7 days)
            Dictionary<DateTime, string> temperaturWoche = _readService.GetDataForWeek(DateTime.Now, "temperatur");
            temperaturWerte.Add("temperaturWerteWocheAktuell", temperaturWoche);
            temperaturWerte.Add("temperaturWerteWocheDurchschnitt", null);
            return Json(temperaturWerte);
        }

        public JsonResult getAmmoniakFunction()
        {
            Dictionary<string, Dictionary<DateTime, string>> ammoniakWerte = new Dictionary<string, Dictionary<DateTime, string>>();

            //get Dictionary with all times and ammoniak values for a speciffic day
            Dictionary<DateTime, string> ammoniakTag = _readService.GetDataForDay(DateTime.Now, "ammoniak");         
            ammoniakWerte.Add("ammoniakWerteTagAktuell", ammoniakTag);


            //same thing for week (dictionary contains all data from last 7 days)
            Dictionary<DateTime, string> ammoniakWoche = _readService.GetDataForWeek(DateTime.Now, "ammoniak");
            ammoniakWerte.Add("ammoniakWerteWocheAktuell", ammoniakWoche);
            ammoniakWerte.Add("ammoniakWerteWocheDurchschnitt", null);
            return Json(ammoniakWerte);

            // Änderungen Hannes
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            var ammoniak = GetAmmoniak() as JsonResult;
            var temperatur = GetTemperatur() as JsonResult;
            var luftfeuchtigkeit = GetLuftfeuchtigkeit() as JsonResult;

             var valuesAmmoniak = ammoniak.Value as Dictionary<string, Dictionary<DateTime, string>>;
             var valuesTemperatur = temperatur.Value as Dictionary<string, Dictionary<DateTime, string>>;
             var valuesLuftfeuchtigkeit = luftfeuchtigkeit.Value as Dictionary<string, Dictionary<DateTime, string>>;

             if(float.Parse(valuesAmmoniak.GetValueOrDefault("ammoniakWerteTagAktuell").Last().Value) > 20)
             {
                 SendMessagetoTelegram("Der Ammoniakgehalt liegt derzeit bei " + valuesAmmoniak.GetValueOrDefault("ammoniakWerteTagAktuell").Last().Value + " ppm und somit im schädlichen Bereich.");
             }

             if (float.Parse(valuesTemperatur.GetValueOrDefault("temperaturWerteTagAktuell").Last().Value) > 25)
             {
                 SendMessagetoTelegram("Die Temperatur liegt derzeit bei " + valuesTemperatur.GetValueOrDefault("temperaturWerteTagAktuell").Last().Value + " Grad Celsius und somit im schädlichen Bereich.");
             };

             if (float.Parse(valuesLuftfeuchtigkeit.GetValueOrDefault("luftfeuchtigkeitWerteTagAktuell").Last().Value) > 60)
             {
                 SendMessagetoTelegram("Die Luftfeuchtigkeit liegt derzeit bei " + valuesLuftfeuchtigkeit.GetValueOrDefault("luftfeuchtigkeitWerteTagAktuell").Last().Value + " Prozent und somit im schädlichen Bereich.");
             };

             if( !(float.Parse(valuesAmmoniak.GetValueOrDefault("ammoniakWerteTagAktuell").Last().Value) > 20) && !(float.Parse(valuesTemperatur.GetValueOrDefault("temperaturWerteTagAktuell").Last().Value) > 25) && !(float.Parse(valuesLuftfeuchtigkeit.GetValueOrDefault("luftfeuchtigkeitWerteTagAktuell").Last().Value) > 60))
             {
                BelüftungOff();
             } 
        }

        private async void SendMessagetoTelegram(string message)
        {
            var flags = GetBelüftungUndBenachrichtigung() as JsonResult;
            var values = flags.Value as Dictionary<string, bool>;
            bool belueftungsFlag = values.GetValueOrDefault("Belüftung");

            if (belueftungsFlag)
            {
                // Wenn das Flag true ist, schalte die Belüftung ein
                if (!istBelueftungAn)
                {
                    BelüftungOn();
                    istBelueftungAn = true;
                }
            }
            else
            {
                // Wenn das Flag false ist und die Belüftung an war, schalte sie aus
                if (istBelueftungAn)
                {
                    BelüftungOff();
                    istBelueftungAn = false;

                    string botToken = "6076806213:AAEXa-EkyOE9ImDtQO9BwmwXtMOMPNgCIjI";
                    string chatId = "-4018396712";
                    string nachricht = "Die Belüftung wird nun ausgeschaltet und muss manuell gesteuert werden.";

                    string apiUrl = $"https://api.telegram.org/bot{botToken}/sendMessage";

                    using (HttpClient client = new HttpClient())
                    {
                        var content = new StringContent($"chat_id={chatId}&text={nachricht}", Encoding.UTF8, "application/x-www-form-urlencoded");
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    }
                }
            }

            if (values.GetValueOrDefault("Benachrichtigung"))
            {
                string botToken = "6076806213:AAEXa-EkyOE9ImDtQO9BwmwXtMOMPNgCIjI";
                string chatId = "-4018396712";
                string nachricht = message;

                string apiUrl = $"https://api.telegram.org/bot{botToken}/sendMessage";

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent($"chat_id={chatId}&text={nachricht}", Encoding.UTF8, "application/x-www-form-urlencoded");
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                }
            }
        }

        static void BelüftungOn()
        {
            string url = "http://wlansteckdose.dnsuser.de/cm?cmnd=Power%20On";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }

        static void BelüftungOff()
        {
            string url = "http://wlansteckdose.dnsuser.de/cm?cmnd=Power%20Off";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }
    }
}