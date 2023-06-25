using smartstall.Models;
using smartstall.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace smartstall.Controllers
{
    public class ReceiveDataController : Controller
    {
        [HttpPost]
        public ActionResult ReceiveData([FromBody]Datenpaket data)
        {
            if (data != null)
            {
                // Datenverarbeitung
                // Zum Beispiel: Daten in einer Datenbank speichern
                // oder weitere Aktionen durchf�hren

                // Beispiel: Daten in der Konsole ausgeben                
                System.Console.WriteLine($"Received sensor data: {JsonConvert.SerializeObject(data)}");

                DbWriteService writeService = new DbWriteService();
                writeService.InsertData(data);



                // R�ckgabe einer erfolgreichen Antwort
                return Content("Data received successfully");
            }

            // Falls keine Daten empfangen wurden, eine Fehlerantwort zur�ckgeben
            return Content("No data received");
        }
    }
}