using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oFormsWeb.Repositories.Entities
{
    public class FormTableEntity : TableEntity
    {
        public FormTableEntity() { }
        public FormTableEntity(string clientId, string formId)
        {
            this.PartitionKey = clientId;
            this.RowKey = formId;
            this.ApiKey = "";
            this.Cors = "";
            this.Name = "";
            this.Desc = "";
            this.FormTemplate = "";
        }
        public string ApiKey { get; set; }
        public string Cors { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string FormTemplate { get; set; }
    }
}
