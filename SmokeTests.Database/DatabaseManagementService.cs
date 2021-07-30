using Microsoft.Extensions.Logging;
using System;

namespace SystemTests.Database
{
    public class DatabaseManagementService : IDatabaseManagementService
    {
        public DatabaseManagementService(IDatabaseManagementRepository repository, ILogger<DatabaseManagementService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public bool DatabaseExists(string databaseName)
        {
            _logger.LogInformation($"Accessing DB to check if {databaseName} exists...");
            bool result;

            try
            {
                result = _repository.DatabaseExists(databaseName);
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to check if DB {databaseName} exists.");
                throw;
            }

            _logger.LogInformation($"Successfully checked if {databaseName} exists. Result: {result}");
            return result;
        }

        public void DeleteDatabase(string databaseName)
        {
            _logger.LogInformation($"Dropping the DB {databaseName}...");

            var result = _repository.DeleteDatabase(databaseName);

            if (result != -1)
            {
                var errorMessage = $"SQL query to drop the DB {databaseName} failed.";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }

            _logger.LogInformation($"Successfully dropped the DB {databaseName}.");
        }

        public void RenameDatabase(string oldName, string newName)
        {
            _logger.LogInformation($"Accessing DB to rename {oldName} to {newName}...");
            var result = _repository.RenameDatabase(oldName, newName);

            if (result != -1)
            {
                var errorMessage = $"SQL query to rename {oldName} to {newName} failed.";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }

            _logger.LogInformation($"Successfully renamed the DB {oldName} to {newName}.");
        }

        private readonly IDatabaseManagementRepository _repository;
        private readonly ILogger _logger;
    }
}
