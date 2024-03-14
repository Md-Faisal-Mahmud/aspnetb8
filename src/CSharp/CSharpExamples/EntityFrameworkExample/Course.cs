using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample
{
	public class Course
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public double Price { get; set; }
		public bool IsActive { get; set; }
		public DateTime ClassStartDate { get; set; }
		public List<Topic> Topics { get; set; }
		public List<CourseStudent> Students { get; set; }
	}
}
