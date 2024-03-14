using FirstDemo.Web.Data;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;

namespace FirstDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, 
            IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            /*
            AdoNetUtility adoNetUtility = 
                new AdoNetUtility(_config.GetConnectionString("DefaultConnection"));
            
            string title = "UI/UX";
            double fees = 30000;
            DateTime startDate = DateTime.Now;

            var sql = "insert into courses(Title, Fees, ClassStartDate) values(@title, @fees, @classStartDate)";
            adoNetUtility.WriteOperation(sql, new List<DbParameter>()
            {
                new SqlParameter("title", title),
                new SqlParameter("fees", fees),
                new SqlParameter("classStartDate", startDate)
            });
            

            var sql = "GetCourses";
            var result = adoNetUtility.ReadOperation(sql, null, true);

            return View(result);
            */

            return View();
        }

        public IActionResult Test()
        {
            var model = new TestModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Test(TestModel model)
        {
            return View(model);
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