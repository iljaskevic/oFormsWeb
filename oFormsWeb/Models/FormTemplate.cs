﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oFormsWeb.Models
{
    public class FormTemplate
    {
        public FormTemplate() {}
        public FormTemplate(MessageFormat msgFormat)
        {
            this.ToEmail = "";
            this.ToName = "";
            this.FromEmail = "";
            this.FromName = "";
            this.Subject = "";
            this.MessageTemplate = msgFormat.MessageTemplate;
            this.FieldTemplate = msgFormat.FieldTemplate;
        }
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        [MaxLength(5000)]
        public string MessageTemplate { get; set; }
        [MaxLength(5000)]
        public string FieldTemplate { get; set; }
    }
}
