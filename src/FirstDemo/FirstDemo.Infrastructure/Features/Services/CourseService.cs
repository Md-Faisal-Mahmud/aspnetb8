using FirstDemo.Application;
using FirstDemo.Application.Features.Training.Services;
using FirstDemo.Domain.Entities.Training;
using FirstDemo.Infrastructure.Features.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Infrastructure.Features.Services
{
    public class CourseService : ICourseService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public CourseService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateCourse(string name, double fees)
        {
            if (_unitOfWork.Courses.IsDuplicateName(name, null))
                throw new DuplicateNameException("Course name is duplicate");

            Course course = new Course()
            {
                Name = name,
                Fees = fees
            };

            _unitOfWork.Courses.Add(course);
            _unitOfWork.Save();
        }

        public Course GetCourse(Guid id)
        {
            return _unitOfWork.Courses.GetById(id);
        }

        public IList<Course> GetCourses()
        {
            return _unitOfWork.Courses.GetAll();
        }

        public async Task<(IList<Course> records, int total, int totalDisplay)> GetPagedCoursesAsync(int pageIndex,
            int pageSize, string searchText, string orderBy)
        {
            var result = await _unitOfWork.Courses.GetTableDataAsync(
                x => x.Name.Contains(searchText), orderBy, pageIndex, pageSize);

            return result;
        }

        public void UpdateCourse(Guid id, string name, double fees)
        {
            if (_unitOfWork.Courses.IsDuplicateName(name, id))
                throw new DuplicateNameException("Course name is duplicate");

            Course course = _unitOfWork.Courses.GetById(id);
            course.Name = name;
            course.Fees = fees;

            _unitOfWork.Save();
        }

        public void DeleteCourse(Guid id)
        {
            _unitOfWork.Courses.Remove(id);
            _unitOfWork.Save();
        }
    }
}
