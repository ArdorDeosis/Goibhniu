using Goibhniu.Analysis;
using Goibhniu.Application;
using Goibhniu.Infrastructure;

var builder = WebApplication
  .CreateBuilder(args)
  .AddProductAnalysisServices()
  .AddInfrastructure()
  .AddDataRepresentationProvider();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();