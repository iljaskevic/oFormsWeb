using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using oFormsWeb.Repositories.Entities;
using Microsoft.Extensions.Logging;
using oFormsWeb.Models;
using Microsoft.Extensions.Options;

namespace oFormsWeb.Repositories
{
    public interface IUserRepository
    {
        void InsertNewUser(string azureObjectID);
    }

    public class UserRepository : BaseRepository, IUserRepository
    {
        ILogger<UserRepository> _logger;

        public UserRepository(ILogger<UserRepository> logger, IOptions<FormsConfiguration> _formsConfiguration) : base(_formsConfiguration)
        {
            _logger = logger;
        }

        public void InsertNewUser(string clientId)
        {
            _logger.LogDebug("Inserting new UserTableEntity - " + clientId);
            CloudTable userTable = GetUserTable();
            UserTableEntity newUser = new UserTableEntity(clientId);
            TableOperation insertNewUserOp = TableOperation.Insert(newUser);
            userTable.ExecuteAsync(insertNewUserOp);
            _logger.LogDebug("Finished inserting new UserTableEntity - " + clientId);
        }
    }
}
