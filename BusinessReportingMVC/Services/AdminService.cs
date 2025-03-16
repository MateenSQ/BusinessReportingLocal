using BusinessReportingMVC.Data;
using BusinessReportingMVC.Models;
using BusinessReportingMVC.Repositories;
using BusinessReportingMVC.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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

        public async Task<List<PersonalInfoViewModel>> GetAllNonAdmins()
        {
            List<User> nonAdminDB = await _repo.GetAllNonAdminUsers();

            List<PersonalInfoViewModel> nonAdminUsers = new();

            foreach (User user in nonAdminDB)
            {
                nonAdminUsers.Add(new PersonalInfoViewModel
                {
                    Id = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    Approved = user.IsApproved,
                    Role = user.UserClaims.First(uc => uc.Claim.ClaimType == "Role").Claim.ClaimName,
                    Position = user.UserClaims.First(uc => uc.Claim.ClaimType == "Position").Claim.ClaimName,
                });
            }

            return nonAdminUsers;
        }

        public async Task<PersonalInfoViewModel> GetUserAndMapToViewModel(long id)
        {
            User user = await _repo.GetUserAndClaimsAsync(id);

            PersonalInfoViewModel UserInfo = new PersonalInfoViewModel
            {
                Id = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Approved=user.IsApproved,
                Role = user.UserClaims.First(uc => uc.Claim.ClaimType == "Role").Claim.ClaimName,
                Position = user.UserClaims.First(uc => uc.Claim.ClaimType == "Position").Claim.ClaimName
            };

            return UserInfo;
        }

        public async Task UpdateUserInformation(PersonalInfoViewModel submittedUserInfo)
        {
            User user = await _context.Users
                .Include(u => u.UserClaims)
                    .ThenInclude(uc => uc.Claim)
                .FirstOrDefaultAsync(u => u.UserId == submittedUserInfo.Id);

            user.IsApproved = (bool)submittedUserInfo.Approved;
            user.UserClaims.FirstOrDefault(uc => uc.Claim.ClaimType == "Position").Claim.ClaimName = submittedUserInfo.Position;
            user.UserClaims.FirstOrDefault(uc => uc.Claim.ClaimType == "Role").Claim.ClaimName = submittedUserInfo.Role;

            await _context.SaveChangesAsync();
        }
    }
}
