﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpSubbieWebAPI.Models
{
    public class EmailConfig
    {
        public String FromName { get; set; }
        public String FromAddress { get; set; }
        public string ToAddress { get; set; }
        public String LocalDomain { get; set; }
        public String MailServerAddress { get; set; }
        public String MailServerPort { get; set; }
        public String UserId { get; set; }
        public String UserPassword { get; set; }
        public string DeveloperEmailAddress { get; set; }
        public string GoogleReCaptchaUrl { get; set; }
        public string GoogleReCaptchaSecret { get; set; }
    }
}
