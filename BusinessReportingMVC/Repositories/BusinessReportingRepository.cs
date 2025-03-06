using BusinessReportingMVC.Data;
using BusinessReportingMVC.Models;
using BusinessReportingMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessReportingMVC.Repositories
{
    public class BusinessReportingRepository : IBusinessReportingRepository
    {
        private readonly BusinessReportingDbContext _context;

        public BusinessReportingRepository(BusinessReportingDbContext context)
        {
            _context = context;
        }

        // ========
        // || Auth
        // ========
        public User? GetByEmail(string Email)
        {
            User user = _context.Users.FirstOrDefault(x => x.Email == Email);

            if (user == null) 
            {
                return null;
            }

            return user;
        }

        public User GetByEmailAndPassword(User user, string InputPassword)
        {
            //User user = GetByEmail(Email);

            string passwordAndSalt = InputPassword + user.Salt;

            string hashedPasswordAndSalt = passwordAndSalt.StringToBytesToHashed();

            if (hashedPasswordAndSalt == user.Password)
            {
                return user;
            }
            return null;
        }

        public async Task<User?> GetUserAndClaimsAsync(long id) // Change 
        {
            return await _context.Users
                                    .Include(r => r.UserClaims)
                                        .ThenInclude(uc => uc.Claim)
                                    .FirstOrDefaultAsync(r => r.UserId == id);
        }

        public async Task<List<Claim>> GetAllDBClaimsAsync()
        {
            return await _context.Claims.ToListAsync();
        }

        // ===========
        // || Reports
        // ===========
        public async Task<Report> GetReportByIdAsync(long id)
        {
            Report? report = await _context.Reports
                                    .Include(r => r.KeyHighlights)
                                    .Include(r => r.Financials)
                                        .ThenInclude(f => f.FinancialsActual)
                                    .Include(r => r.Financials)
                                        .ThenInclude(f => f.FinancialsDeviation)
                                    .Include(r => r.AdminAndResources)
                                    .Include(r => r.Project)
                                        .ThenInclude(r => r.ProjectIndividuals)
                                    .Include(r => r.Strategy)
                                    .Include(r => r.BusinessDevelopment)
                                        .ThenInclude(bd => bd.BusinessDevelopmentValue)
                                    .Include(r => r.BusinessDevelopment)
                                        .ThenInclude(bd => bd.BusinessDevelopmentNotes)
                                    .FirstOrDefaultAsync(r => r.ReportId == id);

            return report;
        }

        public async Task<List<Report>> GetListOfDraftReportsAsync(long id)
        {
            return await _context.Reports
                .Where(r => r.CreatedByUserId == id && r.IsDeleted == false && r.IsDraft == true)
                .ToListAsync();
        }

        public async Task<List<Report>> GetListOfPublishedReportsByUserAsync(long id)
        {
            return await _context.Reports
                .Where(r => r.CreatedByUserId == id && r.IsDeleted == false && r.IsDraft == false)
                .ToListAsync();
        }

        public async Task<List<Report>> GetListOfAllPublishedReports()
        {
            return await _context.Reports
                                    .Where(r => r.IsDraft == false && r.IsDeleted == false)
                                    .Include(r => r.CreatedByUser)
                                    .ToListAsync();
        }
    }
}
