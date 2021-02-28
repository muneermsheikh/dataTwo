using System;
using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class UserPassport
    {
        public int Id { get; set; }
        [Required]
        public string PassportNo { get; set; }
        public string IssuedAt { get; set; }
        public DateTime IssuedOn { get; set; }
        [Required]
        public DateTime Validity {get; set;}
        public bool Ecnr { get; set; }
        public bool IsValid { get; set; }=true;
        public int AppUserId { get; set; }
        
    }
}