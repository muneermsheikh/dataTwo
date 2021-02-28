using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class EditProfDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Industry { get; set; }
    }
}