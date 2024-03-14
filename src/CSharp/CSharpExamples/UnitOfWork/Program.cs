// See https://aka.ms/new-console-template for more information


using RepositoryPattern;
using UnitOfWork;
string connectionSting = "";
TrainingUnitOfWork unitOfWork = new TrainingUnitOfWork(connectionSting);
unitOfWork.CourseRepository.Add(new Course { Title = "C#", Fees = 8000, IsActive = true });
unitOfWork.CourseRepository.Add(new Course { Title = "Asp.net", Fees = 30000, IsActive = true });
unitOfWork.StudentRepository.Add(new Student { Name = "Jalaluddin", CGPA = 3 });
unitOfWork.StudentRepository.Add(new Student { Name = "Tareq", CGPA = 4 });

unitOfWork.Commit();

