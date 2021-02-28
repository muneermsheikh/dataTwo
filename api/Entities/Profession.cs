using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class Profession
    {
        public Profession()
        {
        }

        public Profession(string name, string industry)
        {
            Name = name;
            Industry = industry;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Industry { get; set; }
    }
}