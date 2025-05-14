using BusinessReportingMVC.Data;
using BusinessReportingMVC.Repositories;
using BusinessReportingMVC.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessReportingMVCXUnit.Controllers
{
    public class ControllerTestBase
    {
        public readonly BusinessReportingDbContext _contextMock;
        public readonly Mock<IAuthService> _authServiceMock;
        public readonly Mock<IReportService> _reportServiceMock;
        public readonly Mock<IAdminService> _adminServiceMock;
        public readonly Mock<IBusinessReportingRepository> _repoMock;
        public readonly Mock<IHttpContextAccessor> _httpAccessormock;

        public ControllerTestBase()
        {
            _reportServiceMock = new Mock<IReportService>(MockBehavior.Strict);
            _authServiceMock = new Mock<IAuthService>(MockBehavior.Strict);
            _adminServiceMock = new Mock<IAdminService>(MockBehavior.Strict);
            _repoMock = new Mock<IBusinessReportingRepository>(MockBehavior.Strict);
            _httpAccessormock = new Mock<IHttpContextAccessor>(MockBehavior.Strict);
            _contextMock = new BusinessReportingDbContext();
        }
    }
}
