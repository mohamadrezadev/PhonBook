using Microsoft.EntityFrameworkCore;
using PhonBook.Core.Domain.Contacts;
using PhonBook.Core.Domain.Grops;
using PhonBook.infra.Data.sqlserver.Common;
using PhonBook.infra.Data.sqlserver.Contacts;
using PhonBook.infra.Data.sqlserver.Grops;

namespace PhonBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbcontext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddSingleton<DapperContext>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
            app.Use(async (context, next) =>
                {
                    await next();
                    if (context.Response.StatusCode == 404)
                    {
                        context.Request.Path = "/Home";
                        await next();
                    }
                });
                    }
                }
}