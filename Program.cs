using BartenderApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("BartenderDb"));
builder.Services.AddScoped<BartenderLogic>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    if (!context.Cocktails.Any())
    {
        context.Cocktails.AddRange(
            new Cocktail { Name = "Old Fashioned", Description = "Bourbon, Bitters, Sugar" },
            new Cocktail { Name = "Margarita", Description = "Tequila, Lime, Cointreau" }
        );
        context.SaveChanges();
    }
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();