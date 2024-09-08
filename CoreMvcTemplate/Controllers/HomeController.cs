using CoreMvcTemplate.Models;
using CoreMvcTemplate.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace CoreMvcTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly coredb2Context _db;

        public HomeController(ILogger<HomeController> logger,IConfiguration configuration,coredb2Context db)
        {
            _logger = logger;
            _configuration = configuration;
            _db = db;
        }

        public IActionResult Index()
        {
            var DataFromDb = _db.MainType.Where(x => x.id == 1).FirstOrDefault().description;
            ViewBag.Data = DataFromDb;
            string UseGlobalStaticCallConfig = Program.Configuration["GetValueFromSetting:BelowKey"];
            Program.Logger.Information("使用全域靜態Logger紀錄結構化，{testApvalue}", UseGlobalStaticCallConfig);
            try
            {
                throw new Exception("這是exception");
            }
            catch (Exception ex)
            {
                _logger.LogError("Main Exception: {Message}, {StackTrace}", ex.Message, ex.StackTrace);
                if (ex.InnerException != null)
                {
                    _logger.LogError("Inner Exception: {Message}, {StackTrace}", ex.InnerException.Message, ex.InnerException.StackTrace);
                }
            }

            return View();
        }

        // Retrun JSON formate
        [HttpPost]
        public IActionResult PostJson([Required]InputViewModel input) 
        {
            bool vaild = ModelState.IsValid; // MVC Core Not Valid 不會自動 Retrun 400

            ResultViewModel result = new ResultViewModel();
            result.Status = "200";
            result.Message = "成功";
            var DataListFromDb = _db.MainType.Where(x => x.id == 1).ToList();
            result.Data = DataListFromDb;
            return Json(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
