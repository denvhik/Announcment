using Microsoft.EntityFrameworkCore;

namespace Announcment.DataContext
{
    public class AnnouncmentContext : DbContext
    {
        public AnnouncmentContext()
        {
        }

        public AnnouncmentContext(DbContextOptions<AnnouncmentContext> options) : base(options)
        {
        }

        public DbSet<AnnouncmentJob> AnnouncmentJob { get; set; }
    }
}
