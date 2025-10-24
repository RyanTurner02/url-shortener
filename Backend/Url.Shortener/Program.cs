using Url.Shortener.Features.ShortUrls;
using Url.Shortener.Features.ShortUrls.Create;
using Url.Shortener.Features.ShortUrls.Get;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ShortUrlDbContext>(opt => opt.UseInMemoryDatabase("ShortUrl"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<ICreateShortUrlRepository, CreateShortUrlRepository>();
builder.Services.AddScoped<IGetShortUrlRepository, GetShortUrlRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.RegisterCreateShortUrlEndpoint();
app.RegisterGetShortUrlEndpoint();

app.Run();

public partial class Program { }