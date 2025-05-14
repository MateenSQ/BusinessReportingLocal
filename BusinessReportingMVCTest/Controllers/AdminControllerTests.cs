using BusinessReportingMVC.Controllers;
using BusinessReportingMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessReportingMVCXUnit.Controllers
{
    
    public class AdminControllerTests : ControllerTestBase
    {
        [Fact]
        public async void ManageUsers_ReturnsViewResultWithNoExceptions()
        {
            // Arrange
            var data = new List<PersonalInfoViewModel> 
            {
                new() { Id = 1, Email = "user1@email.com", Name = "User1", Approved = true, Position = "Seacht", Role = "Admin" },
                new() { Id = 1, Email = "user1@email.com", Name = "User1", Approved = true, Position = "MD", Role = "User" }
            };

            _adminServiceMock.Setup(s => s.GetAllNonAdmins()).ReturnsAsync(data);

            AdminController controller = new AdminController(_adminServiceMock.Object);

            // Act
            var result = await controller.ManageUsers();

            var exception = await Record.ExceptionAsync(() => controller.ManageUsers());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            _adminServiceMock.Verify(service => service.GetAllNonAdmins(), Times.Exactly(2));
            Assert.NotNull(viewResult);
            Assert.Null(exception);
        }
    }
}
