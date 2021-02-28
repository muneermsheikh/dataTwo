using System;

namespace api.Entities
{
    public class UserExp
    {
        public int Id { get; set; }
        public int SrNo { get; set; }
        public int ProfessionId { get; set; }
        public string Employer { get; set; }
        public string Position { get; set; }
        public string SalaryCurrency { get; set; }
        public int MonthlySalaryDrawn { get; set; }
        public DateTime WorkedFrom { get; set; }
        public DateTime WorkedUpto {get; set;}
        public int AppUserId { get; set; }
    }
}