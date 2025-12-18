using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daulatpride.Domain.Entities
{
    public class UserRegistration
    {
        public List<Registration> Registrationusers { get; set; }
        public List<CountryType> CountryTypes { get; set; }
        public List<StateType> StateTypes { get; set; }
        public List<CountryType> CountryType { get; set; }
    }
}
