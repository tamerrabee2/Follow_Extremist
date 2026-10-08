using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Core
{
   public class ElementInfoView
    {
        public string ElementName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public DateTime DateFollowNow { get; set; }
        public DateTime DateFollowNext { get; set; }
    }
}
