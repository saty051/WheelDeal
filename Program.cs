using WheelDeal.AppDBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Add Razor Pages services

// Configure DbContext for Entity Framework and Identity
builder.Services.AddDbContext<WheelDealDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Change to true if email confirmation is required
})
    .AddRoles<IdentityRole>() // Enable role management if needed
    .AddEntityFrameworkStores<WheelDealDbContext>()
    .AddDefaultTokenProviders();

// Safely register the length constraint
builder.Services.Configure<RouteOptions>(options =>
{
    if (!options.ConstraintMap.ContainsKey("length"))
    {
        options.ConstraintMap.Add("length", typeof(Microsoft.AspNetCore.Routing.Constraints.LengthRouteConstraint));
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map Razor Pages (required for Identity areas like Register/Login)
app.MapRazorPages();

// Add custom route (optional, commented for now)
// app.MapControllerRoute(
//     name: "ByYearMonth",
//     pattern: "make/bikes/{year:int:length(4)}/{month:int:range(1,12)}",
//     defaults: new { controller = "Make", action = "ByYearMonth" }
// );

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
