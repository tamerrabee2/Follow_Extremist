using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Core
{
    public class ElementInfo
    {
        public int Id { get; set; }
        public string ElementName { get; set; }
        public string NationalId { get; set; }
        public string MotherName { get; set; }
        public string Qualification { get; set; }
        public string Job { get; set; }
        public DateTime BirthDate { get; set; }
        public string  BirthPlace { get; set; }
        public byte[] ElementImage { get; set; }
        public byte[] NationalIdImage { get; set; }
        public string FollowState { get; set; }
        public string ReasonEndFollow { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
        public string  Mobile { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }
        public DateTime DateFollowStart { get; set; }
        public int FollowDaysCount { get; set; }
        public DateTime DateFollowNow { get; set; }
        public DateTime DateFollowNext { get; set; }
        public string Address { get; set; }
        public string RegulatoryStatus { get; set; }
        public string FacebookAcount { get; set; }
        public string FacebookID { get; set; }
        public string PrisonedOrnot { get; set; }
        public string CaseData { get; set; }
        // Navigation
        public List<ElementAddInfo> ElementAddInfo { get; set; }
        public List<ElementFollowAdd> ElementFollowAdd { get; set; }
        public List<ElementCases> ElementCases { get; set; }


    }
}
