using Microsoft.EntityFrameworkCore;

namespace Url.Shortener.Features.ShortUrls
{
    /// <summary>
    /// The Entity Framework database context for short URL operations.
    /// </summary>
    public class ShortUrlDbContext : DbContext
    {
        /// <summary>
        /// Gets/Sets the <see cref="DbSet{ShortUrl}"/> representing the short URLs table.
        /// </summary>
        /// <remarks>
        /// Use this property to query, insert, update, or delete <see cref="ShortUrl"/> entities in the database.
        /// </remarks>
        public virtual DbSet<ShortUrl> ShortUrls { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortUrlDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">
        /// The configuration options for the database context, including provider and connection details.
        /// </param>
        public ShortUrlDbContext(DbContextOptions<ShortUrlDbContext> options) : base(options) { }
    }
}
