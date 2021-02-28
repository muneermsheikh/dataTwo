using System;
using System.Collections.Generic;

namespace api.Entities
{
    public class Person
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FamilyName { get; set; }
        public DateTime DOB { get; set; }
        public string AadharNo { get; set; }
        public string PPNo { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public string Remarks { get; set; }

    }
}