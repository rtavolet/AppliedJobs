using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppliedJobs.Models;

namespace AppliedJobs.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AppliedJobs.Models.JobApp> JobApp { get; set; } = default!;
        public DbSet<AppliedJobs.Models.Recruiter> Recruiter { get; set; } = default!;
    }
}