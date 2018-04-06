using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using oFormsWeb.Repositories.Entities;
using Microsoft.Extensions.Logging;
using oFormsWeb.Models;
using Microsoft.Extensions.Options;
using oFormsWeb.Repositories.Helpers;
using System.Threading.Tasks;

namespace oFormsWeb.Repositories
{
    public interface IFormRepository
    {
        void InsertOrUpdateForm(string clientId, Form form);
        void DeleteForm(string clientId, string formId);
        Task<Form> GetForm(string clientId, string formId);
        Task<IList<Form>> GetAllClientForms(string clientId);
    }

    public class FormRepository : BaseRepository, IFormRepository
    {
        ILogger<FormRepository> _logger;

        public FormRepository(ILogger<FormRepository> logger, IOptions<FormsConfiguration> _formsConfiguration) : base(_formsConfiguration)
        {
            _logger = logger;
        }

        public void InsertOrUpdateForm(string clientId, Form form)
        {
            _logger.LogDebug($"Upserting Form with ClientId: {clientId} and formId: {form.Id}");
            CloudTable apiFormTable = GetFormTable();
            FormTableEntity apiFormTableEntity = form.ToFormEntity();
            TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(apiFormTableEntity);
            apiFormTable.ExecuteAsync(insertOrReplaceOperation);
        }

        public void DeleteForm(string clientId, string formId)
        {
            _logger.LogDebug($"Deleting Form with ClientId: {clientId} and formId: {formId}");
            CloudTable apiFormTable = GetFormTable();
            TableOperation retrieveOperation = TableOperation.Retrieve<FormTableEntity>(clientId, formId);
            TableResult retrievedResult = apiFormTable.ExecuteAsync(retrieveOperation).Result;

            FormTableEntity deleteEntity = (FormTableEntity)retrievedResult.Result;

            // Create the Delete TableOperation
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                apiFormTable.ExecuteAsync(deleteOperation);
                _logger.LogDebug($"Deleted Form with ClientId: {clientId} and formId: {formId}");
            }
            else
            {
                _logger.LogDebug($"Nothing to delete - Form with ClientId: {clientId} and formId: {formId} does not exist");
            }
        }

        public async Task<Form> GetForm(string clientId, string formId)
        {
            var start = DateTime.Now;
            _logger.LogInformation("Started retrieval of Form: " + start.ToString());
            CloudTable apiFormTable = GetFormTable();
            TableOperation retrieveOperation = TableOperation.Retrieve<FormTableEntity>(clientId, formId);
            TableResult retrievedResult = await apiFormTable.ExecuteAsync(retrieveOperation);
            var end = DateTime.Now;
            _logger.LogInformation("Finished retrieval of Form: " + DateTime.Now.ToString() + " - (" + end.Subtract(start).TotalMilliseconds + "ms)");
            return ((FormTableEntity)retrievedResult.Result).ToForm();
        }

        public async Task<IList<Form>> GetAllClientForms(string clientId)
        {
            IList<Form> result = new List<Form>();
            _logger.LogDebug($"Getting all Forms for user ({clientId})");
            try
            {
                CloudTable formTable = GetFormTable();
                TableQuery<FormTableEntity> query = new TableQuery<FormTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, clientId));

                TableContinuationToken token = null;
                do
                {
                    TableQuerySegment<FormTableEntity> resultSegment = await formTable.ExecuteQuerySegmentedAsync(query, token);
                    token = resultSegment.ContinuationToken;

                    foreach (FormTableEntity entity in resultSegment.Results)
                    {
                        _logger.LogDebug($"ClientId:{entity.PartitionKey}, FormId:{entity.RowKey}, APIKey:{entity.ApiKey}");
                        result.Add(entity.ToForm());
                    }
                } while (token != null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while getting client forms: {ex}");
            }
            _logger.LogDebug($"Found {result.Count} Forms for user ({clientId})");

            return result;
        }
    }
}
