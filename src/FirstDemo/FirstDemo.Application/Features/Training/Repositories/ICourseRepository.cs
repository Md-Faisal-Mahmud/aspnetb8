using FirstDemo.Domain.Entities.Training;
using FirstDemo.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Application.Features.Training.Repositories
{
    public interface ICourseRepository : IRepositoryBase<Course, Guid>
    {
        Task<(IList<Course> records, int total, int totalDisplay)>
            GetTableDataAsync(Expression<Func<Course, bool>> expression, string orderBy, 
            int pageIndex, int pageSize);
        bool IsDuplicateName(string name, Guid? id);
    }
}
