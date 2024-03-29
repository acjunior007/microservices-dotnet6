using Duende.IdentityServer.Services;
using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Initializer;
using GeekShopping.IdentityServer.Model.Context;
using GeekShopping.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MySQLContext>(options =>
	options.UseMySql(
		builder.Configuration.GetConnectionString("mySqlConn"),
		new MySqlServerVersion(new Version(8, 0, 5)))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<MySQLContext>()
	.AddDefaultTokenProviders()
	;

builder.Services.AddIdentityServer(options =>
{
	options.Events.RaiseErrorEvents = true;
	options.Events.RaiseInformationEvents = true;
	options.Events.RaiseFailureEvents = true;
	options.Events.RaiseSuccessEvents = true;
	options.EmitStaticAudienceClaim = true;
})
	.AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
	.AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
	.AddInMemoryClients(IdentityConfiguration.Clients)
	.AddAspNetIdentity<ApplicationUser>()
	;

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IProfileService, ProfileService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//ilder.Services.AddDefaultIdentity

var app = builder.Build();

var scope = app.Services.CreateScope();
var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

dbInitializer.Initialize();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
