
using Serilog;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Register application services (repositories, services, AutoMapper, etc.)
            builder.Services.AddApplicationServices(builder.Configuration);

            // Add Serilog
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)  // Read from appsettings.json
                .Enrich.FromLogContext()
                .CreateLogger();

            // Use Serilog as the logger provider
            builder.Host.UseSerilog();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Add the global exception middleware
            app.UseMiddleware<GlobalExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            // Ensure to flush and close the log before the application exits
            Log.CloseAndFlush();
        }
    }
}
