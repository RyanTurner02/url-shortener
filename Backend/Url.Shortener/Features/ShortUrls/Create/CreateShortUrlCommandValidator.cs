using FluentValidation;

namespace Url.Shortener.Features.ShortUrls.Create
{
    /// <summary>
    /// The CreateShortUrlCommand validator class.
    /// </summary>
    public class CreateShortUrlCommandValidator : AbstractValidator<CreateShortUrlCommand>
    {
        /// <summary>
        /// Constructor for adding the CreateShortUrlCommand validation rules.
        /// </summary>
        public CreateShortUrlCommandValidator()
        {
            RuleFor(x => x.Url).NotEmpty();
            RuleFor(x => x.Url).MaximumLength(128).WithMessage("URL must not exceed 128 characters.");
            RuleFor(x => x.Url).Must(BeAValidUrl).WithMessage("Invalid URL.");
        }

        /// <summary>
        /// Checks if a given string is a valid web URL.
        /// </summary>
        /// <param name="url">The original URL.</param>
        /// <returns>True if the string is a valid web URL, false otherwise.</returns>
        private static bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
