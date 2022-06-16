using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Services;
using ABCBankSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ABCBankSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IBankAccountService _bankAccountService;
        private readonly ITransactionService _transactionService;

        public HomeController(IBankAccountService bankAccountService, ITransactionService transactionService)
        {
            _bankAccountService = bankAccountService;
            _transactionService = transactionService;
        }

        public IActionResult Index()
        {

            var bankAccounts = _bankAccountService.GetBankAccounts();
            return View(bankAccounts);
        }

        public IActionResult CheckAreaAd()
        {
            return View();
        }


    }
}
