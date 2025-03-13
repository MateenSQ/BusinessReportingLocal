using BusinessReportingMVC.Data;
using BusinessReportingMVC.Models;
using BusinessReportingMVC.Repositories;
using BusinessReportingMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;

// Aliases
using SecurityClaim = System.Security.Claims.Claim;
using ModelClaim = BusinessReportingMVC.Models.Claim;
using Microsoft.EntityFrameworkCore;

namespace BusinessReportingMVC.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBusinessReportingRepository _repo;
        private readonly IHttpContextAccessor _accessor;
        private readonly BusinessReportingDbContext _context;

        public AuthService(IBusinessReportingRepository Repo, IHttpContextAccessor Accessor, BusinessReportingDbContext Context)
        {
            _repo = Repo;
            _accessor = Accessor;
            _context = Context;
        }

        public int GetIDClaim()
        {
                int createdByUserId = 0;

                foreach (var claim in _accessor.HttpContext.User.Claims) //Investigate the benefits of converting to LINQ statement
                {
                    var claimType = claim.Type;
                    var claimValue = claim.Value;

                    if (claimType == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                    {
                        createdByUserId = int.Parse(claimValue);
                        return createdByUserId;
                    }
                }
            throw new Exception("Error: User ID not found");
        }

        public async Task<Result> HandleLoginAsync(LoginModel model)
        {
            User userByEmail = _repo.GetByEmail(model.Email);

            if (userByEmail == null) 
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "Email not Found"
                };
            }

            User user = _repo.GetByEmailAndPassword(userByEmail, model.Password);

            if (user == null)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "Invalid Credentials"
                };
            }

            user = await _repo.GetUserAndClaimsAsync(user.UserId);

            string roleClaim = user.UserClaims.FirstOrDefault(c => c.Claim.ClaimType == "Role").Claim.ClaimName;
            string positionClaim = user.UserClaims.FirstOrDefault(c => c.Claim.ClaimType == "Position").Claim.ClaimName;

            var claims = new List<SecurityClaim>
            {
                new SecurityClaim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new SecurityClaim(ClaimTypes.Name, user.Name),
                new SecurityClaim(ClaimTypes.Role, roleClaim),
                new SecurityClaim("Position", positionClaim),
                new SecurityClaim("Approved", user.IsApproved.ToString())
            };

            string cookie = "DefaultCookie";

            var identity = new ClaimsIdentity(claims, cookie);

            var principal = new ClaimsPrincipal(identity);

            await _accessor.HttpContext.SignInAsync(cookie, principal,
                new AuthenticationProperties { IsPersistent = model.RememberLogin });

            return new Result
            {
                IsSuccess = true,
                Message = "No error"
            };
        }

        public async Task<Result> HandleRegisterAsync(UserViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "Passwords do not match"
                };
            }

            // Find if user's email exists in database
            var exists = _repo.GetByEmail(model.Email);

            if (exists is not null) 
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "Email already registered"
                };
            }

            // Set the user values first
            User user = new User
            {
                Email = model.Email,
                Name = model.Name,
                IsApproved = false
            };

            // Generate random salt to salt and hash password
            byte[] salt = new byte[32];

            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(salt);
                user.Salt = Convert.ToBase64String(salt);
            }

            string passwordAndSalt = model.Password + user.Salt;

            user.Password = passwordAndSalt.StringToBytesToHashed();

            // DB Claims
            List<ModelClaim> DbClaims = await _repo.GetAllDBClaimsAsync();

            // Claims
            UserClaim roleClaim = new UserClaim
            {
                User = user,
                Claim = DbClaims.SingleOrDefault(c => c.ClaimName == "User")
            };

            UserClaim positionClaim = new UserClaim
            {
                User = user,
                Claim = DbClaims.SingleOrDefault(c => c.ClaimName == "MD")
            };

            // Add UserClaims to List of UserClaims in User (Navigation Properties)
            user.UserClaims.Add(roleClaim);
            user.UserClaims.Add(positionClaim);

            _context.Add(user);

            await _context.SaveChangesAsync();

            return new Result
            {
                IsSuccess = true,
                Message = "Successfully Registered"
            };
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
