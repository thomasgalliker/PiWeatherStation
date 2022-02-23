using WeatherDisplay;
using WeatherDisplay.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSystemd();
builder.Host.UseWindowsService();

// Configure Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Configure Services
var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddWeatherDisplay(builder.Configuration);
services.AddHostedService<AutoStartupBackgroundService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
