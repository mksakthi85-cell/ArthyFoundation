using ArthyFoundation.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Razor Pages services
builder.Services.AddRazorPages();

// 2. Configure Database connection
builder.Services.AddDbContext<ArthyDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=ArthyFoundation.db"));

// ------------------------------------------------------------------
// ADD THESE LINES: Register Session Services (Must be before builder.Build())
// ------------------------------------------------------------------
builder.Services.AddDistributedMemoryCache(); // Required backing store for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session lasts for 30 minutes
    options.Cookie.HttpOnly = true; // Protects session cookie from client-side script hijacking
    options.Cookie.IsEssential = true; // Essential cookie for session functionality
});
// ------------------------------------------------------------------

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ------------------------------------------------------------------
// ADD THIS LINE: Enable Session Middleware
// (CRITICAL: Must call app.UseSession() AFTER UseRouting() and BEFORE UseAuthorization()!)
// ------------------------------------------------------------------
app.UseSession();
// ------------------------------------------------------------------

app.UseAuthorization();

app.MapRazorPages();

app.Run();