using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class AddProfDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Industry { get; set; }
    }
}