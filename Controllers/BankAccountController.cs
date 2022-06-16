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
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using System.Text;

/// <summary>
/// This purpose of this class is to crtate the bank accounts of the customer. Like customer can edit , delete and can see the details of their accounts.
/// </summary>
/// 
namespace ABCBankSystem.Controllers
{
    //By using authorize attribute only authorize and logged in user can access this.
    [Authorize]
    public class BankAccountController : Controller
    {
        // private variables of itnerfaces
        private readonly IBankAccountService _bankAccountService;
        private readonly ITransactionService _transactionService;
        private IConverter _converter;

        public BankAccountController(IBankAccountService bankAccountService, ITransactionService transactionService, IConverter converter)
        {
            _bankAccountService = bankAccountService;
            _transactionService = transactionService;
            _converter = converter;
        }
            
        // Index is used to return all the accounts to the coantroller.
        // Get BankAccount/Index
        public IActionResult Index()
        {
            var bankAccounts = _bankAccountService.GetBankAccounts(); // Load all accounts
            return View(bankAccounts);
        }

        // GET: BankAccount/Create
        public IActionResult Create()
        {
            // return create view
            return View();
        }
        
        // Post Data: to insert in database to create customer account.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankAccount bankAccount)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Get the registered user id and nameof the current login.  
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var userName = this.User.FindFirstValue(ClaimTypes.Name);

                    //create a new bank account for the current user.
                    await _bankAccountService.CreateAsync(bankAccount, userId, userName);
                    
                    //return to Main index page.
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                //Error Message.
                ModelState.AddModelError("", "Bank account not created succecfully. Please try again!");
            }

