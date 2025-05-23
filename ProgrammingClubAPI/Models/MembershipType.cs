using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgrammingClubAPI.Models
{
    public class MembershipType
    {
        [Key]
        public Guid IdMembershipType { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public int? SubscriptionLengthInMonths { get; set; }
    }
}
