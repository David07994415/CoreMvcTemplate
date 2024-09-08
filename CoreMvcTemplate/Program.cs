using CoreMvcTemplate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;

namespace CoreMvcTemplate
{
    public class Program
    {
        // 0. 修改 Gitignore
        // 1. 安裝 Nuget：Serilog.AspNetCore => 修改 appsetting.json 資料
        // 2. 安裝 Nuget：Microsoft.EntityFrameworkCore.SqlServer，可以使用 EF
        // 3. 安裝 Nuget：Microsoft.EntityFrameworkCore.Tools，可以使用 EF 指令
        //      ==> 設定資料庫
        //      DB First：
        //      Scaffold-DbContext "Server=伺服器位置;Database=資料庫;User ID=帳號;Password=密碼;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoOnConfiguring -UseDatabaseNames -NoPluralize -Force
        //      語法參閱：https://learn.microsoft.com/zh-tw/ef/core/cli/powershell#scaffold-dbcontext
        //      補充：Windows 驗證：Trusted_Connection=True
        //
        //      Code First：
        //      需要建立 DbContextOptions 設定和 appsetting connectionString
        //      不需要 Enable-Migration，可以直接使用 Add-Migration


        public static IConfiguration Configuration { get; set; }
        public static Serilog.ILogger Logger { get; set; }


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // 設定新的 Log 系統
            Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(builder.Configuration)
                            .CreateLogger();
            // 給予靜態全域變數
            Logger = Log.Logger;
            // 使用 Serilog 作為全域日誌系統
            builder.Host.UseSerilog();

            // 給予靜態全域變數
            Configuration = builder.Configuration;
            // 讀取 appSetting 的 value
            string ValueofBelowKey_1 = builder.Configuration["GetValueFromSetting:BelowKey"];
            string ValueofBelowKey_2 = builder.Configuration.GetSection("GetValueFromSetting: BelowKey").Value;


            // 設定資料庫注入與相關設定
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
