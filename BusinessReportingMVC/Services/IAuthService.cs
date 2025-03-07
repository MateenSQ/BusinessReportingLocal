using BusinessReportingMVC.Models;
using BusinessReportingMVC.ViewModels;

namespace BusinessReportingMVC.Services
{
    public interface IAuthService
    {
        public int GetIDClaim();

        public Task<Result> HandleLoginAsync(LoginModel model);

        public Task<Result> HandleRegisterAsync(UserViewModel model);

        public Task<PersonalInfoViewModel> GetPersonalInfoAsync(long id);
    }
}
