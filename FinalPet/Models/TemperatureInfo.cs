using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalPet.Models
{
    public class TemperatureInfo
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public float pressure { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
    }
}
