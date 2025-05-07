using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Goibhniu.Application;

/// <summary>
/// Provides extension methods to set up services, configure routing, and map product data analysis endpoints
/// for use within an ASP.NET Core application.
/// </summary>
public static class Setup
{
  /// <summary>
  /// Sets up services needed for data representation conversion.
  /// </summary>
  public static T AddDataRepresentationProvider<T>(this T builder) where T : IHostApplicationBuilder
  {
    builder.Services.Configure<DataRepresentationSettings>(
      builder.Configuration.GetSection(DataRepresentationSettings.SectionName));

    builder.Services.AddScoped<IRepresentationProvider>(serviceProvider =>
      serviceProvider.GetRequiredService<IOptions<DataRepresentationSettings>>().Value.DataRepresentationType switch
      {
        DataRepresentationType.Raw =>
          ActivatorUtilities.CreateInstance<RawProductRepresentationProvider>(serviceProvider),
        DataRepresentationType.OnlyRelevantArticles =>
          ActivatorUtilities.CreateInstance<ArticleFilteredRepresentationProvider>(serviceProvider),
        var value =>
          throw new NotSupportedException($"Unsupported data representation type {value}"),
      }
    );
    
    builder.Services.AddScoped<HttpResponseExceptionFilter>();
    builder.Services.AddControllers();

    return builder;
  }
}