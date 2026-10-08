using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Extremist.Core
{
    public class ElementFollowAdd
    {
        public int Id { get; set; }
        public string ElementName { get; set; }
        public DateTime DateFollow { get; set; }


        // Navigation 
        public int ElementInfoId { get; set; }
        public ElementInfo ElementInfo { get; set; }

    }
}
