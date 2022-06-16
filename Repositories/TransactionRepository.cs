using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Repositories.Interfaces;
using ABCBankSystem.Data;
using ABCBankSystem.Models;

namespace ABCBankSystem.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private ApplicationDbContext _DbContext;

        public TransactionRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            //Insert transactions values in database.
            _DbContext.Transaction.Add(transaction);
            await _DbContext.SaveChangesAsync();
            return transaction;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _DbContext.Transaction.AsQueryable<Transaction>().OrderByDescending(s => s.CreatedOnDate);
        }

        public async Task<Transaction> UpdateAsync(Transaction transaction)
        {
            //Insert CustomerAccount values in database.
            _DbContext.Transaction.Update(transaction);
            await _DbContext.SaveChangesAsync();
            return transaction;
        }

        public async Task RemoveAsync(Guid transactionID)
        {
            Transaction transaction = _DbContext.Transaction.Find(transactionID);
            _DbContext.Transaction.Remove(transaction);
            await _DbContext.SaveChangesAsync();
        }

        public Transaction GetDetailsByID(Guid transactionID)
        {
            return _DbContext.Transaction.Find(transactionID);
        }
    }
}
