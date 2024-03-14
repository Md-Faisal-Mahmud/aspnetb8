using FirstDemo.Application.Features.Training.DTOs;
using FirstDemo.Application.Features.Training.Services;
using FirstDemo.Domain.Entities.Training;
using FirstDemo.Domain.Services;
using FirstDemo.Domain.UnitOfWorks;
using FirstDemo.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Infrastructure.Features.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IAdoNetUtility _adoNetUtility;
        private readonly IEmailService _emailService;

        public EnrollmentService(IAdoNetUtility adoNetUtility, 
            IEmailService emailService)
        {
            _adoNetUtility = adoNetUtility;
            _emailService = emailService;
        }

        public async Task<(IList<EnrollmentDTO> records, int total, int totalDisplay)> 
            GetPagedEnrollmentsAsync(int pageIndex,
            int pageSize, string? courseName, string? studentName, 
            DateTime? enrollDateFrom, DateTime? enrollDateTo,
            string orderBy)
        {
            var outParameters = new Dictionary<string, Type>()
            {
                { "Total", typeof(int) },
                { "TotalDisplay", typeof(int) }
            };

            var result = await _adoNetUtility.QueryWithStoredProcedureAsync<EnrollmentDTO>("GetCourseEnrollments",
                    new Dictionary<string, object> 
                    {
                        { "PageIndex", pageIndex},
                        { "PageSize", pageSize },
                        { "CourseName", courseName},
                        { "StudentName", studentName },
                        { "EnrollmentDateFrom", enrollDateFrom },
                        { "EnrollmentDateTo", enrollDateTo },
                        { "OrderBy", orderBy }
                    },
                    outParameters);

            return (result.result, int.Parse(result.outValues.ElementAt(0).Value.ToString()), int.Parse(result.outValues.ElementAt(1).Value.ToString()));
        }
    }
}
