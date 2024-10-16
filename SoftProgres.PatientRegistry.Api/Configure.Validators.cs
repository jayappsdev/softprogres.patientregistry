using SoftProgres.PatientRegistry.Api.Validators;

[assembly: HostingStartup(typeof(SoftProgres.PatientRegistry.Api.ConfigureValidators))]

namespace SoftProgres.PatientRegistry.Api;

public class ConfigureValidators : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((_, services) =>
        {
            services.AddSingleton<IBirthNumberValidator, BirthNumberValidator>();
        });
}