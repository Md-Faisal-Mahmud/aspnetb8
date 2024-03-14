using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
	public class Course
	{
		public string Title { get; set; }
		public double Fees { get; set; }
		public bool IsActive { get; set; }
		public DateTime ClassStartDate { get; set; }
	}
}
