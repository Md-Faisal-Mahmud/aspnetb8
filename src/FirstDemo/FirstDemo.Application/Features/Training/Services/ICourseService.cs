using FirstDemo.Domain.Entities.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Application.Features.Training.Services
{
    public interface ICourseService
    {
        void CreateCourse(string name, double fees);
        void DeleteCourse(Guid id);
        Course GetCourse(Guid id);
        public IList<Course> GetCourses();
        Task<(IList<Course> records, int total, int totalDisplay)> GetPagedCoursesAsync(int pageIndex,
            int pageSize, string searchText, string orderBy);
        void UpdateCourse(Guid id, string name, double fees);
    }
}
