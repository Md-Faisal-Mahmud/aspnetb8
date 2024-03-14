using FirstDemo.Domain.Entities.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Persistence.Seeds
{
    internal static class StudentSeed
    {
        public static IList<Student> Students
        {
            get
            {
                return new List<Student>
                {
                    new Student { Id = new Guid("3B8D98FE-12AA-48D2-BB81-B63B93989131"), Name = "Jalaluddin", Cgpa = 2.0 },
                    new Student { Id = new Guid("F71E5930-3BD0-4ED7-86F4-925B8E33D797"), Name = "Tareq", Cgpa = 3.0 },
                    new Student { Id = new Guid("B01D7E97-2C4C-451C-8201-F2CAF8B9A0DC"), Name = "Hasan", Cgpa = 3.2 },
                };
            }
        }
    }
}
