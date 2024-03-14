using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
	public static class SqlProducer
	{
		public static (string sql, List<DbParameter> parameters) GetInsertSql(object item)
		{
			throw new NotImplementedException();
		}

		public static (string sql, List<DbParameter> parameters) GetUpdateSql(object item)
		{
			throw new NotImplementedException();
		}

		public static (string sql, List<DbParameter> parameters) GetDeleteSql(object item)
		{
			throw new NotImplementedException();
		}

		public static (string sql, List<DbParameter> parameters) GetCleanSql(object item)
		{
			throw new NotImplementedException();
		}
	}
}
