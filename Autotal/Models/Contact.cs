using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autotal.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string OpenHours { get; set; }
        public string GoogleMapsEmbed { get; set; }
        public double InterestRate { get; set; }
    }
}