using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daulatpride.Domain.Entities
{
    public class DirectMemberV
    {
        public int SNo { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string ActivationDate { get; set; }
        public string KitName { get; set; }
        public decimal KitAmount { get; set; }
        public string BillNo { get; set; }
        public string Terms { get; set; }
    }
}
