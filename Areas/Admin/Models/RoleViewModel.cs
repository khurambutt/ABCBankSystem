using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBankSystem.Areas.Admin.Models
{
    public class RoleViewModel
    {

        private string roleName;
        
        [Required]
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
    }
}
