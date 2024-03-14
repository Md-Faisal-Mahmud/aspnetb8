using FirstDemo.Web.Data;
using Microsoft.Data.SqlClient;
using RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
	public class TrainingUnitOfWork
	{
		public CourseRepository CourseRepository { get; private set; }
		public StudentRepository StudentRepository { get; private set; }
		private SqlTransaction _transaction;
		private SqlConnection _connection;
		private AdoNetUtility _adoNetUtility;

		public TrainingUnitOfWork(string connectionString)
		{
			
			_connection = new SqlConnection(connectionString);
			_transaction = _connection.BeginTransaction();
			new CourseRepository(_connection);
		}

		public void Commit()
		{
			try
			{
				_transaction.Commit();
			}
			catch
			{
				Rollback();
			}
		}

		public void Rollback()
		{
			_transaction.Rollback();
		}
	}
}
