using System.Collections.Generic;

namespace api.Helpers
{
    public class UserParams : PaginationParams
    {
        public string CurrentUsername { get; set; }
        public string Gender { get; set; }
        public string MemberRole {get; set;}
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 150;
        public string Status { get; set; }
        public string AssociateId { get; set; } // csv
        public string UserType { get; set; }="candidate";
        public string NameLike { get; set; }
        public string ProfessionLike { get; set; }
        public string IndustryLike { get; set; }
        public string OrderBy { get; set; } = "lastActive";
    }
}