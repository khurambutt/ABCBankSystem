using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Services.Interfaces;
using ABCBankSystem.Repositories.Interfaces;
using ABCBankSystem.Models;
using ABCBankSystem.ViewModels.BankAccount;
using ABCBankSystem.Common.Enums;

namespace ABCBankSystem.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBankAccountRepository _bankAccountRepository;

        public TransactionService(ITransactionRepository transactionRepository, IBankAccountRepository bankAccountRepository)
        {
            _transactionRepository = transactionRepository;
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<Transaction> CreateAsync(BankAccountViewModel bankAccountViewModel, string transactionType)
        {
            BankAccount bankAccount = _bankAccountRepository.GetBankAccountByID(bankAccountViewModel.ID);

            Transaction transaction = new Transaction();
            transaction.ID = Guid.NewGuid();
            transaction.UserID = Guid.Parse(bankAccount.UserID); // This is the ID of current loged in user to track his accounts.
            transaction.BankAccountID = bankAccount.ID;
            transaction.BankAccountNumber = bankAccount.AccountNumber;
            transaction.CreatedOnDate = DateTime.Now;
            transaction.Balance = bankAccountViewModel.Balance;
            transaction.Details = bankAccountViewModel.Details;
            transaction.TransactionType = transactionType;
            transaction.FromAccount = bankAccount.AccountNumber;
            transaction.ToAccount = bankAccount.AccountNumber;
            return await _transactionRepository.CreateAsync(transaction);
            
        }

        public Transaction GetTransactionByID(Guid transactionID)
        {
            return _transactionRepository.GetDetailsByID(transactionID);
        }

        public async Task<Transaction> UpdateAsync(Transaction transaction)
        {
            transaction.CreatedOnDate = DateTime.Now;
            await _transactionRepository.UpdateAsync(transaction);
            return transaction;
        }
        
        public async Task RemoveAsync(Guid transactionID)
        {
            await _transactionRepository.RemoveAsync(transactionID);

        }

        public async Task<Transaction> CreateOverdraftChargesAsync(BankAccountViewModel bankAccountViewModel, string transactionType)
        {
            BankAccount bankAccount = _bankAccountRepository.GetBankAccountByID(bankAccountViewModel.ID);

            Transaction transaction = new Transaction();
            transaction.ID = Guid.NewGuid();
            transaction.UserID = Guid.Parse(bankAccount.UserID); // This is the ID of current loged in user to track his accounts.
            transaction.BankAccountID = bankAccount.ID;
            transaction.BankAccountNumber = bankAccount.AccountNumber;
            transaction.CreatedOnDate = DateTime.Now;

            decimal overdraftCharges; 
            //overdraft charges is = 19%
            overdraftCharges = bankAccountViewModel.Balance * (19M/ 100M) / (decimal)bankAccountViewModel.OverDraftDays;
           
            await _bankAccountRepository.UpdateAsync(bankAccount);
            transaction.Balance = bankAccountViewModel.Balance;
            transaction.Details = bankAccountViewModel.Details;
            transaction.TransactionType = transactionType;
            transaction.FromAccount = bankAccount.AccountNumber;
            transaction.ToAccount = bankAccount.AccountNumber;
            return await _transactionRepository.CreateAsync(transaction);

        }
        public async Task<Transaction> CreateDirectTransfersAsync(BankAccountViewModel bankAccountViewModel, string transactionType, Guid userIID)
        {
            BankAccount bankAccountSource = _bankAccountRepository.GetBankAccountByID(bankAccountViewModel.ID);
            BankAccount bankAccountDestination = _bankAccountRepository.GetBankAccountByID(bankAccountViewModel.ToID);

            Transaction transaction = new Transaction();
            transaction.ID = Guid.NewGuid();
            transaction.UserID = userIID; // This is the ID of current loged in user to track his accounts.
            transaction.CreatedOnDate = DateTime.Now;
            transaction.Balance = bankAccountViewModel.Balance;
            transaction.Details = bankAccountViewModel.Details;
            transaction.TransactionType = transactionType;
            transaction.FromAccount = bankAccountSource.AccountNumber;
            transaction.ToAccount = bankAccountDestination.AccountNumber;
            return await _transactionRepository.CreateAsync(transaction);
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _transactionRepository.GetAllTransactions();
        }
    }
}
