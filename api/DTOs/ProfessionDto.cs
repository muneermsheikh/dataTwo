namespace api.DTOs
{
    public class ProfessionDto
    {
        public ProfessionDto(int id, string nameWithIndustry)
        {
            Id = id;
            NameWithIndustry = nameWithIndustry;
        }

        public int Id { get; set; }
        public string NameWithIndustry { get; set; }
    }
}