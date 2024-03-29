﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autotal.Models
{
    public class Subpage
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
    }
}