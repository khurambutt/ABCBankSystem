using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Models;

namespace ABCBankSystem.Services.Interfaces
{
    public interface ICustomerAccountService
    {
        Task<CustomerAccount> CreateAsync(CustomerAccount account, string userID);
    }
}
