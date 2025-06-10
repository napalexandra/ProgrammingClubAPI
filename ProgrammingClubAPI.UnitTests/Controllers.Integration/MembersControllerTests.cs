using Microsoft.AspNetCore.Mvc;
using ProgrammingClubAPI.Controllers;
using ProgrammingClubAPI.DataContext;
using ProgrammingClubAPI.Helpers;
using ProgrammingClubAPI.Models;
using ProgrammingClubAPI.Repositories;
using ProgrammingClubAPI.Services;
using ProgrammingClubAPI.UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingClubAPI.UnitTests.Controllers.Integration
{
    public class MembersControllerTests
    {
        private readonly MembersService _membersService;
        private readonly MembersRepository repository;
        private readonly MembersController _membersController;
        private readonly ProgrammingClubDataContext _contextInMemory;

        public MembersControllerTests()
        {
            _contextInMemory = DBContextHelper.GetDatabaseContext();
            repository = new MembersRepository(_contextInMemory);
            _membersService = new MembersService(repository, null);
            _membersController = new MembersController(_membersService);
        }

        [Fact]
        public async Task Get_ShouldReturnAllMembers_WhenMembersExist()
        {
            //Arrange
            var testMember1 = await DBContextHelper.AddTestMember(_contextInMemory);
            var testMember2 = await DBContextHelper.AddTestMember(_contextInMemory);

            //Act
            var result = await _membersController.Get();
            var okResult = result as OkObjectResult;
            var members = okResult.Value as IEnumerable<Member>;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(members);
            Assert.Equal(2, members.Count());
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenNoMembersExist()
        {
            //Arrange

            //Act
            var result = await _membersController.Get();

            //Assert
            Assert.NotNull(result);
            var notFoundResult = result as ObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal(200, notFoundResult.StatusCode);
            Assert.Equal(ErrorMessagesEnum.NoMembersFound, notFoundResult.Value);
        }
    }
}
