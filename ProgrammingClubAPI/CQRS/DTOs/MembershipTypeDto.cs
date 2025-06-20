namespace ProgrammingClubAPI.CQRS.DTOs
{
    public class MembershipTypeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SubscriptionLengthInMonths { get; set; }
    }
}
