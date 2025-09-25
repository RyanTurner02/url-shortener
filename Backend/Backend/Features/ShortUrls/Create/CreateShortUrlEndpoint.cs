using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Features.ShortUrls.Create
{
    public static class CreateShortUrlEndpoint
    {
        public static void RegisterCreateShortUrlEndpoint(this WebApplication app)
        {
            app.MapPost("/", CreateShortUrl);
        }

        private static async Task<Created> CreateShortUrl(CreateShortUrlCommand createShortUrlCommand, ISender sender, CancellationToken ct)
        {
            var shortUrl = await sender.Send(createShortUrlCommand, ct);
            return TypedResults.Created(shortUrl);
        }
    }
}
