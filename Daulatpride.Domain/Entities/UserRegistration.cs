using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daulatpride.Domain.Entities
{
    public class UserRegistration
    {
        public int SponsorId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public int CountryId { get; set; }
        public string StdCode { get; set; }
        public string Mobile { get; set; }
        public int StateCode { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public string PanNo { get; set; }
    }
}
