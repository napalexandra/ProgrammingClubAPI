using MediatR;
using ProgrammingClubAPI.CQRS.Commands;
using ProgrammingClubAPI.DataContext;

namespace ProgrammingClubAPI.CQRS.Handlers
{
    public class DeleteMembershipTypeHandler:IRequestHandler<DeleteMembershipTypeCommand, bool>
    {
        private readonly ProgrammingClubDataContext _context;
        public DeleteMembershipTypeHandler(ProgrammingClubDataContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteMembershipTypeCommand request, CancellationToken cancellationToken)
        {
            var membershipType = await _context.MembershipTypes.FindAsync(request.IdMembershipType);
            if (membershipType == null)
            {
                return false; // Not found
            }
            _context.MembershipTypes.Remove(membershipType);
            await _context.SaveChangesAsync(cancellationToken);
            return true; // Successfully deleted
        }
    }
}
