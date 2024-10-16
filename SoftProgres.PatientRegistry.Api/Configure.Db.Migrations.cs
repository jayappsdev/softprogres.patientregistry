using ServiceStack.Data;
using ServiceStack.OrmLite;
using SoftProgres.PatientRegistry.Api.Database.Migrations;

[assembly: HostingStartup(typeof(SoftProgres.PatientRegistry.Api.ConfigureDbMigrations))]

namespace SoftProgres.PatientRegistry.Api;

public class ConfigureDbMigrations : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureAppHost(appHost =>
        {
            var migrator = new Migrator(appHost.Resolve<IDbConnectionFactory>(), typeof(Migration0001).Assembly);
            migrator.Run();
        });

   
}