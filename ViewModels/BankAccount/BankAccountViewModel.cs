using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ABCBankSystem.ViewModels.BankAccount
{
     public class BankAccountViewModel

    {
        private Guid id;
        private string userID;
        private Guid toID;
        private string name;
        private long accountNumber;
        private decimal balance;
        private string address;
        private string details;
        private int overDraftDays;
        private DateTime createdOn;

        [DisplayName("Bank Account")]
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public Guid ToID
        {
            get { return toID; }
            set { toID = value; }
        }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public long AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        [Required]
        [Display(Name = "Balance")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(1, int.MaxValue, ErrorMessage = "Enter balance in greater than 0.")]
        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        [Required]
        [StringLength(60)]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Details
        {
            get { return details; }
            set { details = value; }
        }

        public int OverDraftDays
        {
            get { return overDraftDays; }
            set { overDraftDays = value; }
        }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }

        //Load all bank accounts
        public List<SelectListItem> ListofBankAccounts { get; set; }

        //Load all bank accounts
        public List<SelectListItem> ListofDestinationBankAccounts { get; set; }
    }
}

