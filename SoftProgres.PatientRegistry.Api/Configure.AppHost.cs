[assembly: HostingStartup(typeof(SoftProgres.PatientRegistry.Api.AppHost))]

namespace SoftProgres.PatientRegistry.Api;

public class AppHost() : AppHostBase("SoftProgres.PatientRegistry.Api"), IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            // Configure ASP.NET Core IOC Dependencies
        });

    public override void Configure()
    {
        // Configure ServiceStack, Run custom logic after ASP.NET Core Startup
        SetConfig(new HostConfig {
            DebugMode = true
        });
    }
}