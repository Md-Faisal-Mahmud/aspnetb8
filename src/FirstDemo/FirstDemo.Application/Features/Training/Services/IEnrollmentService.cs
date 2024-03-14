using FirstDemo.Application.Features.Training.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Application.Features.Training.Services
{
    public interface IEnrollmentService
    {
        Task<(IList<EnrollmentDTO> records, int total, int totalDisplay)> GetPagedEnrollmentsAsync(int pageIndex,
            int pageSize, string? courseName, string? studentName,
            DateTime? enrollDateFrom, DateTime? enrollDateTo,
            string orderBy);
    }
}
