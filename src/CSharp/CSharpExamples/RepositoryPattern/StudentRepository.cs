using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
	public class StudentRepository
	{
		public StudentRepository(SqlConnection connection) { }

		public void Add(Student student) { }
	}
}
