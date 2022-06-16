using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ABCBankSystem.Models
{
    public class CustomerAccount
    {
        private Guid id;
        private string userID;
        private string name;
        private long accountNumber;
        private string address;
        private DateTime createdOn;
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
        public long AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        [Required]
        [StringLength(60)]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }
    }
}
