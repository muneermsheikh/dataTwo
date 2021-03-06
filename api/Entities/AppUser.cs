using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Extensions;
using Microsoft.AspNetCore.Identity;

namespace api.Entities
{
    public class AppUser: IdentityUser<int>
    {
        public int ApplicationNo { get; set; }
        public string UserType { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FamilyName { get; set; }
        public int AssociateId { get; set; }
        public string CompanyName { get; set; }
        public string KnownAs { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AadharNo { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string Status { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<UserLike> LikedByUsers { get; set; }
        public ICollection<UserLike> LikedUsers { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }        
        public ICollection<AppUserRole> UserRoles {get; set; }
        public ICollection<UserPassport> UserPassports {get; set;}
        public ICollection<UserPhone> UserPhones {get; set;}
        public ICollection<UserProfession> UserProfessions {get; set;}
        public virtual ICollection<UserQualification> UserQualifications {get; set; }
        public virtual ICollection<Address> UserAddresses {get; set;}
        public virtual ICollection<UserExp> UserExperiences {get; set;}
        
        public string FullName {get{ return FirstName + " " + SecondName + " " + FamilyName;}}
        
    }
}