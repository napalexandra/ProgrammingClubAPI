using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProgrammingClubAPI.CQRS.Queries;
using ProgrammingClubAPI.DataContext;
using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.CQRS.Handlers
{
    //noi in controller vom chema Query-ul, MediatR Cauta handler-ul care implementeaza query-ul, si se apeleaza metoda Handle
    public class GetAllMembershipTypesHandler : IRequestHandler<GetAllMembershipTypesQuery, IEnumerable<MembershipType>>
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMemoryCache _cache;
        public GetAllMembershipTypesHandler(ProgrammingClubDataContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<MembershipType>> Handle(GetAllMembershipTypesQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "AllMembershipTypes";

            // 1. Verficam daca sunt date in cache
            if (_cache.TryGetValue(cacheKey, out IEnumerable<MembershipType> cachedMembershipTypes))
            {
                return cachedMembershipTypes; // Return cached data if available
            }

            //2. Daca nu sunt date in cache, le luam din baza de date
            var membershipTypes = await _context.MembershipTypes.ToListAsync(cancellationToken);

            //3. Configuram optiunire de cache
            var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));

            //4. Salvam datele in cache
            _cache.Set(cacheKey, membershipTypes, cacheOptions);

            return membershipTypes;
        }
    }
}
