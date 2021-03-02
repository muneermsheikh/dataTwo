using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Entities;

namespace api.DTOs
{
    public class RegisterDTO
    {
    //byepassed: created, LastACtive, photos, ikedUsers, likedByUsers
        // message sent, message recd
        
    //from IdentityDB 
        [Required, MinLength(5), MaxLength(20)] public string UserType {get; set;}
        [Required, MaxLength(15), MinLength(4)] public string Username { get; set; }
        [Required, MinLength(8), MaxLength(15)]public string Password { get; set; }
    // new fields    
        [StringLength(1)] public string Gender { get; set; }
        [MaxLength(20), MinLength(5)] public string FirstName {get; set;}
        public string SecondName {get; set;}
        public string FamilyName {get; set;}
        [MaxLength(50), MinLength(10)]public string CompanyName { get; set; }
        [Required, MaxLength(10), MinLength(4)] public string KnownAs { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
        [StringLength(12)] public string AadharNo {get; set;}
        [Required] public string City { get; set; }
        [Required] public string Country { get; set; }
        
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        //public string UserRole {get; set; }   //address this in api
        [Required] public ICollection<UserPassport> UserPassports {get; set;}        
        [Required] public ICollection<UserPhone> UserPhones {get; set; }
        public ICollection<UserQualification> UserQualifications {get; set;}
        public ICollection<Address> UserAddresses {get; set;}
        public ICollection<UserExp> UserExperiences {get; set;}

        [Required]public ICollection<UserProfessionDto> UserProfessions {get; set;}

    }
}