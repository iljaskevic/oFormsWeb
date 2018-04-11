using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Table;
using oFormsWeb.Models;
using oFormsWeb.Repositories.Entities;
using oFormsWeb.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oFormsWeb.Repositories
{
    public interface IApiFormRepository
    {
        void InsertUpdateFormApiMap(string clientId, FormApiMap formApiMap);
        void DeleteFormApiMap(string apiKey);
        Task<FormApiMap> GetFormApiMap(string apiKey);
    }

    public class ApiFormRepository : BaseRepository, IApiFormRepository
    {
        ILogger<ApiFormRepository> _logger;

        public ApiFormRepository(ILogger<ApiFormRepository> logger, IOptions<FormsConfiguration> _formsConfiguration) : base(_formsConfiguration)
        {
            _logger = logger;
        }

        public void InsertUpdateFormApiMap(string clientId, FormApiMap formApiMap)
        {

            CloudTable apiFormTable = GetAPIKeyTable();
            ApiFormTableEntity apiFormTableEntity = formApiMap.ToApiForm();
            TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(apiFormTableEntity);
            apiFormTable.ExecuteAsync(insertOrReplaceOperation);
        }

        public void DeleteFormApiMap(string apiKey)
        {
            _logger.LogDebug($"Deleting FormApiMap with API key: {apiKey}");
            CloudTable apiFormTable = GetAPIKeyTable();
            TableOperation retrieveOperation = TableOperation.Retrieve<ApiFormTableEntity>(Consts.AZURE_API_KEY_TABLE_NAME, apiKey);
            TableResult retrievedResult = apiFormTable.ExecuteAsync(retrieveOperation).Result;

            ApiFormTableEntity deleteEntity = (ApiFormTableEntity)retrievedResult.Result;

            // Create the Delete TableOperation
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                apiFormTable.ExecuteAsync(deleteOperation);
                _logger.LogDebug($"Deleted FormApiMap with API key: {apiKey}");
            }
            else
            {
                _logger.LogDebug($"Nothing to delete - FormApiMap with API key: {apiKey} does not exist");
            }
        }

        public async Task<FormApiMap> GetFormApiMap(string apiKey)
        {
            var start = DateTime.Now;
            _logger.LogInformation("Started retrieval of FormApiMap: " + start.ToString());
            CloudTable apiFormTable = GetAPIKeyTable();
            TableOperation retrieveOperation = TableOperation.Retrieve<ApiFormTableEntity>("form-api-keys", apiKey);
            TableResult retrievedResult = await apiFormTable.ExecuteAsync(retrieveOperation);
            var end = DateTime.Now;
            _logger.LogInformation("Finished retrieval of FormApiMap: " + DateTime.Now.ToString() + " - (" + end.Subtract(start).TotalMilliseconds + "ms)");
            return ((ApiFormTableEntity)retrievedResult.Result).ToApiMap();
        }
    }
}
