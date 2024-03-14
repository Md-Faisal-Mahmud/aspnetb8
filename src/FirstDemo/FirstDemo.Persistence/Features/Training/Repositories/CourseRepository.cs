using FirstDemo.Application.Features.Training.Repositories;
using FirstDemo.Domain.Entities.Training;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Persistence.Features.Training.Repositories
{
    public class CourseRepository : Repository<Course, Guid>, ICourseRepository
	{
		public CourseRepository(IApplicationDbContext context) : base((DbContext)context)
		{
		}

        public async Task<(IList<Course> records, int total, int totalDisplay)> 
            GetTableDataAsync(Expression<Func<Course, bool>> expression, 
            string orderBy, int pageIndex, int pageSize)
        {
            return await GetDynamicAsync(expression, orderBy, null, 
                pageIndex, pageSize, true);
        }

        public bool IsDuplicateName(string name, Guid? id)
		{
			int? existingCourseCount = null;

            if (id.HasValue)
                existingCourseCount = GetCount(x => x.Name == name && x.Id != id.Value);
            else
				existingCourseCount = GetCount(x => x.Name == name);

            return existingCourseCount > 0;
        }
	}
}
