using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);
builder.Services.AddOcelot(builder.Configuration).AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();
await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

var logFactory = new LoggerFactory().CreateLogger<Type>();
logFactory.LogInformation("EnrironmentName:" + builder.Environment.EnvironmentName);
app.Run();