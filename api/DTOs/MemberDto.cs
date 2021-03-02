using System;
using System.Collections.Generic;

namespace api.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FamilyName { get; set; }
        public string Username { get; set; }
        public string KnownAs { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }
        public string AadharNo { get; set; }
        public string PassportNo { get; set; }
        public string UserProfessions { get; set; }
        public string UserPhones {get; set;}
        public string PhotoUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}