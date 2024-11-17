using Microsoft.EntityFrameworkCore;
using Universities.Infrastructure.Contracts;
using Universities.Infrastructure.Impl;
using Universities.Infrastructure.Impl.DbContext;
using Universities.Library.Contracts;
using Universities.Library.Impl;

namespace Universities.DistributedServices.WebApiUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services
          .AddScoped<IUniversitiesApiRepository, UniversitiesApiRepository>()
          .AddScoped<IUniversitiesDBRepository, UniversitiesDBRepository>()
          .AddScoped<IUniversitiesService, UniversitiesService>();

            builder.Services.AddDbContext<UniversityDBContext>(options =>
           options.UseSqlServer("Data Source=074BCN2024\\SQLEXPRESS;Initial Catalog=UniversityDB;User ID=adria;Password=1234;Trust Server Certificate=True"));



            builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
