using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class Profession
    {
        public Profession()
        {
        }

        public Profession(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}