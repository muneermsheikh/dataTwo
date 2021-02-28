using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class UserProfessionDto
    {
        public int Id { get; set; }
        [Required]
        public int ProfessionId { get; set; }
        [Required]
        public bool IsMain { get; set; }
    }
}