using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WEBPROJECT.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Datacontext>(options=>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectedDb"]);
}
);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
    options.Cookie.IsEssential = true;
}
);

var app = builder.Build();
app.UseSession();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var context=app.Services.CreateScope().ServiceProvider.GetRequiredService<Datacontext>();
SeedData.SeedingData(context);
app.Run();
