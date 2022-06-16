using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Repositories;
using ABCBankSystem.Models;

namespace ABCBankSystem.Repositories.Interfaces
{
    public interface ICustomerAccountRepository
    {
        Task<CustomerAccount> CreateAsync(CustomerAccount account);
    }
}
