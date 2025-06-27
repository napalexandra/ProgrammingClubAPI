using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgrammingClubAPI.CQRS.Queries;
using ProgrammingClubAPI.DataContext;
using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.CQRS.Handlers
{
    public class GetAllMembersPagedHandler : IRequestHandler<GetAllMembersPagedQuery, PagedResultDto<Member>>
    {
        private readonly ProgrammingClubDataContext _context;
        public GetAllMembersPagedHandler(ProgrammingClubDataContext context)
        {
            _context = context;
        }
        public async Task<PagedResultDto<Member>> Handle(GetAllMembersPagedQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Members.AsNoTracking().OrderBy(member => member.Name).AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResultDto<Member>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
