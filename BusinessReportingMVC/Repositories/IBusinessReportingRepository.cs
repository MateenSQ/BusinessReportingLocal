using BusinessReportingMVC.Models;

namespace BusinessReportingMVC.Repositories
{
    public interface IBusinessReportingRepository
    {
        // ========
        // || Auth
        // ========
        public User GetByEmail(string Email);

        public User GetByEmailAndPassword(User user, string InputPassword);

        public Task<List<Claim>> GetAllDBClaimsAsync();

        // ===========
        // || Reports
        // ===========
        public Task<List<Report>> GetListOfDraftReportsAsync(long id);

        public Task<List<Report>> GetListOfPublishedReportsByUserAsync(long id);

        public Task<Report> GetReportByIdAsync(long id);

        public Task<User?> GetUserAndClaimsAsync(long id);

        public Task<List<Report>> GetListOfAllPublishedReports();

        // =========
        // || Admin
        // =========
        public Task<List<User>> GetAllNonAdminUsers();
    }
}
