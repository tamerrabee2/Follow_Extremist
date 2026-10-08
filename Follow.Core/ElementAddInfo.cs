using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Core
{
    public class ElementAddInfo
    {
        public int Id { get; set; }
        public string NameRelationElement { get; set; }
        public string ElementName { get; set; }
        public string ElementRelationNationalID { get; set; }
        public string Relationship { get; set; }
        public string Age { get; set; }
        public byte[] ElemmentRelationImage { get; set; }
        public byte[] ElementRelationNationalIDImage { get; set; }

        // Navigation 
        public int ElementInfoId { get; set; }
        public ElementInfo ElementInfo { get; set; }

    }
}
