using RunGroopApp.Models;

namespace RunGroopApp.Interfaces
{
	public interface IDashBoardRepository
	{
		 Task<List<Race>> GetAllUserRaces();
		 Task<List<Club>> GetAllUserClubs();
		 Task<AppUser> GetUserById(string id);
		Task<AppUser> GetUserByIdNoTracking(string id);
		bool Update(AppUser user);
		bool Save();

    }
}
