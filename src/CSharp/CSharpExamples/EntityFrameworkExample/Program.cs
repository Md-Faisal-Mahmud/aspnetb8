// See https://aka.ms/new-console-template for more information


using EntityFrameworkExample;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
var context = new TrainingDbContext();

/*
Course course = new Course();
course.ClassStartDate = DateTime.Now;
course.Title = "Asp.net";
course.Price = 30000;
course.IsActive = true;
course.Topics = new List<Topic>();
course.Topics.Add(new Topic() { Name = "Getting Started", Duration = 30 });
course.Topics.Add(new Topic() { Name = "Tool Installation", Duration = 40 });


context.Courses.Add(course);
context.SaveChanges();
*/
/*
var c = context.Courses.Where(x => x.Id == 1).Include(x => x.Topics).FirstOrDefault();
Console.WriteLine(c.Title);

foreach(var topic in c.Topics)
{
	Console.WriteLine(topic.Name);
	topic.Name = $"-{topic.Name}-";
}

c.Topics.Add(new Topic { Name = "Basic Syntax", Duration = 20 });
context.SaveChanges();
*/

var c2 = new Course 
{ 
	Title = "C#", 
	Price = 8000, 
	IsActive = true, 
	ClassStartDate = DateTime.Now,
	Topics = new List<Topic>
	{
		new Topic
		{
			Name = "Variables",
			Duration = 20,
		},
		new Topic
		{
			Name = "Conditions",
			Duration = 10,
		},
	},
	Students = new List<CourseStudent>
	{
		new CourseStudent
		{
			Student = new Student { Name = "jalaluddin", CGPA = 3.2}
		},
		new CourseStudent
		{
			Student = new Student { Name = "tareq", CGPA = 2.2}
		}
	}
};

context.Courses.Add(c2);
context.SaveChanges();
