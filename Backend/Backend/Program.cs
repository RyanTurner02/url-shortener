using Backend.Features.ShortUrls;
using Backend.Features.ShortUrls.Create;
using Backend.Features.ShortUrls.Get;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShortUrlDbContext>(opt => opt.UseInMemoryDatabase("ShortUrl"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<ICreateShortUrlRepository, CreateShortUrlRepository>();

var app = builder.Build();

app.RegisterCreateShortUrlEndpoint();
app.RegisterGetShortUrlEndpoint();

app.Run();