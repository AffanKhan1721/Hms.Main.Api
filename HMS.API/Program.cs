using HMS.API.Data;
using HMS.API.Repositories.Implementations;
using HMS.API.Repositories.Interfaces;
using HMS.API.Services;
using HMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Configure EF Core DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=ABU-HURAIRA\\SQLEXPRESS;Database=HmsDb;Trusted_Connection=True;Encrypt=False;";

builder.Services.AddDbContext<HmsDbContext>(options =>
    options.UseSqlServer(connectionString,
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(typeof(HmsDbContext).Assembly.FullName);
        }));

// DI registrations for repositories and services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddSingleton<PasswordHasher>();

// Add HTTP client for Rates API
builder.Services.AddHttpClient("RatesAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7282/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Add Redis caching
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
    options.InstanceName = "HMS";
});

// Service registrations
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IRatesService, RatesService>();
builder.Services.AddScoped<ICacheService, CacheService>();

// Background service for room status updates

// Configure Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
        options.LoginPath = "/api/auth/login";
        options.LogoutPath = "/api/auth/logout";
        options.AccessDeniedPath = "/api/auth/access-denied";
        options.Cookie.Name = "HMS.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HmsDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


