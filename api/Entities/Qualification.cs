using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class Qualification
    {
        public Qualification()
        {
        }

        public Qualification(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}