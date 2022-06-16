using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Models;
using ABCBankSystem.ViewModels.BankAccount;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ABCBankSystem.Services.Interfaces
{
    public interface IBankAccountService
    {
        Task<BankAccount> CreateAsync(BankAccount account, string userID, string userName);
        IEnumerable<BankAccount> GetBankAccounts();
        BankAccount GetBankAccountByID(Guid bankAccountID);

        Task<BankAccount> UpdateAsync(BankAccount account);

        Task RemoveAsync(Guid bankAccountID);

        public List<SelectListItem> LoadBankAccountsDDL();

        public List<SelectListItem> LoadDestinationBankAccountsDDL();
        Task<BankAccount> DepositMoneyAsync(BankAccountViewModel account);

        Task<BankAccount> WithdrawlMoneyAsync(BankAccountViewModel account);

        Task<BankAccount> TransferOrDirectDebitSourceAsync(BankAccountViewModel bankAccountViewModel);

        Task<BankAccount> TransferOrDirectDebitDestinationAsync(BankAccountViewModel bankAccountViewModel);

        Task<BankAccount> OverdraftChargesAsync(BankAccountViewModel bankAccountViewModel);

        public string getBankAccountString(Guid id);

    }
}
