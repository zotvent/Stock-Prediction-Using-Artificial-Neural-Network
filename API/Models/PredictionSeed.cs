using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class PredictionSeed
    {
        public Day day1 { get; set; }
        public Day day2 { get; set; }
        public Day day3 { get; set; }
        public int id { get; set; }
    }
}