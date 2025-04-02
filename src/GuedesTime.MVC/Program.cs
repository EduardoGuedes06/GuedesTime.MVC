using GuedesTime.Configurations;
using GuedesTime.Data.Context;
using GuedesTime.MVC.Configurations;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using static GuedesTime.MVC.Configurations.HealthChecksConfig;

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.ResolveDependencies();

builder.Services.AddIdentityConfiguration(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString("connection");

builder.Services.AddDbContext<MeuDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);


//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMvcConfiguration();
builder.Services.AddControllersWithViews();

builder.Services.AddHealthChecksConfig(builder.Configuration);

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/erro/500");
    app.UseStatusCodePagesWithRedirects("/erro/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configuração do Health Check
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHealthChecks("/Saude", new HealthCheckOptions()
{
    ResponseWriter = HealthChecksResponseWriter.WriteResponse 
});
app.UseHealthChecks("/Saude/db", new HealthCheckOptions()
{
    Predicate = check => check.Tags.Contains("database"),
    ResponseWriter = HealthChecksConfig.HealthChecksResponseWriter.WriteResponse
});

app.UseHealthChecks("/Saude/system", new HealthCheckOptions()
{
    Predicate = check => check.Tags.Contains("system"),
    ResponseWriter = HealthChecksConfig.HealthChecksResponseWriter.WriteResponse
});


app.UseHealthChecksUI(options => options.UIPath = "/Saude-ui");

app.MapRazorPages();

app.Run();
