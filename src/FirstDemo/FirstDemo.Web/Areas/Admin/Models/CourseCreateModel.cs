using Autofac;
using FirstDemo.Application.Features.Training.Services;
using System.ComponentModel.DataAnnotations;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseCreateModel
    {
        [Required]
        public string Name { get; set; }
        [Required, Range(0, 50000, ErrorMessage = "Fees should be between 0 & 50000")]
        public double Fees { get; set; }

        private ICourseService _courseService;

        public CourseCreateModel()
        {

        }

        public CourseCreateModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _courseService = scope.Resolve<ICourseService>();
        }

        internal void CreateCourse()
        {
            if(!string.IsNullOrWhiteSpace(Name)
                && Fees >=0)
            {
                _courseService.CreateCourse(Name, Fees);
            }
        }
    }
}
