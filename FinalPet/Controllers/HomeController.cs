using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FinalPet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FinalPet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public string City()
        {
            string url = string.Format("https://api.freegeoip.app/);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string response;
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
                CityResponse _city = JsonConvert.DeserializeObject<CityResponse>(response);
                return _city.city;
            }
            catch
            {
                return "";
            }
        }
        [Route("/")]
        public IActionResult Index(string name)
        {
            DateTime thisDay = DateTime.Today;
            string _City = City();
            if (String.IsNullOrEmpty(name))
            {
                name = _City;
            }
            string url = string.Format("http://api.openweathermap.org/data/", name);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string response;
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
                ViewBag.Name = "Погода в городе " + weatherResponse.Name;
                ViewBag.Date = "Сегодня " + thisDay.ToString("D");
                ViewBag.Temp = weatherResponse.Main.temp;
                ViewBag.Feels_like = "Ощущается как" + weatherResponse.Main.feels_like;
                ViewBag.Temp_max = "Максимальная температура " + weatherResponse.Main.temp_max;
                ViewBag.Temp_min = weatherResponse.Main.temp_min;
                ViewBag.Pressure = "Давление " + weatherResponse.Main.pressure + " мм рт.ст.";
                ViewBag.Speed = "Скорость " + weatherResponse.Wind.speed + " м/c";
            }
            catch
            {

            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
