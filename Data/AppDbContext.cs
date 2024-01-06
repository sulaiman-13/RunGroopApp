using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunGroopApp.Models;

namespace RunGroopApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Address> Addresses { get; set; }
		public DbSet<Club> Clubs { get; set; }
		public DbSet<Race> Races { get; set; }
	}
}

