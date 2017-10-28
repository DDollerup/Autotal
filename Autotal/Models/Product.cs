using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autotal.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int BrandID { get; set; }
        public string Model { get; set; }
        public int BHP { get; set; }
        public int Year { get; set; }
        public int Km { get; set; }
        public double Price { get; set; }
        public int ColorID { get; set; }
        public bool Smoker { get; set; }
        public bool Inspected { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
    }
}