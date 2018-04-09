using Newtonsoft.Json;
using oFormsWeb.Models;
using oFormsWeb.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oFormsWeb.Repositories.Helpers
{
    public static class FormExtension
    {
        public static FormTableEntity ToFormEntity(this Form form)
        {
            if (form == null) return null;
            var result = new FormTableEntity(form.ClientId, form.Id);
            result.ApiKey = form.ApiKey;
            result.Cors = form.Cors;
            result.Name = form.Name;
            result.Desc = form.Desc;
            result.FormTemplate = JsonConvert.SerializeObject(form.FormTemplate);

            return result;
        }

        public static Form ToForm(this FormTableEntity formEntity)
        {
            if (formEntity == null) return null;
            var result = new Form();
            result.Id = formEntity.RowKey;
            result.ClientId = formEntity.PartitionKey;
            result.ApiKey = formEntity.ApiKey;
            result.Cors = formEntity.Cors;
            result.Name = formEntity.Name;
            result.Desc = formEntity.Desc;
            result.FormTemplate = JsonConvert.DeserializeObject<FormTemplate>(formEntity.FormTemplate);
            return result;
        }
    }
}
