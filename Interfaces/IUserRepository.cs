﻿using RunGroopApp.Models;

namespace RunGroopApp.Interfaces
{
	public interface IUserRepository
	{
		Task<IEnumerable<AppUser>> GetAllUsers();
		Task<AppUser> GetUserById(string userId);
		bool Add(AppUser user);
		bool Update(AppUser user);
		bool Delete(AppUser user);
		bool Save();
	}
}
