using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oFormsWeb.Repositories.Entities
{
    public class UserTableEntity : TableEntity
    {
        public readonly static string USER_PARTITION_NAME = "users";
        public UserTableEntity(string clientId)
        {
            this.PartitionKey = USER_PARTITION_NAME;
            this.RowKey = clientId;
            this.StripeData = "";
            this.PlanData = "";
            this.UserData = "";
            this.IsDeleted = false;
            this.CreatedDate = DateTime.UtcNow;
        }
        public string StripeData { get; set; }
        public string PlanData { get; set; }
        public string UserData { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
