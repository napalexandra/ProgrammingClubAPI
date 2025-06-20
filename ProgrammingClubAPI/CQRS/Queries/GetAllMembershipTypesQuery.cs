using MediatR;
using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.CQRS.Queries
{
    //Vreau sa obtin toate tipurile de membership din baza de date
    public class GetAllMembershipTypesQuery : IRequest<IEnumerable<MembershipType>>
    {
    }
}
