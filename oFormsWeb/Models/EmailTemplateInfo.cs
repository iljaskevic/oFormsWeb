﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oFormsWeb.Models
{
    public class EmailTemplateInfo
    {
        [Required]
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string ReCaptchaPrivateKey { get; set; }

        public string MessageTemplate { get; set; }
        public string FieldTemplate { get; set; }
    }
}
