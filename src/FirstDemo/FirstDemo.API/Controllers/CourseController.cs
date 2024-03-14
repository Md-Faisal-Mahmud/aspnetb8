using Autofac;
using FirstDemo.API.Models;
using FirstDemo.Domain.Entities.Training;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.API.Controllers
{
    [ApiController]
    [Route("v3/[controller]")]
    [EnableCors("AllowSites")]
    public class CourseController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        /*
        [HttpGet, Authorize(Policy = "CourseViewRequirementPolicy")]
        public object Get()
        {
            _logger.LogInformation($"Origin:" + Request.Headers.Origin.Count);
            for(int i = 0; i < Request.Headers.Origin.Count; i++)
            {
                _logger.LogInformation($"Origin:" + Request.Headers.Origin[i]);
            }
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);

            var model = _scope.Resolve<CourseModel>();

            var data = model.GetPagedCourses(dataTablesModel);
            return data;
        }
        */

        [HttpGet, Authorize(Policy = "CourseViewRequirementPolicy")]
        public IEnumerable<Course> Get()
        {
            try
            {
                var model = _scope.Resolve<CourseModel>();
                return model.GetCourses();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't get courses");
                return null;
            }
        }

        [HttpGet("{id}")]
        public Course Get(Guid id)
        {
            var model = _scope.Resolve<CourseModel>();
            return model.GetCourse(id);
        }

        //[HttpGet("{name}")]
        //public Course Get(string name)
        //{
        //    var model = _scope.Resolve<CourseModel>();
        //    return model.GetCourse(name);
        //}

        [HttpPost()]
        public IActionResult Post([FromBody] CourseModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                model.CreateCourse();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't delete course");
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(CourseModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                model.UpdateCourse();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't delete course");
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var model = _scope.Resolve<CourseModel>();
                model.DeleteCourse(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't delete course");
                return BadRequest();
            }
        }
    }
}