using smartstall.Models;
using smartstall.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace smartstall.Controllers
{
    public class ReceiveDataController : Controller
    {
        private readonly DbWriteService writeService;
        public ReceiveDataController(DbWriteService myService)
        {
            writeService = myService;
        }
        [HttpPost]
        public ActionResult ReceiveData([FromBody]Datenpaket data)
        {
            if (data != null)
            {                
                data.Timestamp = DateTime.Now;
                System.Console.WriteLine($"Received sensor data: {JsonConvert.SerializeObject(data)}");               
                
                writeService.InsertData(data);

                // Rückgabe einer erfolgreichen Antwort
                return Content("Data received successfully");
            }

            // Falls keine Daten empfangen wurden, eine Fehlerantwort zurückgeben
            return Content("No data received");
        }
    }
}