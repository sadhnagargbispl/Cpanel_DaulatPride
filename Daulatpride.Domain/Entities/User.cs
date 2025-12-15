using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daulatpride.Domain.Entities
{
    public class User
    {
    public string IDNo { get; set; }
        public long FormNo { get; set; }
        public string MemFirstName { get; set; }
        public string MemLastName { get; set; }
        public string Mobl { get; set; }
        public int KitID { get; set; }
        public string KitName { get; set; }

        public DateTime? Doj { get; set; }
        public DateTime? UpgradeDate { get; set; }

        public string Address1 { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }
        public string Panno { get; set; }

        public string ActStatus { get; set; }   // Active / Inactive
        public string ActivationDate { get; set; }

        public string Passw { get; set; }
        public string Epassw { get; set; }
        public bool IsActive { get; set; }

    }
}
