using ServiceStack.Data;
using ServiceStack.OrmLite;
using SoftProgres.PatientRegistry.Api.Database;

[assembly: HostingStartup(typeof(SoftProgres.PatientRegistry.Api.ConfigureDb))]

namespace SoftProgres.PatientRegistry.Api;

public class ConfigureDb : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((_, services) =>
        {
            const string connectionString = "DataSource=App_Data/app.db;Cache=Shared";
            services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
                connectionString, SqliteDialect.Provider));
            services.AddSingleton<IDataProvider, DataProvider>();
        });

}