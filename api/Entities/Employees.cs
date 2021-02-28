using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class Employees: Person
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string KnownAs { get; set; }
        public Qualification RelevantQualification {get; set;}
        public DateTime DOJ {get; set;}
        public DateTime LastWorkingDay {get; set;}
        public ICollection<Skill> Skills {get; set;}
        
        public bool IsCurrent { get; set; }
        
    }
}