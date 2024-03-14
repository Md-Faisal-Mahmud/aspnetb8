using Autofac;
using FirstDemo.Infrastructure.Features.Exceptions;
using FirstDemo.Infrastructure.Models;
using FirstDemo.Web.Areas.Admin.Models;
using FirstDemo.Web.Models;
using FirstDemo.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class CourseController : Controller
	{
		ILifetimeScope _scope;
        ILogger<CourseController> _logger;


        public CourseController(ILifetimeScope scope, ILogger<CourseController> logger)
		{
			_scope = scope;
            _logger = logger;
        }

        [Authorize(Policy = "CourseViewRequirementPolicy")]
		public IActionResult Index()
		{
			var model = _scope.Resolve<CourseListModel>();

			return View(model);
		}

        [Authorize(Policy = "BackOfficeStaff")]
        public IActionResult Create()
        {
            var model = _scope.Resolve<CourseCreateModel>();

            return View(model);
        }

        [Authorize(Policy = "BackOfficeStaff")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CourseCreateModel model)
        {
            model.ResolveDependency(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateCourse();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully added a new course.",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch(DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch(Exception e)
                {
                    _logger.LogError(e, "Server Error");

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in creating course.",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            
            return View(model);
        }

        [Authorize(Policy = "BackOfficeStaff")]
        public IActionResult Update(Guid id)
        {
            var model = _scope.Resolve<CourseUpdateModel>();
            model.Load(id);
            return View(model);
        }

        [Authorize(Policy = "BackOfficeStaff")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(CourseUpdateModel model)
        {
            model.ResolveDependency(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    model.UpdateCourse();

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully updated course.",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in updating course.",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(model);
        }

        [Authorize(Policy = "BackOfficeStaff")]
        public IActionResult Delete(Guid id)
        {
            var model = _scope.Resolve<CourseListModel>();

            if (ModelState.IsValid)
            {
                try
                {
                    model.DeleteCourse(id);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");
                }
            }

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "CourseViewRequirementPolicy")]
        public async Task<JsonResult> GetCourses()
		{
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
			var model = _scope.Resolve<CourseListModel>();

            var data = await model.GetPagedCoursesAsync(dataTablesModel);
            return Json(data);
        }
    }
}
