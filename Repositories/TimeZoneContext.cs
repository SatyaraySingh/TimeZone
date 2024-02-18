using Microsoft.EntityFrameworkCore;
using TimeZone.Entities;

namespace TimeZone.Repositories
{
    public class TimeZoneContext: DbContext
    {
        public TimeZoneContext(DbContextOptions<TimeZoneContext> options) : base(options) { }

        public DbSet<Guest> Guests { get; set; }
    }
}
