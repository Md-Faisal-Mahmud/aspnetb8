using FirstDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Application.Features.Training.Services
{
    public interface ICourseRegistrationService
    {
        public void Register(string studentId, Guid courseId);
    }
}
