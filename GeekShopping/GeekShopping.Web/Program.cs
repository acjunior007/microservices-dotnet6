using GeekShopping.Web.Services;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Services.AddAuthentication(c =>
//{
//	c.DefaultScheme = "Cookie";
//	c.DefaultChallengeScheme = "oidc";

//})
//	.AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
//	.AddOpenIdConnect("oidc", c =>
//	{
//		c.Authority = builder.Configuration["ServicesUrls:IdentityServer"];
//		c.GetClaimsFromUserInfoEndpoint = true;
//		c.ClientId = "geek_shopping";
//		c.ClientSecret = "key_secrect_super";
//		c.ResponseType = "code";
//		c.ClaimActions.MapJsonKey("role", "role", "role");
//		c.ClaimActions.MapJsonKey("sub", "sub", "sub");
//		c.TokenValidationParameters.NameClaimType = "name";
//		c.TokenValidationParameters.RoleClaimType = "role";
//		c.Scope.Add("geek_shopping");
//		c.SaveTokens = true;
//	});
var configuration = builder.Configuration["ServiceUrls:IdentityServer"];
builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = "Cookies";
	options.DefaultChallengeScheme = "oidc";
})
	.AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
	.AddOpenIdConnect("oidc", options =>
	{
		options.Authority = "https://localhost:4435";
		options.GetClaimsFromUserInfoEndpoint = true;
		options.ClientId = "geek_shopping";
		options.ClientSecret = "key_secrect_super";
		options.ResponseType = "code";
		options.ClaimActions.MapJsonKey("role", "role", "role");
		options.ClaimActions.MapJsonKey("sub", "sub", "sub");
		options.TokenValidationParameters.NameClaimType = "name";
		options.TokenValidationParameters.RoleClaimType = "role";
		options.Scope.Add("geek_shopping");
		options.SaveTokens = true;
	}
);

builder.Services.AddHttpClient<IProductService, ProductService>(
	c => c.BaseAddress =
		new Uri(builder.Configuration["ServiceUrls:ProductAPI"])
	);

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
