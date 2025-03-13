using BusinessReportingMVC.Data;
using BusinessReportingMVC.Models;
using BusinessReportingMVC.Repositories;
using BusinessReportingMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessReportingMVC.Services
{
    public class AdminService : IAdminService
    {
        private readonly IBusinessReportingRepository _repo;
        private readonly IHttpContextAccessor _accessor;
        private readonly BusinessReportingDbContext _context;

        public AdminService(IBusinessReportingRepository Repo, IHttpContextAccessor Accessor, BusinessReportingDbContext Context)
        {
            _repo = Repo;
            _accessor = Accessor;
            _context = Context;
        }

        public async Task<PersonalInfoViewModel> GetPersonalInfoAsync(long id)
        {
            User? user = await _repo.GetUserAndClaimsAsync(id);

            string dbPositionClaim = user.UserClaims.FirstOrDefault(c => c.Claim.ClaimType == "Position").Claim.ClaimName;
            string dbRoleClaim = user.UserClaims.FirstOrDefault(c => c.Claim.ClaimType == "Role").Claim.ClaimName;

            PersonalInfoViewModel personalViewModel = new()
            {
                Id = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Position = dbPositionClaim,
                Role = dbRoleClaim
            };
            return personalViewModel;
        }

        public async Task DeleteUser(long id)
        {
            User user = await _context.Users.Include(u => u.UserClaims).FirstOrDefaultAsync(u => u.UserId == id);

            _context.Remove(user);

            await _context.SaveChangesAsync();
        }
    }
}
