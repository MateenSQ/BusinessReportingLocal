using BusinessReportingMVC.ViewModels;

namespace BusinessReportingMVC.Services
{
    public interface IAdminService
    {
        public Task<PersonalInfoViewModel> GetPersonalInfoAsync(long id);

        public Task DeleteUser(long id);

        public Task<List<PersonalInfoViewModel>> GetAllNonAdmins();
    }
}
