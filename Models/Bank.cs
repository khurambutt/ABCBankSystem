using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBankSystem.Models
{
    public class Bank
    {
        private Guid id;
        private string name;
        private string branchCode;
        private string address;

        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string BranchCode
        {
            get { return branchCode; }
            set { branchCode = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }
}
