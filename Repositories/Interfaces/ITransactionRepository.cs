using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Models;

namespace ABCBankSystem.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateAsync(Transaction transaction);

        IEnumerable<Transaction> GetAllTransactions();

        Task<Transaction> UpdateAsync(Transaction account);

        Task RemoveAsync(Guid transactionID);

        Transaction GetDetailsByID(Guid transactionID);

    }
}
