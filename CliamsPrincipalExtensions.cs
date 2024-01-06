using System.Security.Claims;

namespace RunGroopApp
{
	public static  class CliamsPrincipalExtensions
	{
		public static string GetUserId(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}
