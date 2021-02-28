using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class UserQualification
    {
        public int Id { get; set; }
        public int? QualificationId {get; set;}
        public string Qualification { get; set; }="";
        [Required]
        public bool IsMain { get; set; }=false;
        public int AppUserId { get; set; }
        
    }
}