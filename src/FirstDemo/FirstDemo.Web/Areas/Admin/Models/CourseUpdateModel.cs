using Autofac;
using AutoMapper;
using FirstDemo.Application.Features.Training.Services;
using FirstDemo.Domain.Entities.Training;
using System.ComponentModel.DataAnnotations;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseUpdateModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, Range(0, 50000, ErrorMessage = "Fees should be between 0 & 50000")]
        public double Fees { get; set; }

        private ICourseService _courseService;
        private IMapper _mapper;

        public CourseUpdateModel()
        {

        }

        public CourseUpdateModel(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _courseService = scope.Resolve<ICourseService>();
            _mapper = scope.Resolve<IMapper>();
        }

        internal void Load(Guid id)
        {
            Course course = _courseService.GetCourse(id);
            if (course != null)
            {
                _mapper.Map(course, this);
            }
        }

        internal void UpdateCourse()
        {
            if (!string.IsNullOrWhiteSpace(Name)
                && Fees >= 0)
            {
                _courseService.UpdateCourse(Id, Name, Fees);
            }
        }
    }
}
