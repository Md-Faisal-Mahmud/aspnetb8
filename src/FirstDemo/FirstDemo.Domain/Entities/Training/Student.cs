using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Domain.Entities.Training
{
    public class Student : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Cgpa { get; set; }
        public IList<CourseStudent> Courses { get; set; }
    }
}
