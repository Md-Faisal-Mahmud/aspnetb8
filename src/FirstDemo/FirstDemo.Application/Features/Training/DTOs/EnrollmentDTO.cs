using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Application.Features.Training.DTOs
{
    public class EnrollmentDTO
    {
        public string CourseName {  get; set; }
        public string StudentName { get; set; }
        public DateTime EnrollDate { get; set; }
    }
}
