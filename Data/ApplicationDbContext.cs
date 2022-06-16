using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ABCBankSystem.Models;

namespace ABCBankSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ABCBankSystem.Models.CustomerAccount> CustomerAccount { get; set; }
        public DbSet<ABCBankSystem.Models.BankAccount> BankAccount { get; set; }
        public DbSet<ABCBankSystem.Models.Transaction> Transaction { get; set; }
    }
}
