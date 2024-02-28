using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Initializer
{
	public class DbInitializer : IDbInitializer
	{
		private readonly MySQLContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public DbInitializer(MySQLContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public void Initialize()
		{
			if (_roleManager.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;


			_roleManager.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

			#region Admin
			ApplicationUser admin = new ApplicationUser()
			{
				UserName = "ac-junior-admin",
				Email = "ac-junior@gmail.com",
				EmailConfirmed = true,
				PhoneNumber = "+55 (21) 92354-2415",
				FirstName = "Ac",
				LastName = "Junior"
			};

			_userManager.CreateAsync(admin, "Ac-Junior-007@").GetAwaiter().GetResult();
			_userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();
			var adminClaim = _userManager.AddClaimsAsync(admin, new Claim[]
			{
				new Claim(JwtClaimTypes.Name, $"{admin.FirstName} ${admin.LastName}"),
				new Claim(JwtClaimTypes.GivenName, $"{admin.FirstName}"),
				new Claim(JwtClaimTypes.FamilyName, $"{admin.LastName}"),
				new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
			}).Result;
			#endregion

			#region User

			ApplicationUser client = new ApplicationUser()
			{
				UserName = "ac-junior-client",
				Email = "ac-junior@gmail.com",
				EmailConfirmed = true,
				PhoneNumber = "+55 (21) 92354-2415",
				FirstName = "Ac",
				LastName = "Junior"
			};

			_userManager.CreateAsync(client, "Ac-Junior-007@").GetAwaiter().GetResult();
			_userManager.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();
			var clientClaim = _userManager.AddClaimsAsync(client, new Claim[]
			{
				new Claim(JwtClaimTypes.Name, $"{client.FirstName} ${admin.LastName}"),
				new Claim(JwtClaimTypes.GivenName, $"{client.FirstName}"),
				new Claim(JwtClaimTypes.FamilyName, $"{client.LastName}"),
				new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),
			}).Result;

			#endregion
		}
	}
}
