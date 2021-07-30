using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace SystemTests.Database.RunningModes
{
    public class DeleteBackupModeRunner : IDeleteBackupModeRunner
    {
        public DeleteBackupModeRunner(IConfiguration config, ILogger<DatabaseRegenerationApp> logger, IDatabaseManagementService databaseManagementService)
        {
            _logger = logger;
            _databaseManagementService = databaseManagementService;
            _backupDbName = config["TargetDbName"] + "Backup";
        }

        public void Run()
        {
            _logger.LogInformation("Running backup deletion...");

            if (_databaseManagementService.DatabaseExists(_backupDbName))
            {
                try
                {
                    _logger.LogInformation($"Deleting database {_backupDbName}...");
                    _databaseManagementService.DeleteDatabase(_backupDbName);
                }
                catch (Exception)
                {
                    _logger.LogError($"Failed to delete database {_backupDbName}");
                    throw;
                }
            }
            else
            {
                _logger.LogWarning("No backup database exists. Cancelling deletion request...");
                return;
            }

            _logger.LogInformation("Successfully deleted backup database");
        }

        private readonly ILogger<DatabaseRegenerationApp> _logger;
        private readonly IDatabaseManagementService _databaseManagementService;
        private readonly string _backupDbName;
    }
}
