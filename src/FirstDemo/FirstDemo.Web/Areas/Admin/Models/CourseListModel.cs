using Autofac;
using FirstDemo.Application.Features.Training.Services;
using FirstDemo.Domain.Entities.Training;
using FirstDemo.Infrastructure.Models;
using System.Text.Encodings.Web;
using System.Web;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseListModel
    {
        private readonly ICourseService _courseService;

        public CourseListModel()
        {

        }

        public CourseListModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IList<Course> GetPopularCourses()
        {
            return _courseService.GetCourses();
        }

        public async Task<object> GetPagedCoursesAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _courseService.GetPagedCoursesAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                dataTablesUtility.SearchText,
                dataTablesUtility.GetSortText(new string[] { "Name", "Fee" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                HttpUtility.HtmlEncode(record.Name),
                                record.Fees.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void DeleteCourse(Guid id)
        {
            _courseService.DeleteCourse(id);
        }
    }
}
