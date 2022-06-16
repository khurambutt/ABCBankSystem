using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Repositories.Interfaces;
using ABCBankSystem.Data;
using ABCBankSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ABCBankSystem.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private ApplicationDbContext _DbContext;

        public BankAccountRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<BankAccount> CreateAsync(BankAccount account)
        {
            //Insert CustomerAccount values in database.
            _DbContext.BankAccount.Add(account);
            await _DbContext.SaveChangesAsync();
            return account;
        }

        public async Task<BankAccount> UpdateAsync(BankAccount account)
        {
            //Insert CustomerAccount values in database.
            _DbContext.BankAccount.Update(account);
            await _DbContext.SaveChangesAsync();
            return account;
        }

        public async Task RemoveAsync(Guid bankAccountID)
        {
            BankAccount bankAccount = _DbContext.BankAccount.Find(bankAccountID);
            _DbContext.BankAccount.Remove(bankAccount);
            await _DbContext.SaveChangesAsync();
        }
        public async Task<BankAccount> DepositMoneyAsync(BankAccount account)
        {
            //Insert CustomerAccount values in database.
            _DbContext.BankAccount.Add(account);
            await _DbContext.SaveChangesAsync();
            return account;
        }

        public IEnumerable<BankAccount> GetAllBankAccounts()
        {
            return _DbContext.BankAccount.AsQueryable<BankAccount>().OrderByDescending(s => s.CreatedOn); ;
        }

        public  BankAccount GetBankAccountByID(Guid bankAccountID)
        {
            return _DbContext.BankAccount.Find(bankAccountID);
        }

        public BankAccount GetDetailsByID(Guid bankAccountID)
        {
            return _DbContext.BankAccount.Find(bankAccountID);
        }
    }
}
