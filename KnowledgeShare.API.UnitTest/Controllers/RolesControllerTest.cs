using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeShare.API.Controllers;
using KnowledgeShare.API.Interface;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace KnowledgeShare.API.UnitTest.Controllers
{
    //public class RolesControllerTest
    //{
    //    //private readonly Mock<IRoleService>()
    //    //public RolesControllerTest()
    //    //{

    //    //}
    //    [Fact]
    //    public void ShouldCreateInstance_NotNull_Ok()
    //    {
    //        var roleStore = new Mock<IRoleStore<IdentityRole>>();
    //        // Thay vì Mock RoleManager, hãy Mock đúng Interface mà Controller cần
    //        var mokRoleService = new Mock<IRoleService>();

    //        // Truyền trực tiếp Object của Mock vào (không cần ép kiểu thủ công)
    //        var rolesController = new RolesController(mokRoleService.Object);

    //        Assert.NotNull(rolesController);
    //    }

    //    [Fact]
    //    public void PostRole_ValidInput_Success()
    //    {
    //        var roleStore = new Mock<IRoleStore<IdentityRole>>();
    //        var mokRoleService = new Mock<IRoleService>();
    //        var rolesController = new RolesController(mokRoleService.Object);

    //        Assert.NotNull(rolesController);
    //    }
    //}
}
