using ProgrammingClubAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingClubAPI.UnitTests.Models.Builders
{
    public class MemberBuilder : BuilderBase<Member>
    {
        public MemberBuilder()
        {
            _objectToBuild = new Member
            {
                IdMember = Guid.NewGuid(),
                Name = "John",
                Description = "test member",
                Username = "test@gmail.com",
                Title = "Member",
                Position = "Developer",
                Password = "password123",
                Resume = "https://example.com/resume.pdf"
            };
        }
    }
}
