using Microsoft.EntityFrameworkCore;

namespace Backend.Features.ShortUrls
{
    public class ShortUrlDbContext : DbContext
    {
        public DbSet<ShortUrl> ShortUrls { get; set; }

        public ShortUrlDbContext(DbContextOptions options) : base(options) { }
    }
}
