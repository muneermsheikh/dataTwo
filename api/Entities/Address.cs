using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Add { get; set; }
        public string StreetAdd { get; set; }
        public string Location { get; set; }
        [Required]
        public string City { get; set; }
        public string Pin { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }="India";
        public AppUser AppUser { get; set; }
        [Required ]
        public bool IsMain { get; set; }

    }
}