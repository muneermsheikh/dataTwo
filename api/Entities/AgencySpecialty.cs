namespace api.Entities
{
    public class AgencySpecialty
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProfessionId { get; set; }
        public int IndustryId { get; set; }
        public Customer Customer { get; set; }
    }
}