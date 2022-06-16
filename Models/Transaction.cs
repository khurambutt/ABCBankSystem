using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ABCBankSystem.Models
{
    public class Transaction
    {
        private Guid id;
        private Guid userID;
        private Guid bankAccountID;
        private long bankAccountNumber;
        private long fromAccount;
        private long toAccount;
        private string transactionType;
        private decimal balance;
        private string details;
        private DateTime createdOnDate;

        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }

        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public Guid BankAccountID
        {
            get { return bankAccountID; }
            set { bankAccountID = value; }
        }
        [Display(Name = "Account Number" )]
        public long BankAccountNumber
        {
            get { return bankAccountNumber; }
            set { bankAccountNumber = value; }
        }
        [Display(Name = "From")]
        public long FromAccount
        {
            get { return fromAccount; }
            set { fromAccount = value; }
        }

        [Display(Name = "To")]
        public long ToAccount
        {
            get { return toAccount; }
            set { toAccount = value; }
        }
        [Display(Name = "Type")]
        public string TransactionType
        {
            get { return transactionType; }
            set { transactionType = value; }

        }

        [Display(Name = "Balance")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }


        public string Details
        {
            get { return details; }
            set { details = value; }
        }

        [Display(Name = "Date")]
        public DateTime CreatedOnDate
        {
            get { return createdOnDate; }
            set { createdOnDate = value; }
        }

        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public ICollection<BankAccount> BankAccounts { get; set; }
    }
}
