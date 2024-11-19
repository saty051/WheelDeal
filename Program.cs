using WheelDeal.AppDBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WheelDealDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    // The default HSTS value is 30 days. Consider changing for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Add custom route
//app.MapControllerRoute(
//    name: "ByYearMonth",
//    pattern: "make/bikes/{year:int:length(4)}/{month:int:range(1,12)}",
//    defaults: new { controller = "Make", action = "ByYearMonth" }
//);

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
