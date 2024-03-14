using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
	public class CourseRepository
	{
		private SqlConnection connection;

		public CourseRepository() { }

		public CourseRepository(SqlConnection connection)
		{
			this.connection = connection;
		}

		public void Add(Course course)
		{

		}

		public void Remove(Course course) { }

		public Course Get(int id)
		{
			throw new NotImplementedException();
		}

		public Course this[int i]
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public void Update(Course course)
		{

		}
	}
}
