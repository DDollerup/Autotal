﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autotal.Models
{
    public class CMSUser
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}