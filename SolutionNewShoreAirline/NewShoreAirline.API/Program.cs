using Microsoft.OpenApi.Models;
using NewShoreAirline.DataAccess.Dacs;
using NewShoreAirline.DataAccess.Interfaces;
using NewShoreAirline.Services.Interfaces;
using NewShoreAirline.Services.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Assembly GetAssemblyByName(string name)
{
    return AppDomain.CurrentDomain.GetAssemblies().
           SingleOrDefault(assembly => assembly.GetName().Name == name);
}

var a = GetAssemblyByName("NewShoreAirline.API");

using var stream = a.GetManifestResourceStream("NewShoreAirline.API.appsettings.json");

var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

builder.Configuration.AddConfiguration(config);

builder.Services.AddControllers()
     .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Endpoints for test NewShore developer by Juan Diego Duque", Version = "v1.1" });
});

//DEPENDENCY INJECTION
builder.Services.AddTransient<IConexionDac, ConexionDac>()
                .AddTransient<IFlightService, FlightsService>()
                .AddTransient<IFlightsDac, FlightsDac>()
                .AddTransient<IJourneysDac, JourneysDac>()
                .AddTransient<ITransportsDac, TransportsDac>();

builder.Services.AddCors();

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors(options =>
    {
        options.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
}

app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseDefaultFiles();

app.UseStaticFiles();

app.Run();

