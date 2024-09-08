using CoreMvcTemplate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;

namespace CoreMvcTemplate
{
    public class Program
    {
        // 0. �ק� Gitignore
        // 1. �w�� Nuget�GSerilog.AspNetCore => �ק� appsetting.json ���
        // 2. �w�� Nuget�GMicrosoft.EntityFrameworkCore.SqlServer�A�i�H�ϥ� EF
        // 3. �w�� Nuget�GMicrosoft.EntityFrameworkCore.Tools�A�i�H�ϥ� EF ���O
        //      ==> �]�w��Ʈw
        //      DB First�G
        //      Scaffold-DbContext "Server=���A����m;Database=��Ʈw;User ID=�b��;Password=�K�X;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoOnConfiguring -UseDatabaseNames -NoPluralize -Force
        //      �y�k�Ѿ\�Ghttps://learn.microsoft.com/zh-tw/ef/core/cli/powershell#scaffold-dbcontext
        //      �ɥR�GWindows ���ҡGTrusted_Connection=True
        //
        //      Code First�G
        //      �ݭn�إ� DbContextOptions �]�w�M appsetting connectionString
        //      ���ݭn Enable-Migration�A�i�H�����ϥ� Add-Migration


        public static IConfiguration Configuration { get; set; }
        public static Serilog.ILogger Logger { get; set; }


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // �]�w�s�� Log �t��
            Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(builder.Configuration)
                            .CreateLogger();
            // �����R�A�����ܼ�
            Logger = Log.Logger;
            // �ϥ� Serilog �@�������x�t��
            builder.Host.UseSerilog();

            // �����R�A�����ܼ�
            Configuration = builder.Configuration;
            // Ū�� appSetting �� value
            string ValueofBelowKey_1 = builder.Configuration["GetValueFromSetting:BelowKey"];
            string ValueofBelowKey_2 = builder.Configuration.GetSection("GetValueFromSetting: BelowKey").Value;


            // �]�w��Ʈw�`�J�P�����]�w
            builder.Services.AddDbContext<coredb2Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbName")));


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
        }
    }
}
