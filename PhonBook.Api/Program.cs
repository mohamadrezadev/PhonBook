using Microsoft.EntityFrameworkCore;
using PhonBook.Core.Domain.Contacts;
using PhonBook.Core.Domain.Grops;
using PhonBook.infra.Data.sqlserver.Common;
using PhonBook.infra.Data.sqlserver.Contacts;
using PhonBook.infra.Data.sqlserver.Grops;

namespace PhonBook.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // builder.Services.AddControllers();
            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddSingleton<DapperContext>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddScoped<IContactRepository, ContactRepository>();
           
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

           
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}