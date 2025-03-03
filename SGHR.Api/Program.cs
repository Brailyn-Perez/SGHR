
using Microsoft.EntityFrameworkCore;
using SGHR.IOC.DependencyInjection;
using SGHR.Persistence.Context;

namespace SGHR.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SGHRContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("LeandroConnection")));


            // aqui es donde se inyectan todas las dependencias necesarias
            builder.Services.AddAplicationServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
