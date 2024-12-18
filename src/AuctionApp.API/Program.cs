using AuctionApp.API.Contracts;
using AuctionApp.API.Filters;
using AuctionApp.API.Repositories;
using AuctionApp.API.Repositories.DataAccess;
using AuctionApp.API.Services;
using AuctionApp.API.UseCases.Auctions.GetCurrent;
using AuctionApp.API.UseCases.Offers.CreateOffer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer Scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer 12345derfgh'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
        new List<string>()
        }
    });
});

builder.Services.AddScoped<AuthenticationUserAttribute>();
builder.Services.AddScoped<ILoggedUser, LoggedUser>();
builder.Services.AddScoped<CreateOfferUseCase>();
builder.Services.AddScoped<GetCurrentAuctionUseCase>();
builder.Services.AddScoped<IAuctionRepository, AuctionRepository> ();
builder.Services.AddScoped<IOfferRepository, OfferRepository> ();
builder.Services.AddScoped<IUserRepository, UserRepository> ();

builder.Services.AddDbContext<AuctionAppDbContext>(options =>
{
    options.UseSqlite(@"Data Source=C:\\Databases\\SQLite\\leilaoDbNLW.db");
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
