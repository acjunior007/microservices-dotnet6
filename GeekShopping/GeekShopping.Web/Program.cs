using GeekShopping.Web.Services;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IProductService, ProductService>(
	c => c.BaseAddress =
		new Uri(builder.Configuration["ServiceUrls:ProductAPI"])
	);

builder.Services.AddAuthentication(c =>
{
	c.DefaultScheme = "Cookie";
	c.DefaultChallengeScheme = "oidc";

})
	.AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
	.AddOpenIdConnect("oidc", c =>
	{
		c.Authority = builder.Configuration["ServicesUrls:IdentityServer"];
		c.GetClaimsFromUserInfoEndpoint = true;
		c.ClientId = "geek_shopping";
		c.ClientSecret = "key_secrect_super";
		c.ResponseType = "code";
		c.ClaimActions.MapJsonKey("role", "role", "role");
		c.ClaimActions.MapJsonKey("sub", "sub", "sub");
		c.TokenValidationParameters.NameClaimType = "name";
		c.TokenValidationParameters.RoleClaimType = "role";
		c.Scope.Add("geek_shopping");
		c.SaveTokens = true;
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
