using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class Industry
    {
        public Industry()
        {
        }

        public Industry(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}