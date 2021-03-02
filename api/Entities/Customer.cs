using System.Collections.Generic;

namespace api.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string KnownAs { get; set; }
        public string CustomerType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Add { get; set; }
        public string Add2 { get; set; }
        public string City { get; set; }
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