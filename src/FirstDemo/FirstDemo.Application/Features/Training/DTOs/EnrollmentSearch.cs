namespace FirstDemo.Application.Features.Training.DTOs
{
    public class EnrollmentSearch
    {
        public string? CourseName { get; set; }
        public string? StudentName { get; set; }
        public DateTime? EnrollmentDateFrom { get; set; }
        public DateTime? EnrollmentDateTo { get; set; }
    }
}
