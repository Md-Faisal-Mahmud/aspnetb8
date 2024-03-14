using Autofac;
using AutoMapper;
using FirstDemo.Application.Features.Training.Services;
using FirstDemo.Domain.Entities.Training;
using FirstDemo.Infrastructure.Models;

namespace FirstDemo.API.Models
{
    public class CourseModel
    {
        private ICourseService? _courseService;
        private IMapper _mapper;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Fees { get; set; }

        public CourseModel()
        {

        }

        public CourseModel(ICourseService coursService, IMapper mapper)
        {
            _courseService = coursService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _courseService = scope.Resolve<ICourseService>();
            _mapper = scope.Resolve<IMapper>();
        }

        internal IList<Course>? GetCourses()
        {
            return _courseService?.GetCourses();
        }

        internal void DeleteCourse(Guid id)
        {
            _courseService?.DeleteCourse(id);
        }

        internal void CreateCourse()
        {
            _courseService?.CreateCourse(Name, Fees);
        }

        internal void UpdateCourse()
        {
            _courseService?.UpdateCourse(Id, Name, Fees);
        }

        //internal Course GetCourse(string name)
        //{
        //    return _courseService.GetCourse(name);
        //}

        internal Course? GetCourse(Guid id)
        {
            return _courseService?.GetCourse(id);
        }

        internal async Task<object?> GetPagedCourses(DataTablesAjaxRequestUtility model)
        {

            var data = await _courseService?.GetPagedCoursesAsync(
                model.PageIndex,
                model.PageSize,
                model.SearchText,
                model.GetSortText(new string[] { "Title", "Fees", "ClassStartDate" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Fees.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
