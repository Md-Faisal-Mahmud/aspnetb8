using Autofac;
using AutoMapper;
using FirstDemo.Application.Features.Training.DTOs;
using FirstDemo.Application.Features.Training.Services;
using FirstDemo.Infrastructure.Features.Services;
using FirstDemo.Infrastructure.Models;

namespace FirstDemo.API.Models
{
    public class EnrollmentModel
    {
        private IMapper _mapper;
        private IEnrollmentService _enrollmentService;
        public EnrollmentSearch SearchItem { get; set; }

        public EnrollmentModel()
        {

        }

        public EnrollmentModel(IEnrollmentService enrollmentService, IMapper mapper)
        {
            _enrollmentService = enrollmentService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _enrollmentService = scope.Resolve<IEnrollmentService>();
            _mapper = scope.Resolve<IMapper>();
        }

        internal async Task<object> GetPagedCoursesAdvanced(DataTablesAjaxRequestUtility dataTablesModel)
        {
            var data = await _enrollmentService?.GetPagedEnrollmentsAsync(
                dataTablesModel.PageIndex,
                dataTablesModel.PageSize,
                SearchItem.CourseName,
                SearchItem.StudentName,
                SearchItem.EnrollmentDateFrom,
                SearchItem.EnrollmentDateTo,
                dataTablesModel.GetSortText(new string[] { "c.Name", "s.Name", "EnrollmentDate" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.CourseName,
                                record.StudentName,
                                record.EnrollDate.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
