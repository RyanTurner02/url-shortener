using Url.Shortener.Features.ShortUrls;
using Url.Shortener.Features.ShortUrls.Create;
using Url.Shortener.Features.ShortUrls.Get;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Url.Shortener.Features.ShortUrls.Common;
using Url.Shortener.Features.ShortUrls.Create.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ShortUrlDbContext>(opt => opt.UseInMemoryDatabase("ShortUrl"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<IUrlRandomizer, UrlRandomizer>();
builder.Services.AddScoped<IUrlShortenerService, UrlShortenerService>();
builder.Services.AddScoped<IShortUrlRepository, ShortUrlRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.RegisterCreateShortUrlEndpoint();
app.RegisterGetShortUrlEndpoint();

app.Run();

public partial class Program { }