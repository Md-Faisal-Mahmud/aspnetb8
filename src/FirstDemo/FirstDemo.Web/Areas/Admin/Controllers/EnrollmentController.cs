using Autofac;
using FirstDemo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EnrollmentController : Controller
    {
        ILifetimeScope _scope;
        ILogger<CourseController> _logger;


        public EnrollmentController(ILifetimeScope scope, ILogger<CourseController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [Authorize(Policy = "CourseViewRequirementPolicy")]
        public IActionResult Index()
        {
            var model = _scope.Resolve<EnrollmentListModel>();

            return View(model);
        }
    }
}
