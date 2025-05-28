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

            //_context.Add(new User
            //{
            //    UserId = 1,
            //    Email = "John@email.com",
            //    Name = "John",
            //    Password = "HfnHBpXC/v6r1CpPNztP3J7u+IyTrpPwgdVge1V3d+E=",
            //    Salt = "9pcyevxF1uUR9pPh1dV7lc1yEDcMOYArZXLw3F+nNZc=",
            //    IsApproved = true,
            //    Reports = new List<Report>()
            //    {
            //        new Report()
            //        {
            //            ReportId = 1,
            //            CreatedByUserId = 1,
            //            ReportName = "2024 Q1 Report for company X",
            //            FromDateRange = new DateTime(2024, 1, 1),
            //            ToDateRange = new DateTime(2024, 3, 31),
            //            CreatedDate = new DateTime(2024, 2, 20),
            //            IsDraft = false,
            //        }
            //    }
            //});

            _context.SaveChanges();
        }

    }
}
