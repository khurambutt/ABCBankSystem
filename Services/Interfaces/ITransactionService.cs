using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Models;
using ABCBankSystem.ViewModels.BankAccount;

namespace ABCBankSystem.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> CreateAsync(BankAccountViewModel bankAccountViewModel,  string transactionType);

        Task<Transaction> CreateDirectTransfersAsync(BankAccountViewModel bankAccountViewModel, string transactionType, Guid userID);

        IEnumerable<Transaction> GetAllTransactions();

        Task<Transaction> CreateOverdraftChargesAsync(BankAccountViewModel bankAccountViewModel, string transactionType);

        Transaction GetTransactionByID(Guid transactionID);

        Task<Transaction> UpdateAsync(Transaction account);

        Task RemoveAsync(Guid transactionID);
    }

   
}
