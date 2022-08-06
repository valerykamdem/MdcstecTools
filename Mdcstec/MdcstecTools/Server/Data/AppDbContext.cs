using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MdcstecTools.Server.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }
    }
}
