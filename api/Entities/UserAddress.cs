namespace api.Entities
{
    public class UserAddress
    {
        public int Id { get; set; }
        public Address Address { get; set; }
        public bool IsMain { get; set; }
        
    }
}