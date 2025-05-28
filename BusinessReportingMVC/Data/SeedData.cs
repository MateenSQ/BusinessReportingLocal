using BusinessReportingMVC.Models;

namespace BusinessReportingMVC.Data
{
    public class SeedData
    {
        private readonly BusinessReportingDbContext _context;
        public SeedData(BusinessReportingDbContext Context)
        {
            _context = Context;
        }
    
        public void InitializeAsync()
        {
            // Avoid double seeding (if possible with in-memory)
            if (_context.Claims.Any())
            {
                return;
            }

            _context.Add(new Claim
            {
                ClaimId = 1,
                ClaimType = "Role",
                ClaimName = "Admin"
            });

            _context.Add(new Claim
            {
                ClaimId = 2,
                ClaimType = "Role",
                ClaimName = "User"
            });

            _context.Add(new Claim
            {
                ClaimId = 3,
                ClaimType = "Position",
                ClaimName = "Seacht"
            });

            _context.Add(new Claim
            {
                ClaimId = 4,
                ClaimType = "Position",
                ClaimName = "MD"
            });

            _context.Add(new User
            {
                UserId = 1,
                Email = "John@email.com",
                Name = ""
            });

            _context.SaveChanges();
        }

    }
}
