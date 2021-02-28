using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class EditQDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}