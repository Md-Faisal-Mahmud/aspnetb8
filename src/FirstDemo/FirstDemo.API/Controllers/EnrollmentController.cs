using Autofac;
using FirstDemo.API.Models;
using FirstDemo.Application.Features.Training.DTOs;
using FirstDemo.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.API.Controllers
{
    [ApiController]
    [Route("v3/[controller]")]
    [EnableCors("AllowSites")]
    public class EnrollmentController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<CourseController> _logger;

        public EnrollmentController(ILogger<CourseController> logger, 
            ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        
        [HttpGet, Authorize(Policy = "CourseViewRequirementPolicy")]
        public async Task<object> Get()
        {
            var model = _scope.Resolve<EnrollmentModel>();
            model.SearchItem = new EnrollmentSearch();
            model.SearchItem.CourseName = Request.Query["SearchItem[CourseName]"];
            model.SearchItem.StudentName = Request.Query["SearchItem[StudentName]"];

            var enrollmentDateFrom = Request.Query["SearchItem[EnrollmentDateFrom]"];
            var enrollmentDateTo = Request.Query["SearchItem[EnrollmentDateTo]"];

            if (!string.IsNullOrWhiteSpace(enrollmentDateFrom))
                model.SearchItem.EnrollmentDateFrom = DateTime.Parse(enrollmentDateFrom);
            else
                model.SearchItem.EnrollmentDateFrom = null;

            if (!string.IsNullOrWhiteSpace(enrollmentDateTo))
                model.SearchItem.EnrollmentDateTo = DateTime.Parse(enrollmentDateTo);
            else
                model.SearchItem.EnrollmentDateTo = null;

            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            model.ResolveDependency(_scope);

            var data = await model.GetPagedCoursesAdvanced(dataTablesModel);
            
            return data;
        }
    }
}