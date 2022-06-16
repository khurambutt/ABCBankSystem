using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Services.Interfaces;
using ABCBankSystem.Repositories.Interfaces;
using ABCBankSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ABCBankSystem.ViewModels.BankAccount;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace ABCBankSystem.Services
{
    [Authorize]
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankRepository;

        public BankAccountService(IBankAccountRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<BankAccount> CreateAsync(BankAccount account, string userID, string userName)
        {
            account.ID = Guid.NewGuid();
            account.UserID = userID; // This is the ID of current loged in user to track his accounts.

            //Generate random number as customer account number.
            Random random = new Random();
            long number = random.Next(00000000, 99999999);

            account.LogedInUserName = userName;
            account.AccountNumber = number;
            account.CreatedOn = DateTime.Now;
            await _bankRepository.CreateAsync(account);
            return account;
        }

        public  IEnumerable<BankAccount> GetBankAccounts()
        {
            return _bankRepository.GetAllBankAccounts();
        }

        public BankAccount GetBankAccountByID(Guid bankAccountID)
        {
            return _bankRepository.GetBankAccountByID(bankAccountID);
        }

        public async Task<BankAccount> UpdateAsync(BankAccount account)
        {
            account.UpdateOn = DateTime.Now;
            await _bankRepository.UpdateAsync(account);
            return account;
        }

        public async Task RemoveAsync(Guid bankAccountID)
        {
            await _bankRepository.RemoveAsync(bankAccountID);
            
        }

        public List<SelectListItem> LoadBankAccountsDDL()
        {
            var bankAccounts = _bankRepository.GetAllBankAccounts();

            var bankAccountsList = (from bankAccount in bankAccounts
                                    select new SelectListItem()
                                    {
                                        Text = bankAccount.Name + " ---  £" + bankAccount.Balance,
                                        Value = bankAccount.ID.ToString(),
                                    }).ToList();

            bankAccountsList.Insert(0, new SelectListItem()
            {
                Text = "Select Source Account",
                Value = string.Empty
            });

            return bankAccountsList;
        }

        public List<SelectListItem> LoadDestinationBankAccountsDDL()
        {
            var bankAccounts = _bankRepository.GetAllBankAccounts();

            var bankAccountsList = (from bankAccount in bankAccounts
                                    select new SelectListItem()
                                    {
                                        Text = bankAccount.Name + " ---  £" + bankAccount.Balance,
                                        Value = bankAccount.ID.ToString(),
                                    }).ToList();

            bankAccountsList.Insert(0, new SelectListItem()
            {
                Text = "Select Destination Account",
                Value = string.Empty
            });

            return bankAccountsList;
        }

        public async Task<BankAccount> DepositMoneyAsync(BankAccountViewModel bankAccountViewModel)
        {
            BankAccount bankAccount = _bankRepository.GetBankAccountByID(bankAccountViewModel.ID);
            bankAccount.Balance = bankAccount.Balance + bankAccountViewModel.Balance;
            bankAccount.Details = bankAccountViewModel.Details;
            bankAccount.UpdateOn = DateTime.Now;
            return await _bankRepository.UpdateAsync(bankAccount);
        }

        public async Task<BankAccount> WithdrawlMoneyAsync(BankAccountViewModel bankAccountViewModel)
        {
            BankAccount bankAccount = _bankRepository.GetBankAccountByID(bankAccountViewModel.ID);
            bankAccount.Balance = bankAccount.Balance - bankAccountViewModel.Balance; /// apply conditions for to check if it less than 0
            bankAccount.Details = bankAccountViewModel.Details;
            bankAccount.UpdateOn = DateTime.Now;
            return await _bankRepository.UpdateAsync(bankAccount);
        }

        public async Task<BankAccount> TransferOrDirectDebitSourceAsync(BankAccountViewModel bankAccountViewModel)
        {
            BankAccount bankAccount = _bankRepository.GetBankAccountByID(bankAccountViewModel.ID);
            bankAccount.Balance = bankAccount.Balance - bankAccountViewModel.Balance; /// apply conditions for to check if it less than 0
            bankAccount.Details = bankAccountViewModel.Details;
            bankAccount.UpdateOn = DateTime.Now;
            return await _bankRepository.UpdateAsync(bankAccount);
        }

        public async Task<BankAccount> TransferOrDirectDebitDestinationAsync(BankAccountViewModel bankAccountViewModel)
        {
            BankAccount bankAccount = _bankRepository.GetBankAccountByID(bankAccountViewModel.ToID);
            bankAccount.Balance = bankAccount.Balance + bankAccountViewModel.Balance; /// apply conditions for to check if it less than 0
            bankAccount.Details = bankAccountViewModel.Details;
            bankAccount.UpdateOn = DateTime.Now;
            return await _bankRepository.UpdateAsync(bankAccount);
        }

        public async Task<BankAccount> OverdraftChargesAsync(BankAccountViewModel bankAccountViewModel)
        {
            BankAccount bankAccount = _bankRepository.GetBankAccountByID(bankAccountViewModel.ID);
            
            Decimal draftFees;
            //19 % CHARGES
            draftFees = bankAccountViewModel.Balance * (19M / 100M) / (decimal)bankAccountViewModel.OverDraftDays;

            Decimal roundedoverdraftCharges = Math.Round(draftFees, 3);

            bankAccount.Balance = bankAccount.Balance + roundedoverdraftCharges; /// apply conditions for to check if it less than 0
            bankAccount.Details = bankAccountViewModel.Details;
            bankAccount.UpdateOn = DateTime.Now;
            return await _bankRepository.UpdateAsync(bankAccount);
        }

        public string getBankAccountString1(Guid id)
        {
            BankAccount bankAccount = _bankRepository.GetBankAccountByID(id);
            //var employees = DataStorage.GetAllEmployess();
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Name</th>
                                        <th>LastName</th>
                                        <th>Age</th>
                                        <th>Gender</th>
                                    </tr>");
            //foreach (var bac in bankAccount)
            //{
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", bankAccount.Name, bankAccount.Balance, bankAccount.Address, bankAccount.CreatedOn);
            //}
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }

        public string getBankAccountString(Guid id)
        {
            BankAccount bankAccount = _bankRepository.GetBankAccountByID(id);
            //var employees = DataStorage.GetAllEmployess();
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    ");
            //foreach (var bac in bankAccount)
            //{
            sb.AppendFormat(@"<tr>
                                    <td>Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<td>
                                    <td>{0}</td>
                                  </tr>", bankAccount.Name);

            sb.AppendFormat(@"<tr>

                                    <td>Balance:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<td>
                                    <td>{0}</td>
                                  </tr>", bankAccount.Balance);

            sb.AppendFormat(@"<tr>
                                     <td>Address:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<td>
                                    <td>{0}</td>
                                  </tr>", bankAccount.Address);
            
            sb.AppendFormat(@"<tr>
                                     <td>CreatedOn:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<td>
                                    <td>{0}</td>
                                  </tr>", bankAccount.CreatedOn);

            //}
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}
