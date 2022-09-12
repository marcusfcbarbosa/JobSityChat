using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobSity.Identity.API.Data
{
    public class JobSityContext : IdentityDbContext
    {
        public JobSityContext(DbContextOptions<JobSityContext> options) : base(options) { }
    }
}
