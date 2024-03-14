using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.SRP
{
	class EmailUitlity
	{
		public void SendSingleEmail(string receiver, string subject, string body)
		{
			var random = GenerateRandomNumber();
		}

		public void SendBulkEmail(string[] receivers, string subject, string body)
		{
			foreach (var receiver in receivers)
			{
				try
				{
					SendSingleEmail(receiver, subject, body);

				}
				catch (Exception ex)
				{

				}
			}
		}

		public void SendHtmlEmail(string receiver, string subject, string body)
		{

		}

		public string GenerateRandomNumber()
		{
			throw new NotImplementedException();
		}

		public void LogError()
		{

		}
	}
}
