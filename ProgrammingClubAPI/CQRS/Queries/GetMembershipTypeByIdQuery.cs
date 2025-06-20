using MediatR;
using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.CQRS.Queries
{
    public class GetMembershipTypeByIdQuery : IRequest<MembershipType>
    {
        public Guid IdMembershipType { get; set; }
        public GetMembershipTypeByIdQuery(Guid idMembershipType)
        {
            IdMembershipType = idMembershipType;
        }
    }
}
