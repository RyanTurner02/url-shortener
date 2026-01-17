using Microsoft.EntityFrameworkCore;
using Testcontainers.MySql;
using Url.Shortener.Features.ShortUrls;

namespace Url.Shortener.Tests.Infrastructure
{
    /// <summary>
    /// Class for the MySQL test container.
    /// </summary>
    public class MySqlContainerFixture : IAsyncLifetime
    {
        /// <summary>
        /// The MySQL test container.
        /// </summary>
        private readonly MySqlContainer _mySqlContainer = new MySqlBuilder("mysql:8.0")
            .WithDatabase("testdb")
            .WithUsername("testuser")
            .WithPassword("testpassword")
            .WithCleanUp(true)
            .Build();

        /// <summary>
        /// Gets/Private Sets the database connection string.
        /// </summary>
        public string ConnectionString => _mySqlContainer.GetConnectionString();

        /// <summary>
        /// Starts the test container.
        /// </summary>
        public async Task InitializeAsync()
        {
            await _mySqlContainer.StartAsync();
            await ApplyMigrationsAsync();
        }

        /// <summary>
        /// Cleans up the test container.
        /// </summary>
        public async Task DisposeAsync()
        {
            await _mySqlContainer.DisposeAsync();
        }

        /// <summary>
        /// Creates a new instance of the ShortUrlDbContext for accessing the ShortUrl database.
        /// </summary>
        /// <returns>The ShortUrlDbContext.</returns>
        public ShortUrlDbContext CreateShortUrlDbContext()
        {
            var options = new DbContextOptionsBuilder<ShortUrlDbContext>()
                .UseMySql(
                    ConnectionString,
                    ServerVersion.AutoDetect(ConnectionString))
                .Options;

            return new ShortUrlDbContext(options);
        }

        /// <summary>
        /// Applies the database migrations.
        /// </summary>
        private async Task ApplyMigrationsAsync()
        {
            await using var context = CreateShortUrlDbContext();
            await context.Database.MigrateAsync();
        }
    }
}