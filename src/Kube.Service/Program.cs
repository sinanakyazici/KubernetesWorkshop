using System.Text;
var builder = WebApplication.CreateBuilder(args);
Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", (IEnumerable<EndpointDataSource> endpointSources, HttpContext context) =>
{
    var baseUrl = $"{context.Request.Scheme}://{context.Request.Host}";

    var routes = endpointSources
        .SelectMany(ds => ds.Endpoints)
        .OfType<RouteEndpoint>()
        .Select(endpoint => new
        {
            HttpMethod = endpoint.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault()?.HttpMethods.FirstOrDefault(),
            RoutePattern = endpoint.RoutePattern.RawText
        });

    var list = new StringBuilder();
    foreach (var route in routes)
    {
        list.Append($"{baseUrl}/{route.RoutePattern}".Trim('/'));
        list.Append(Environment.NewLine);
    }

    return Results.Content(list.ToString());
});


app.Run();
