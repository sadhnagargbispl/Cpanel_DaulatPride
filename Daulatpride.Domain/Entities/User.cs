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
        public string Formno { get; set; }

        public string MemFirstName { get; set; }
        public string MemLastName { get; set; }

        public string Mobl { get; set; }
        public int KitID { get; set; }
        public string KitName { get; set; }
        public string fld3 { get; set; }

        public DateTime? Doj { get; set; }
        public DateTime? UpgradeDate { get; set; }

        public string Address1 { get; set; }
        public string Fld5 { get; set; }
        public string ActiveStatus { get; set; }

        public string MID { get; set; }
        public string Email { get; set; }
        public string profilepic { get; set; }
        public string Panno { get; set; }
        public string MemPassw { get; set; }
        public string MemEPassw { get; set; }
    }

    public class MemberInfo
    {
        public int Run { get; set; }
        public string Status { get; set; }
        public string IDNo { get; set; }
        public string FormNo { get; set; }
        public string MemName { get; set; }
        public string MobileNo { get; set; }
        public int MemKit { get; set; }
        public string Package { get; set; }
        public string Position { get; set; }
        public DateTime? Doj { get; set; }
        public string DOA { get; set; }
        public string Address { get; set; }
        public string IsFranchise { get; set; }
        public string ActiveStatus { get; set; }
        public string MFormno { get; set; }
        public string MemUpliner { get; set; }
        public string MID { get; set; }
        public string EMail { get; set; }
        public string ProfilePic { get; set; }
        public string Panno { get; set; }
        public DateTime? ActivationDate { get; set; }
        public string Type { get; set; }

        // ❌ password session me store nahi karenge
        public string MemPassw { get; set; }
        public string MemEPassw { get; set; }
    }
}
