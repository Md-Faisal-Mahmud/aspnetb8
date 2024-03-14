using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
	public class Logger : ILogger
	{
		private static Logger instance;
		private Logger()
		{

		}

		public static ILogger GetLogger()
		{
			if (instance == null)
			{
				instance = new Logger();
			}

			return instance;
		}

		public void Log(string message)
		{
			Console.WriteLine(message);
		}
	}
}
