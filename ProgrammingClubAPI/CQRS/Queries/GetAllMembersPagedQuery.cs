using MediatR;
using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.CQRS.Queries
{
    public class GetAllMembersPagedQuery : IRequest<PagedResultDto<Member>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllMembersPagedQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : pageSize;
        }
    }
}
