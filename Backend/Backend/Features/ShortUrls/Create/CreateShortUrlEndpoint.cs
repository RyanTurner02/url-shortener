using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Features.ShortUrls.Create
{
    /// <summary>
    /// Endpoint class for creating shortened URLs.
    /// </summary>
    public static class CreateShortUrlEndpoint
    {
        /// <summary>
        /// Registers the endpoint for creating shortened URLs.
        /// </summary>
        /// <param name="app">The web application.</param>
        public static void RegisterCreateShortUrlEndpoint(this WebApplication app)
        {
            app.MapPost("/", CreateShortUrl);
        }

        /// <summary>
        /// Handles the request for creating shortened URLs.
        /// </summary>
        /// <param name="createShortUrlCommand">The create short URL command.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The status of creating the short URL.</returns>
        private static async Task<Created<ShortUrlResponse>> CreateShortUrl(
            CreateShortUrlCommand createShortUrlCommand,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var shortUrl = await sender.Send(createShortUrlCommand, cancellationToken);
            var response = new ShortUrlResponse(shortUrl);

            return TypedResults.Created("/", response);
        }
    }
}
