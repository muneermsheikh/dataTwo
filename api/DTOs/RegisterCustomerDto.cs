using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Entities;

namespace api.DTOs
{
    public class RegisterCustomerDto
    {
        public int Id { get; set; }
        [Required]public string Username { get; set; }
        [Required]public string Password { get; set; }
        [Required, MinLength(5), MaxLength(25)]public string CustomerName { get; set; }
        [Required, MinLength(4), MaxLength(15)]public string KnownAs { get; set; }
        [Required, MinLength(5), MaxLength(10)]public string CustomerType { get; set; }
        public string Add { get; set; }
        public string Add2 { get; set; }
        [Required, MinLength(5), MaxLength(20)]public string City { get; set; }
        public string Pin { get; set; }
        public string State { get; set; }
        public string Country { get; set; }="India";
        public string IntroducedBy { get; set; }
        public bool IsActive { get; set; }=true;
        public ICollection<CustomerIndustry> CustomerIndustries { get; set; }
        public ICollection<AgencySpecialty> AgencySpecialties {get; set;}
        public ICollection<CustomerOfficial> CustomerOfficials {get; set;}
    }
}