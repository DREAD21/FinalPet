using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalPet.Models
{
    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }
        public SpeedInfo Wind { get; set; }
        public string Name { get; set; }
    }
}
