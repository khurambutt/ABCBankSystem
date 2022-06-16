using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Repositories.Interfaces;
using ABCBankSystem.Data;
using ABCBankSystem.Models;

namespace ABCBankSystem.Repositories
{
    public class CustomerAccountRepository : ICustomerAccountRepository
    {
        private ApplicationDbContext _DbContext;

        public CustomerAccountRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<CustomerAccount> CreateAsync(CustomerAccount account)
        {
            //Insert CustomerAccount values in database.
            _DbContext.CustomerAccount.Add(account);
            await _DbContext.SaveChangesAsync();
            return account;
        }
    }
}
