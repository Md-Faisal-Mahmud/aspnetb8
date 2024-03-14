using FirstDemo.Application.Features.Training.Repositories;
using FirstDemo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Application.Features.Training.Services
{
    public class AccountService
    {
        private readonly IEmailService _emailService;
        private readonly IAccountRepository _accountRepository;

        public AccountService(IEmailService emailService, 
            IAccountRepository accountRepository)
        {
            _emailService = emailService;
            _accountRepository = accountRepository;
        }

        public void CreateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new InvalidDataException();

            if (username.Length > 50)
                username = username.Substring(0, 50);

            _accountRepository.CreateAccount(username, password);
        }
    }
}
