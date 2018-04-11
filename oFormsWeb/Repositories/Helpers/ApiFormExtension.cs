using Newtonsoft.Json;
using oFormsWeb.Models;
using oFormsWeb.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oFormsWeb.Repositories.Helpers
{
    public static class ApiFormExtension
    {
        public static ApiFormTableEntity ToApiForm(this FormApiMap formApiMap)
        {
            if (formApiMap == null) return null;
            var result = new ApiFormTableEntity(formApiMap.ApiKey);
            result.FormTemplate = JsonConvert.SerializeObject(formApiMap.FormTemplate);
            result.FormId = formApiMap.FormId;
            result.ClientId = formApiMap.ClientId;
            result.NumberOfRequests = formApiMap.NumberOfRequests;
            return result;
        }

        public static FormApiMap ToApiMap(this ApiFormTableEntity apiFormEntity)
        {
            if (apiFormEntity == null) return null;
            var result = new FormApiMap();
            result.FormId = apiFormEntity.FormId;
            result.ClientId = apiFormEntity.ClientId;
            result.NumberOfRequests = apiFormEntity.NumberOfRequests;
            result.FormTemplate = JsonConvert.DeserializeObject<FormTemplate>(apiFormEntity.FormTemplate);

            return result;
        }
    }
}
