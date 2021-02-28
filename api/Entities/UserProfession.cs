using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class UserProfession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public int ProfessionId { get; set; }
        public string Industry { get; set; }
        public bool IsMain { get; set; }=false;
        public int AppUserId { get; set; }
        
    }
}