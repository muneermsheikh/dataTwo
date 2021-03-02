namespace api.Entities
{
    public class CustomerOfficial
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string OfficialName { get; set; }
        public string Designation { get; set; }
        public string Mobile { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public bool IsValid { get; set; }=true;
        public Customer Customer {get; set;}
    }
}