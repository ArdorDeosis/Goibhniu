using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Goibhniu.Infrastructure;

public static class Setup
{
  /// <summary>
  /// Sets up infrastructure services for data provision.
  /// </summary>
  public static T AddInfrastructure<T>(this T builder) where T : IHostApplicationBuilder
  {
    builder.Services.Configure<DataSourcesSettings>(builder.Configuration.GetSection(DataSourcesSettings.SectionName));

    builder.Services.AddHttpClient<IProductDataService>().AddPolicyHandler(Resilience.DefaultPolicy);
    
    // NOTE: Even though the two instances will always be the same singletons, the scoped binding here allows the DI to
    // react to setting changes. 
    builder.Services.AddScoped(serviceProvider =>
      serviceProvider.GetRequiredService<IOptions<DataSourcesSettings>>().Value.AllowProductionData
        ? DeserializationStrategy.IncludeProductionData
        : DeserializationStrategy.Default
    );
    builder.Services.AddScoped<IProductDataService, ProductDataService>();

    return builder;
  }
}