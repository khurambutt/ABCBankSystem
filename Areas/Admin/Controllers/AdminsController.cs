using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ABCBankSystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace ABCBankSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminsController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateNewRole()
        {
            RoleViewModel rl = new RoleViewModel();
            return View(rl);
        }
    }
}
