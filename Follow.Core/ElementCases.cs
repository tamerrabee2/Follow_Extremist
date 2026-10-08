using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Core
{
   public class ElementCases
    {
        public int Id { get; set; }
        public string ElementName { get; set; }
        public DateTime ElementDateJail { get; set; }
        public string CasesData { get; set; }
        public string ElementDateRelease { get; set; }
        public string ElementStateJailOrNot { get; set; }
        public string ElementFollowNew { get; set; }

        // Navigation 
        public int ElementInfoId { get; set; }
        public ElementInfo ElementInfo { get; set; }
    }
}
