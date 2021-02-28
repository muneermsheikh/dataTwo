using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class UserPhone
    {
        public int Id { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public bool IsValid { get; set; }=true;

        public int AppUserId { get; set; }
    }
}