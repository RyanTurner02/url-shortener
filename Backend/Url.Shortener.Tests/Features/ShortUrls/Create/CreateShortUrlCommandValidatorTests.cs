using FluentValidation.TestHelper;
using Url.Shortener.Features.ShortUrls.Create;

namespace Url.Shortener.Tests.Features.ShortUrls.Create
{
    /// <summary>
    /// Tests the <see cref="CreateShortUrlCommandValidator"/> class.
    /// </summary>
    public sealed class CreateShortUrlCommandValidatorTests
    {
        /// <summary>
        /// Tests valid URLs.
        /// </summary>
        /// <param name="url">The URL.</param>
        [Theory]
        [InlineData("http://example.com/")]
        [InlineData("https://example.com/")]
        [InlineData(" https://example.com/")]
        [InlineData("https://example.com/ ")]
        [InlineData(" https://example.com/ ")]
        public async Task Validate_ValidUrls_ReturnsTrue(string url)
        {
            var command = new CreateShortUrlCommand(url);
            var validator = new CreateShortUrlCommandValidator();

            var result = await validator.ValidateAsync(command);

            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Tests invalid URLs.
        /// </summary>
        /// <param name="url">The URL.</param>
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("example")]
        [InlineData("example.com")]
        [InlineData("www.example.com")]
        public async Task Validate_InvalidUrls_ReturnsFalse(string url)
        {
            var command = new CreateShortUrlCommand(url);
            var validator = new CreateShortUrlCommandValidator();

            var result = await validator.ValidateAsync(command);

            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Tests the URL exceeding the maximum length.
        /// </summary>
        [Fact]
        public async Task Validate_ExceedingMaxLength_ReturnsFalse()
        {
            var maxLength = 128;
            var baseUrl = "https://example.com/";
            var urlPath = new string('a', maxLength - baseUrl.Length + 1);
            var url = $"{baseUrl}{urlPath}";

            var command = new CreateShortUrlCommand(url);
            var validator = new CreateShortUrlCommandValidator();

            var result = await validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Url.Trim());
        }
    }
}
