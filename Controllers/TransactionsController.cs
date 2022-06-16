using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ABCBankSystem.Data;
using ABCBankSystem.Models;
using ABCBankSystem.ViewModels.BankAccount;
using ABCBankSystem.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ABCBankSystem.Common.Enums;

namespace ABCBankSystem.Controllers
{
    /// <summary>
    /// This calass is create to maintian all transactions occur in the system.
    /// </summary>
    
    //Apply authorization to whole controller
    [Authorize]
    public class TransactionsController : Controller
    {
        //decleration for interface vairable
        private readonly ITransactionService _transactionService;

        //resolve DI 
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: All Transactions
        public IActionResult Index()
        {
            var transactions = _transactionService.GetAllTransactions();// Load all transactions in the system.
            return View(transactions);
        }

        //Edit a transaction
        public IActionResult Edit(Guid id)
        {
            //get transaction by its id.
            var transactions = _transactionService.GetTransactionByID(id);

            return View(transactions);
        }

        //show all details of the transaction.
        public IActionResult Details(Guid id)
        {
            //load a transaction details by its id.
            var bankAccount = _transactionService.GetTransactionByID(id);
            return View(bankAccount);
        }

        //post transaction data to update its values.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Transaction transaction)
        {
            if (id != transaction.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //update transaction data.
                    await _transactionService.UpdateAsync(transaction);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // Delete a transaction 
        public IActionResult Delete(Guid id)
        {
            // delete a transaction entry by its id.
            var transaction = _transactionService.GetTransactionByID(id);
            return View(transaction);
        }

        // POST: BankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid ID)
        {
            //Remove transaction form database.
            await _transactionService.RemoveAsync(ID);

            return RedirectToAction(nameof(Index));
        }
    }
}
