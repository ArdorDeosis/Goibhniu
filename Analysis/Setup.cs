using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Goibhniu.Analysis;

public static class Setup
{
  /// <summary>
  /// Sets up data analysis services.
  /// </summary>
  public static T AddProductAnalysisServices<T>(this T builder) where T : IHostApplicationBuilder
  {
    builder.Services.AddSingleton<IProductAnalysisService, ProductAnalysisService>();
    return builder;
  }
}