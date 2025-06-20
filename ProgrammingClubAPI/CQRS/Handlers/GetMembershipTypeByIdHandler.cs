using MediatR;
using ProgrammingClubAPI.CQRS.Queries;
using ProgrammingClubAPI.DataContext;
using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.CQRS.Handlers
{
    public class GetMembershipTypeByIdHandler : IRequestHandler<GetMembershipTypeByIdQuery, MembershipType>
    {
        private readonly ProgrammingClubDataContext _context;
        public GetMembershipTypeByIdHandler(ProgrammingClubDataContext context)
        {
            _context = context;
        }
        public async Task<MembershipType> Handle(GetMembershipTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var membershipType = await _context.MembershipTypes.FindAsync(request.IdMembershipType);
            if (membershipType == null)
            {
                throw new KeyNotFoundException($"Membership type with ID {request.IdMembershipType} not found.");
            }
            return membershipType;
        }
    }
}
