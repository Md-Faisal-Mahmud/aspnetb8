// See https://aka.ms/new-console-template for more information

using RepositoryPattern;

CourseRepository courseRepository = new CourseRepository();

var course = new Course()
{
	Title = "C#",
	Fees = 8000,
	IsActive = true,
	ClassStartDate = new DateTime(2022, 2, 2)
};

courseRepository.Add(course);

Course find = courseRepository.Get(1);

courseRepository.Remove(find);

courseRepository[0].Fees = 9000;



