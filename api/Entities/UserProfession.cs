using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class UserProfession
    {
        public int Id { get; set; }
        public int ProfessionId { get; set; }
        public int IndustryId { get; set; }
        public string ProfessionName { get; set; }
        public string IndustryName { get; set; }
        public bool IsMain { get; set; }=false;
        public int AppUserId { get; set; }
        
    }
}