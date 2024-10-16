using SoftProgres.PatientRegistry.Api.Helpers;

[assembly: HostingStartup(typeof(SoftProgres.PatientRegistry.Api.ConfigureHelpers))]

namespace SoftProgres.PatientRegistry.Api;

public class ConfigureHelpers : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((_, services) =>
        {
            services.AddSingleton<IBirthNumberHelper, BirthNumberHelper>();
        });
}