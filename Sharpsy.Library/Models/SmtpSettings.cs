﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sharpsy.Library.Models
{
    public class SmtpSettings
    {
        public string From { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
