using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ABCBankSystem.Models
{
    public class BankAccount
    {
        private Guid id;
        private string userID;
        private string name;
        private long accountNumber;
        private decimal balance;
        private string logedInUserName;
        private string address;
        private string details;
        private DateTime createdOn;
        private DateTime updateOn;
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

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Display(Name = "Account Number")]
        public long AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        [Required]
        [Display(Name="Balance")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(1, int.MaxValue, ErrorMessage = "Enter balance in greater than 0.")]
        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public string LogedInUserName
        {
            get { return logedInUserName; }
            set { logedInUserName = value; }
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

        [Display(Name = "Date")]
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }

        [DisplayFormat(DataFormatString = "{yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime UpdateOn
        {
            get { return updateOn; }
            set { updateOn = value; }
        }
    }
}
