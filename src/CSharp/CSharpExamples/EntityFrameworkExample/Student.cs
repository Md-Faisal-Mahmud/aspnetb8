using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample
{
	public class Student
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double CGPA { get; set; }
		public List<CourseStudent> Courses { get; set; }
	}
}
