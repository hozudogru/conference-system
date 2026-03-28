using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Services;
using Microsoft.EntityFrameworkCore;
using ConferenceSystem.Web.Models;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.IdleTimeout = TimeSpan.FromHours(8);
});

builder.Services.AddScoped<LetterService>();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.Configure<EditorAccountSettings>(builder.Configuration.GetSection("EditorAccount"));
builder.Services.AddScoped<EmailService>();
builder.Services.Configure<AdminNotificationSettings>(
    builder.Configuration.GetSection("AdminNotificationSettings"));
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<SmtpSettingsService>();
builder.Services.AddScoped<SiteSettingsService>();
//builder.Services.AddHostedService<ReminderBackgroundService>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuditService>();
builder.Services.AddScoped<ProgramBookletService>();
builder.Services.AddScoped<PdfService>();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.Use(async (context, next) =>
{
    var path = context.Request.Path;

    if (path.StartsWithSegments("/Admin", StringComparison.OrdinalIgnoreCase))
    {
        var isAuthenticated = context.Session.GetString("EditorAuthenticated") == "true";
        if (!isAuthenticated)
        {
            var returnUrl = context.Request.Path + context.Request.QueryString;
            var loginUrl = $"/AdminAuth/Login?returnUrl={Uri.EscapeDataString(returnUrl)}";
            context.Response.Redirect(loginUrl);
            return;
        }
    }

    await next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "pages",
    pattern: "{slug}",
    defaults: new { controller = "Pages", action = "Index" });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
