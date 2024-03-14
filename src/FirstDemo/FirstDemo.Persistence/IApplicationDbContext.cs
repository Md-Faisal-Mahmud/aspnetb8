using FirstDemo.Domain.Entities.Training;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Course> Courses { get; set; }
    }
}