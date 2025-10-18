using JadooTravel.Services.AdminServices;
using JadooTravel.Services.BookingStepServices;
using JadooTravel.Services.CategoryServices;
using JadooTravel.Services.DestinationServices;
using JadooTravel.Services.FeatureServices;
using JadooTravel.Services.ReservationServices;
using JadooTravel.Services.ServiceServices;
using JadooTravel.Services.TestimonialServices;
using JadooTravel.Services.TravelAiServices;
using JadooTravel.Services.TripPlanServices;
using JadooTravel.Settings;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDestinationService, DestinationService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<ITestimonialService, TestimonialService>();
builder.Services.AddScoped<ITripPlanService, TripPlanService>();
builder.Services.AddScoped<ITravelAiService, TravelAiService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettingsKey"));

builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddHttpClient();

builder.Services.AddMemoryCache();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 dk aktif kalır
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

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
app.UseSession();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var path = context.Request.Path.ToString().ToLower();

    // 👇 Sadece admin paneline özel login kontrolü
    bool isAdminPanel = path.StartsWith("/admin") || path.StartsWith("/dashboard");

    // 👇 Admin login sayfaları hariç (AdminLogin, logout vb.)
    bool isLoginPage = path.Contains("/adminlogin");

    // 👇 Public site (Default, jadoo, UI tarafı) hariç
    bool isPublicSite = path.StartsWith("/home") ||
                        path.StartsWith("/default") ||
                        path.Contains("/jadoo") ||
                        path.Contains("/assets") ||
                        path.Contains("/public") ||
                        path.Contains("/images") ||
                        path.Contains("/css") ||
                        path.Contains("/js");

    if (isAdminPanel && !isLoginPage)
    {
        var adminUser = context.Session.GetString("AdminUser");
        if (string.IsNullOrEmpty(adminUser))
        {
            context.Response.Redirect("/AdminLogin/Login");
            return;
        }
    }

    await next.Invoke();
});

app.MapStaticAssets();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
