using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Repositories;
using ABCBankSystem.Models;

namespace ABCBankSystem.Repositories.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> CreateAsync(BankAccount account);
        Task<BankAccount> DepositMoneyAsync(BankAccount account);

       IEnumerable<BankAccount> GetAllBankAccounts();

        BankAccount GetBankAccountByID(Guid bankAccountID);

        Task<BankAccount> UpdateAsync(BankAccount account);

        Task RemoveAsync(Guid bankAccountID);

        BankAccount GetDetailsByID(Guid bankAccountID);
        
    }
}
