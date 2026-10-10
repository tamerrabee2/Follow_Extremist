using System;

namespace Follow_Extremist.Core
{
    public class ElementSearchCriteria
    {
        public string ElementName { get; set; }
        public string NationalId { get; set; }
        public string MotherName { get; set; }
        public string PhoneOrMobile { get; set; }
        public string Address { get; set; }
        public string BirthPlace { get; set; }
        public string Job { get; set; }
        public string Qualification { get; set; }
        public string RegulatoryStatus { get; set; }
        public string PrisonedOrnot { get; set; }
        public string FollowState { get; set; }
        public string CaseData { get; set; }
        public string Notes { get; set; }
        public int? BirthYear { get; set; }
        public bool UseBirthDateFilter { get; set; }
        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }
        public bool UseDateFilter { get; set; }
        public DateTime? DateFollowFrom { get; set; }
        public DateTime? DateFollowTo { get; set; }
    }
}
