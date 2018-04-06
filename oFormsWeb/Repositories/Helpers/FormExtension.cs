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
            result.FormTemplate = JsonConvert.SerializeObject(form);

            return result;
        }

        public static Form ToForm(this FormTableEntity formEntity)
        {
            if (formEntity == null) return null;
            var result = new Form();
            result.Id = formEntity.RowKey;
            result.ClientId = formEntity.PartitionKey;
            result.ApiKey = formEntity.ApiKey;
            var form = JsonConvert.DeserializeObject<Form>(formEntity.FormTemplate);
            result.Name = form.Name;
            result.Desc = form.Desc;
            return result;
        }
    }
}
