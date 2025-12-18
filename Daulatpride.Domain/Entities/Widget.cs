using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daulatpride.Domain.Entities
{
    public class Widget
    {
        public string Name { get; set; }
        public int Direct { get; set; }
        public string Icon { get; set; }

        [Column("div")]
        public string Div { get; set; }
    }
}
