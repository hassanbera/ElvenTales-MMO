using ElvenTales.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext for dependency injection.
builder.Services.AddDbContext<ElvenTalesDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session services.
builder.Services.AddSession();  // IMPORTANT: This registers the session services.

var app = builder.Build();

// Use session middleware.
app.UseSession();  // Session middleware must be added after session registration.

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();  // Optional, based on your needs for authentication and authorization.

// Configure default route mapping.
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();