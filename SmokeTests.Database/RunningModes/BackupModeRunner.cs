using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using SystemTests.Database.RunningModes;

namespace SystemTests.Database
{
    public class BackupModeRunner : IBackupModeRunner
    {
        public BackupModeRunner(IConfiguration config, ILogger<DatabaseRegenerationApp> logger, IDatabaseManagementService databaseManagementService)
        {
            _logger = logger;
            _databaseManagementService = databaseManagementService;
            _targetDbName = config["TargetDbName"];
        }

        public void Run()
        {
            _logger.LogInformation("Running database backup...");

            if (_databaseManagementService.DatabaseExists(_targetDbName + "Backup"))
            {
                _logger.LogWarning("Backup already exists. Cancelling request to backup current database...");
                return;
            }

            if (!_databaseManagementService.DatabaseExists(_targetDbName))
            {
                _logger.LogWarning("No current database exists. Cancelling request to backup current database...");
                return;
            }

            try
            {
                _logger.LogInformation($"Backing up database {_targetDbName} as {_targetDbName + "Backup"}...");
                _databaseManagementService.RenameDatabase(_targetDbName, _targetDbName + "Backup");
            }
            catch (Exception)
            {
                _logger.LogError("Failed to backup database.");
                throw;
            }

            _logger.LogInformation($"Successfully backed up database {_targetDbName} as {_targetDbName}Backup");
        }

        private readonly ILogger<DatabaseRegenerationApp> _logger;
        private readonly IDatabaseManagementService _databaseManagementService;
        private readonly string _targetDbName;
    }
}
