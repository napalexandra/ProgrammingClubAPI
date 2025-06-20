using MediatR;
using ProgrammingClubAPI.CQRS.DTOs;
using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.CQRS.Commands
{
    public class UpdateMembershipTypeCommand : IRequest<MembershipType>
    {
        public MembershipType Dto { get; set; }
        public UpdateMembershipTypeCommand(MembershipType dto)
        {
            Dto = dto;
        }
    }
}
