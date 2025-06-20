using MediatR;
using ProgrammingClubAPI.CQRS.Commands;
using ProgrammingClubAPI.CQRS.DTOs;
using ProgrammingClubAPI.DataContext;
using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.CQRS.Handlers
{
    public class UpdateMembershipTypeHandler : IRequestHandler<UpdateMembershipTypeCommand, MembershipType>
    {
        private readonly ProgrammingClubDataContext _context;
        public UpdateMembershipTypeHandler(ProgrammingClubDataContext context)
        {
            _context = context;
        }

        public async Task<MembershipType> Handle(UpdateMembershipTypeCommand request, CancellationToken cancellationToken)
        {
            var membershipType = await _context.MembershipTypes.FindAsync(request.Dto.IdMembershipType);
            if (membershipType == null)
            {
                throw new KeyNotFoundException($"Membership type with ID {request.Dto.IdMembershipType} not found.");
            }
            membershipType.Name = request.Dto.Name;
            membershipType.Description = request.Dto.Description;
            membershipType.SubscriptionLengthInMonths = request.Dto.SubscriptionLengthInMonths;
            _context.MembershipTypes.Update(membershipType);
            _context.SaveChanges();
            return membershipType;
        }
    }

}
