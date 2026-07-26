
using Application;
using Infrastructure;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();


            /**************************
             * They are used in the UseInfraestructure middleware
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            ************************************/


            builder.Services.AddInfrastructurServices(builder.Configuration);
            builder.Services.AddJwtAuthentication(builder.Services.GetJwtSettings(builder.Configuration));

            builder.Services.AddApplicationServices();

            var app = builder.Build();

            // Database seeder
            await app.Services.AddDatabaseInitializerAsync();

            // Configure the HTTP request pipeline.

            /*********
             * It is call on UseInfraestructure Middleware
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger(); // serves /swagger/v1/swagger.json
                app.UseSwaggerUI(); // UI at /swagger
            }
            ********/

            app.UseHttpsRedirection();

            // It is call on the infrasctructure layer on Startup.cs - UseInfrastructure middleware
            //app.UseAuthorization();

            app.UseInfrastructure();

            // add global error handler
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
