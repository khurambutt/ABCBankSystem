using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankSystem.Services.Interfaces;
using ABCBankSystem.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace ABCBankSystem.Controllers
{
    // Added this attribute to make Authorization at controller level. 
    [Authorize]
    public class CustomerAccountController : Controller
    {
        private readonly ICustomerAccountService _customerAccountService;

        public CustomerAccountController(ICustomerAccountService customerAccountService)
        {
            _customerAccountService = customerAccountService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerAccount account)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    //Get the registered user id of the current login.  
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    
                    //create a new bank account for the current user.
                    await _customerAccountService.CreateAsync(account, userId);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                //Error Message.
                ModelState.AddModelError("", "Bank account not created succecfully. Please try again!" );
            }
            return View(account);
        }
    }
}
