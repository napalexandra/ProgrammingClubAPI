using MediatR;
using ProgrammingClubAPI.CQRS.DTOs;

namespace ProgrammingClubAPI.CQRS.Commands
{
    public class CreateMembershipTypeCommand : IRequest<Guid>
    {
        public MembershipTypeDto Dto { get; set; }
        public CreateMembershipTypeCommand(MembershipTypeDto dto)
        {
            Dto = dto;
        }
    }
}
