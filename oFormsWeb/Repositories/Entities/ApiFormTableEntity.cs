using Microsoft.WindowsAzure.Storage.Table;
using oFormsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oFormsWeb.Repositories.Entities
{
    public class ApiFormTableEntity : TableEntity
    {
        public ApiFormTableEntity() { }
        public ApiFormTableEntity(string apiKey)
        {
            this.PartitionKey = Consts.AZURE_API_PARTITION_NAME;
            this.RowKey = apiKey;
        }
        public string ClientId { get; set; }
        public string FormId { get; set; }
        public int NumberOfRequests { get; set; }
        public string FormTemplate { get; set; }
    }
}
