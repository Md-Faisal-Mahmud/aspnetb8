using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Application.Features.Training.Services
{
    public interface IEmailMessageService
    {
        Task SendEmailConfirmationEmailAsync(string receiverEmail, string receiverName,
            string confirmationPage);
    }
}
