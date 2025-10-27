﻿using MediatR;

namespace Url.Shortener.Features.ShortUrls.Create
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
            app.MapPost("/", CreateShortUrl)
                .WithTags("Create Short URL")
                .WithSummary("Creates a shortened URL from a URL.")
                .Produces(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Handles the request for creating shortened URLs.
        /// </summary>
        /// <param name="createShortUrlCommand">The create short URL command.</param>
        /// <param name="sender">The sender.</param>
        /// <returns>The status of creating the short URL.</returns>
        private static async Task<IResult> CreateShortUrl(
            CreateShortUrlCommand createShortUrlCommand,
            ISender sender,
            HttpRequest req)
        {
            var shortUrlHash = await sender.Send(createShortUrlCommand);    
            var fullUrl = $"{req.Scheme}://{req.Host}{req.Path}{shortUrlHash}";

            return TypedResults.Created(req.Path, new ShortUrlResponse(fullUrl));
        }
    }
}
