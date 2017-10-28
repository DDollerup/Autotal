using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autotal.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string ImageURL { get; set; }
        public string Alt { get; set; }
        public int ProductID { get; set; }
        public int SubpageID { get; set; }
    }
}