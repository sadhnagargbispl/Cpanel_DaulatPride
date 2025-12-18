using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daulatpride.Domain.Entities
{
    public class MemberDto
    {
        public string IdNo { get; set; }
        public string MemName { get; set; }
        public DateTime Doj { get; set; }
        public string Passw { get; set; }
        public string EPassw { get; set; }
    }

}
