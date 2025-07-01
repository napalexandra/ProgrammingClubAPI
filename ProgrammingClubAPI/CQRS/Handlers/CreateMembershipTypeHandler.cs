using MediatR;
using ProgrammingClubAPI.CQRS.Commands;
using ProgrammingClubAPI.DataContext;
using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.CQRS.Handlers
{
    public class CreateMembershipTypeHandler : IRequestHandler<CreateMembershipTypeCommand, Guid>
    {
        private readonly ProgrammingClubDataContext _context;

        public CreateMembershipTypeHandler(ProgrammingClubDataContext context)
        {
            _context = context;
        }


        public async Task<Guid> Handle(CreateMembershipTypeCommand request, CancellationToken cancellationToken)
        {
            var membershipType = new MembershipType
            {
                IdMembershipType = Guid.NewGuid(),
                Name = request.Dto.Name,
                Description = request.Dto.Description,
                SubscriptionLengthInMonths = request.Dto.SubscriptionLengthInMonths
            };

            if (string.IsNullOrEmpty(request.Dto.Name))
            {
                throw new Exception("Name required");
            }

            _context.MembershipTypes.Add(membershipType);
            _context.SaveChangesAsync(cancellationToken);
            return membershipType.IdMembershipType;
        }
    }

}
