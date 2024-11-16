using Microsoft.AspNetCore.Mvc;
using log4net;

namespace MyAspNetCoreApp.Controllers
{
    public class HelloController : Controller
    {


        // [HttpGet("/hello")]
        // public IActionResult Index()
        // {
        //     return Content("Hello, ASP.NET Core from Visual Studio Code!");
        // }
        // 使用 log4net 的 GetLogger 來取得日誌物件
        private static readonly ILog log = LogManager.GetLogger(typeof(HelloController));

        [HttpGet("/hello")]
        public IActionResult Index()
        {
            // 記錄一條 Info 級別的日誌
            log.Info("Index action has been called.");

            try
            {
                // 模擬業務邏輯
                log.Debug("Starting some process in Index method...");

                // 假設發生例外情況
                // throw new Exception("An unexpected error occurred!");

                return Content("Hello, ASP.NET Core from Visual Studio Code!");
            }
            catch (Exception ex)
            {
                // 記錄錯誤日誌
                log.Error("An error occurred in Index action.", ex);
                return StatusCode(500, "Internal Server Error");
            }
            finally
            {
                log.Debug("Index action completed.");
            }
        }





    }
}