using exp.net6.backend.Helpers;
using exp.NET6.Models.Helpers;
using exp.NET6.Services.Email;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.Extensions;
using exp.NET6.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using exp.NET6.Infrastructure.Context;
using exp.net6.backend.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using exp.NET6.Infrastructure.Repositories.UserLocations;
using exp.NET6.Services.DBServices.UserService.UserLocationService;
using exp.NET6.Infrastructure.Repositories.User;
using exp.NET6.Services.DBServices.UserService;
using exp.NET6.Services.DBServices;
using exp.NET6.Infrastructure.Repositories.TeamCategories;
using exp.NET6.Services.DBServices.TeamCategoryService;
using exp.NET6.Infrastructure.Repositories.PlayerHistories;
using exp.NET6.Services.DBServices.PlayerHistoryService;
using exp.NET6.Infrastructure.Repositories.StaffCategories;
using exp.NET6.Infrastructure.Repositories.Staffs;
using exp.NET6.Services.DBServices.StaffService;
using exp.NET6.Services.DBServices.PlayerService;
using exp.NET6.Infrastructure.Repositories.PlayerCategories;
using exp.NET6.Infrastructure.Repositories.Players;
using exp.NET6.Infrastructure.Repositories.ArticleGallerys;
using exp.NET6.Services.DBServices.ArticleService;
using exp.NET6.Infrastructure.Repositories.Articles;
using exp.NET6.Services.DBServices.ArticleGallerys;
using exp.NET6.Infrastructure.Repositories.PlayerGalleries;
using exp.NET6.Services.DBServices.PlayerGalleryService;
using exp.NET6.Infrastructure.Repositories.ClubDetails;
using exp.NET6.Services.DBServices.ClubDetailsService;
using exp.NET6.Infrastructure.Repositories.Sponsors;
using exp.NET6.Services.DBServices.SponsorService;
using exp.NET6.Infrastructure.Repositories.NextMatches;
using exp.NET6.Services.DBServices.NextMatchService;
using exp.NET6.Infrastructure.Repositories.Matches;
using exp.NET6.Services.DBServices.MatchService;
using exp.NET6.Infrastructure.Repositories.Competitions;
using exp.NET6.Services.DBServices.CompetitionService;
using exp.NET6.Infrastructure.Repositories.Trophies;
using exp.NET6.Services.DBServices.TrophiesService;
using exp.NET6.Infrastructure.Repositories.TeamRanking;
using exp.NET6.Services.DBServices.TeamRankingService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FlowerPowerDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(
            builder.Configuration.GetConnectionString("WebApiDatabase"))
    );

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
})
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AuthDbContext>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddCors();
builder.Services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddMemoryCache();



//configure DI for application services
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IArticleGalleryRepository, ArticleGalleryRepository>();
builder.Services.AddScoped<IArticleGalleryService, ArticleGalleryService>();

builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleService, ArticleService>();

builder.Services.AddScoped<IGenericService, GenericService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserLocationRepository, UserLocationRepository>();
builder.Services.AddScoped<IUserLocationService, UserLocationService>();

builder.Services.AddScoped<ITeamCategoryRepository, TeamCategoryRepository>();
builder.Services.AddScoped<ITeamCategoryService, TeamCategoryService>();

builder.Services.AddScoped<IPlayerHistoryRepository, PlayerHistoryRepository>();
builder.Services.AddScoped<IPlayerHistoryService,  PlayerHistoryService>();

builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IStaffService,  StaffService>();

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddScoped<IPlayerGalleryRepository, PlayerGalleryRepository>();
builder.Services.AddScoped<IPlayerGalleryService, PlayerGalleryService>();

builder.Services.AddScoped<IClubDetailsRepository, ClubDetailsRepository>();
builder.Services.AddScoped<IClubDetailsService, ClubDetailsService>();

builder.Services.AddScoped<ISponsorRepository, SponsorRepository>();
builder.Services.AddScoped<ISponsorService, SponsorService>();

builder.Services.AddScoped<INextMatchRepository, NextMatchRepository>();
builder.Services.AddScoped<INextMatchService, NextMatchService>();

builder.Services.AddScoped<ICompetitionRepository, CompetitionRepository>();
builder.Services.AddScoped<ICompetitionService, CompetitionService>();

builder.Services.AddScoped<IMatchRepository, MatchRepository>();
builder.Services.AddScoped<IMatchService, MatchService>();

builder.Services.AddScoped<ITrophiesRepository, TrophiesRepository>();
builder.Services.AddScoped<ITrophiesService, TrophiesService>();


builder.Services.AddScoped<ITeamRankingRepository, TeamRankingRepository>();
builder.Services.AddScoped<ITeamRankingService, TeamRankingService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//global cors policy
app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

app.UseMiddleware<ErrorHandlerMiddleware>();


app.MapControllers();

app.Run();
