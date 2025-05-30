using AutoMapper;
using ProgrammingClubAPI.Models;
using ProgrammingClubAPI.Models.CreateOrUpdateModels;
using System.Runtime;

namespace ProgrammingClubAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, UpdateMemberPartially>().ReverseMap();
        }
    }
}
