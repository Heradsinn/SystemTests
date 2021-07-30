using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace SystemTests.Database.RunningModes
{
    public class DeleteModeRunner : IDeleteModeRunner
    {
        public DeleteModeRunner(IConfiguration config, ILogger<DatabaseRegenerationApp> logger, IDatabaseManagementService databaseManagementService)
        {
            _logger = logger;
            _databaseManagementService = databaseManagementService;
            _targetDbName = config["TargetDbName"];
        }

        public void Run()
        {
            _logger.LogInformation("Running database deletion...");

            if (_databaseManagementService.DatabaseExists(_targetDbName))
            {
                try
                {
                    _logger.LogInformation($"Deleting database {_targetDbName}...");
                    _databaseManagementService.DeleteDatabase(_targetDbName);
                }
                catch (Exception)
                {
                    _logger.LogError($"Failed to delete database {_targetDbName}");
                    throw;
                }
            }
            else
            {
                _logger.LogWarning("No current database exists. Cancelling request to delete database...");
                return;
            }

            _logger.LogInformation($"Successfully deleted current database");
        }

        private readonly ILogger<DatabaseRegenerationApp> _logger;
        private readonly IDatabaseManagementService _databaseManagementService;
        private readonly string _targetDbName;
    }
}
