using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daulatpride.Domain.Entities
{
    public class WelcomeViewModel
    {
        public string MemberName { get; set; }
        public string MemberId { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Password { get; set; }
        public string TransactionPassword { get; set; }
    }

}
