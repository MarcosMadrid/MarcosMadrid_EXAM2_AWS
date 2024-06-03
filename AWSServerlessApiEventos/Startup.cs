using AWSServerlessApiEventos.Data;
using AWSServerlessApiEventos.Helpers;
using AWSServerlessApiEventos.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AWSServerlessApiEventos;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        string connectionString = JsonDocument.Parse(HelperSecretManager.GetSecretAsync().Result)
            .RootElement
            .GetProperty("MySql")
            .GetString()!;

        services.AddTransient<RepositoryEventos>();
        services.AddDbContext<EventosContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            )
        );

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My Api Eventos Marcos", Version = "v1" });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(options =>
        {
            options.AllowAnyOrigin();
        });

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(url: "swagger/v1/swagger.json", name: "Api Eventos");
            options.RoutePrefix = "";
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}