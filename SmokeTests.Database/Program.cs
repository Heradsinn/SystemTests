using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SystemTests.Database.RunningModes;

namespace SystemTests.Database
{
    public class Program
    {
        static void Main()
        {
            // DI setup
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IDatabaseRegenerationApp, DatabaseRegenerationApp>();
                    services.AddTransient<IDatabaseManagementRepository, DatabaseManagementRepository>();
                    services.AddTransient<IDatabaseManagementService, DatabaseManagementService>();
                    services.AddTransient<IDatabaseImportService, DatabaseImportService>();
                    services.AddTransient<IFileManagementService, FileManagementService>();
                    services.AddTransient<IImportModeRunner, ImportModeRunner>();
                    services.AddTransient<IBackupModeRunner, BackupModeRunner>();
                    services.AddTransient<IDeleteBackupModeRunner, DeleteBackupModeRunner>();
                    services.AddTransient<IDeleteModeRunner, DeleteModeRunner>();
                    services.AddTransient<IBackupSwapModeRunner, BackupSwapModeRunner>();
                })
                .Build();

            host.Services.GetService<ILogger<Program>>()
                .LogInformation("Application started...");

            // Application startup
            var svc = ActivatorUtilities.CreateInstance<DatabaseRegenerationApp>(host.Services);
            svc.Run();
            host.Dispose();
        }
    }
}
