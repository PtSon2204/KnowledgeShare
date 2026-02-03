using KnowledgeShare.API.Controllers;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.API.ViewModels;
using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeShare.API.UnitTest.Controllers
{
    public class RolesControllerTest
    {
        private readonly Mock<IRoleService> _roleServiceMock;
        private readonly RolesController _controller;

        public RolesControllerTest()
        {
            _roleServiceMock = new Mock<IRoleService>();
            _controller = new RolesController(_roleServiceMock.Object);
        }

        [Fact]
        public void Constructor_CreateInstance_NotNull()
        {
            // Assert
            Assert.NotNull(_controller);
        }


        [Fact] //Test 2 – POST Role thành công → 201 Created
        public async Task PostRole_ValidInput_ReturnsCreatedAtAction()
        {
            // Arrange
            var roleVm = new RoleVm
            {
                Id = "admin",
                Name = "Admin"
            };

            _roleServiceMock
                .Setup(s => s.CreateRoleAsync(It.IsAny<RoleVm>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.PostRole(roleVm);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(RolesController.GetById), createdResult.ActionName);

            _roleServiceMock.Verify(
                s => s.CreateRoleAsync(It.Is<RoleVm>(r => r.Name == "Admin")),
                Times.Once
            );
        }

        [Fact] //Test 3 – POST Role thất bại → 400 BadRequest
        public async Task PostRole_CreateFail_ReturnsBadRequest()
        {
            // Arrange
            var roleVm = new RoleVm
            {
                Id = "admin",
                Name = "Admin"
            };

            var failedResult = IdentityResult.Failed(
                new IdentityError { Description = "Duplicate role" }
            );

            _roleServiceMock
                .Setup(s => s.CreateRoleAsync(It.IsAny<RoleVm>()))
                .ReturnsAsync(failedResult);

            // Act
            var result = await _controller.PostRole(roleVm);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact] //Test 4 – GET Role by Id → 200 OK
        public async Task GetById_ExistingRole_ReturnsOk()
        {
            // Arrange
            var roleVm = new RoleVm
            {
                Id = "admin",
                Name = "Admin"
            };

            _roleServiceMock
                .Setup(s => s.GetById("admin"))
                .ReturnsAsync(roleVm);

            // Act
            var result = await _controller.GetById("admin");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<RoleVm>(okResult.Value);
            Assert.Equal("Admin", value.Name);
        }

        [Fact] //Test 5 – GET Role không tồn tại → 404
        public async Task GetById_NotFound_Returns404()
        {
            // Arrange
            _roleServiceMock
                .Setup(s => s.GetById("not-exist"))
                .ReturnsAsync((RoleVm)null);

            // Act
            var result = await _controller.GetById("not-exist");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact] //Test 6 – DELETE Role thành công → 204 NoContent
        public async Task DeleteRole_Success_ReturnsNoContent()
        {
            // Arrange
            _roleServiceMock
                .Setup(s => s.DeleteRoleAsync("admin"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.DeteleRole("admin");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact] //Test 7 – DELETE Role không tồn tại → 404
        public async Task DeleteRole_NotFound_Returns404()
        {
            // Arrange
            _roleServiceMock
                .Setup(s => s.DeleteRoleAsync("not-exist"))
                .ReturnsAsync((IdentityResult)null);

            // Act
            var result = await _controller.DeteleRole("not-exist");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}