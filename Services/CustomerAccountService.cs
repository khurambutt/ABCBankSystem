using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Services.Interfaces;
using ABCBankSystem.Repositories.Interfaces;
using ABCBankSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;


namespace ABCBankSystem.Services
{
    [Authorize]
    public class CustomerAccountService : ICustomerAccountService
    {
        private readonly ICustomerAccountRepository _customreRepository;

        public CustomerAccountService(ICustomerAccountRepository customerAccountRepository)
        {
            _customreRepository = customerAccountRepository;
        }

        public async Task<CustomerAccount> CreateAsync(CustomerAccount account, string userID)
        {
            account.ID = Guid.NewGuid();
            account.UserID = userID; // This is the ID of current loged in user to track his accounts.

            //Generate random number as customer account number.
            Random random = new Random();
            long number = random.Next(00000000, 99999999);

            account.AccountNumber = number;
            account.CreatedOn = DateTime.Now;
            await _customreRepository.CreateAsync(account);
            return account;
        }
    }
}