            return View(bankAccount);
        }

         //GET: BankAccount/Edit/5
        public IActionResult Edit(Guid id)
        {
           var bankAccount = _bankAccountService.GetBankAccountByID(id); //Load account by account id.
            return View(bankAccount);
        }
        [HttpGet]
        //[Route("/pdfcreator")]
        public IActionResult CreatePdf(Guid id)
        {
            var bankAccount = _bankAccountService.GetBankAccountByID(id); //Load account by account id.
                                                                          ////////
            ///
             

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                //Out = @"E:\PDFCreator\Employee_Report.pdf"
                //Out = @"C:\PDFCreator\BankAccountReport.pdf"


            };


            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = _bankAccountService.getBankAccountString(id),///TemplateGenerator.GetHTMLString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "style.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);
            //return Ok("Successfully created PDF document.");
            return File(file, "application/pdf", "BankAccountReport.pdf");
            ///////
            //return View(bankAccount);
        }



        // GET: BankAccount/Details/5
        public  IActionResult Details(Guid id)
        {
            var bankAccount = _bankAccountService.GetBankAccountByID(id); // Load bank details by bank account id.

            // return view
            return View(bankAccount);
        }

        // Post data: to submit update details.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult >Edit(Guid id, BankAccount bankAccount)
        {
           if (ModelState.IsValid)
            {
                //update account details and call update method.
                await _bankAccountService.UpdateAsync(bankAccount);
                return RedirectToAction(nameof(Index));
            }
            return View(bankAccount);
        }

        //Deposit money 
        public IActionResult DepositMoney()
        {
            BankAccountViewModel bankAccountViewModel = new BankAccountViewModel();

            //Load all bank accounts to populate drop down list.
            bankAccountViewModel.ListofBankAccounts = _bankAccountService.LoadBankAccountsDDL();

            return View(bankAccountViewModel);
        }
       
        // Withdraw money from account.
        public IActionResult WithdrawlMoney()
         {
            BankAccountViewModel bankAccountViewModel = new BankAccountViewModel();

            //Load all bank accounts to populate drop down list. 
            bankAccountViewModel.ListofBankAccounts = _bankAccountService.LoadBankAccountsDDL();

            return View(bankAccountViewModel);
        }

        //Transfer money from one account to another account or dorect debits.
        public IActionResult TransferOrDirectDebit()
        {
            BankAccountViewModel bankAccountViewModel = new BankAccountViewModel();
            
            //Load all bank accounts to populate source drop down list. 
            bankAccountViewModel.ListofBankAccounts = _bankAccountService.LoadBankAccountsDDL();

            //Load all bank accounts to populate destination drop down list. 
            bankAccountViewModel.ListofDestinationBankAccounts = _bankAccountService.LoadDestinationBankAccountsDDL();

            return View(bankAccountViewModel);
        }

        //Overdrat debits and charges
        public IActionResult OverdraftCharges()
        {
            BankAccountViewModel bankAccountViewModel = new BankAccountViewModel();

            //Load all bank accounts to populate drop down list. 
            bankAccountViewModel.ListofBankAccounts = _bankAccountService.LoadBankAccountsDDL();

            return View(bankAccountViewModel);
        }

        //Post deposit money data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DepositMoney(BankAccountViewModel bankAccountViewModel)
        {
            //update customer bank account details and balance
            await _bankAccountService.DepositMoneyAsync(bankAccountViewModel);

            //create transaction and add in transactions table.
            await _transactionService.CreateAsync(bankAccountViewModel, TransactionTypes.Deposit.ToString());

            // return to details.
            return RedirectToAction(nameof(Index));
        }

        //Post data to save withdrawl money
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WithdrawlMoney(BankAccountViewModel bankAccountViewModel)
        {
            BankAccount bankAccount = _bankAccountService.GetBankAccountByID(bankAccountViewModel.ID);
            if(bankAccountViewModel.Balance > bankAccount.Balance)
            {
                ViewBag.ErrorMessage = "Withdrawl balance should be less than your current balance.";
                
                //Load all bank accounts to populate drop down list. 
                bankAccountViewModel.ListofBankAccounts = _bankAccountService.LoadBankAccountsDDL();

                return this.View(bankAccountViewModel);
            }
            //Add latest values to record withdrawl money.
            await _bankAccountService.WithdrawlMoneyAsync(bankAccountViewModel);

            //add values into transaction table.
            await _transactionService.CreateAsync(bankAccountViewModel, TransactionTypes.Withdrawl.ToString());

            return RedirectToAction(nameof(Index));
        }

        //Post data for Transfer money or for direct debits. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransferOrDirectDebit(BankAccountViewModel bankAccountViewModel)
        {
            BankAccount sourcebankAccount = _bankAccountService.GetBankAccountByID(bankAccountViewModel.ID);
            //BankAccount destinatonbankAccount = _bankAccountService.GetBankAccountByID(bankAccountViewModel.ToID);

            if (bankAccountViewModel.ID == bankAccountViewModel.ToID)
            {
                ViewBag.ErrorMessage = "Destination Account should be different from Source Account.";

                //Load all bank accounts to populate source drop down list. 
                bankAccountViewModel.ListofBankAccounts = _bankAccountService.LoadBankAccountsDDL();

                //Load all bank accounts to populate destination drop down list. 
                bankAccountViewModel.ListofDestinationBankAccounts = _bankAccountService.LoadDestinationBankAccountsDDL();

                return this.View(bankAccountViewModel);
            }
            else if (bankAccountViewModel.Balance > sourcebankAccount.Balance)
            {
                ViewBag.ErrorMessage = "Balance should be less than from Source Account Balance";

                //Load all bank accounts to populate source drop down list. 
                bankAccountViewModel.ListofBankAccounts = _bankAccountService.LoadBankAccountsDDL();

                //Load all bank accounts to populate destination drop down list. 
                bankAccountViewModel.ListofDestinationBankAccounts = _bankAccountService.LoadDestinationBankAccountsDDL();

                return this.View(bankAccountViewModel);
            }
            
            //get ligged in user ID.
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //update source account 
            await _bankAccountService.TransferOrDirectDebitSourceAsync(bankAccountViewModel);

            //update destination account
            await _bankAccountService.TransferOrDirectDebitDestinationAsync(bankAccountViewModel);

            //add values into transaction table.
            await _transactionService.CreateDirectTransfersAsync(bankAccountViewModel, TransactionTypes.DirectTransfer.ToString(), Guid.Parse(userId));

            return RedirectToAction(nameof(Index));
        }

        //Post overdraft values
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OverdraftCharges(BankAccountViewModel bankAccountViewModel)
        {
            //add overdraft balance and charges
            await _bankAccountService.OverdraftChargesAsync(bankAccountViewModel);

            //add into transaction tabe as debit.
            await _transactionService.CreateOverdraftChargesAsync(bankAccountViewModel, TransactionTypes.OverdraftDebit.ToString());
            return RedirectToAction(nameof(Index));
        }

        //delete bank acount
        public IActionResult Delete(Guid id)
        {
            var bankAccount = _bankAccountService.GetBankAccountByID(id);// Load bank account by id.

            return View(bankAccount);
        }

        // POST: BankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid ID)
        {
                await _bankAccountService.RemoveAsync(ID);
                return RedirectToAction(nameof(Index));
        }
    }
}
