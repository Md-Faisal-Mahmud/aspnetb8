using FirstDemo.Application.Features.Training.Services;
using FirstDemo.Domain.Services;
using FirstDemo.Infrastructure.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FirstDemo.Infrastructure.Features.Services
{
    public class EmailMessageService : IEmailMessageService
    {
        private readonly IEmailService _emailService;

        public EmailMessageService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendEmailConfirmationEmailAsync(string receiverEmail, 
            string receiverName, string confirmationLink)
        {
            var template = new EmailConfirmationTemplate(receiverName,
                HtmlEncoder.Default.Encode(confirmationLink));

            string body = template.TransformText();
            string subject = "Please confirm your email";

            _emailService.SendSingleEmail(receiverName, receiverEmail, subject, body);
        }
    }
}
