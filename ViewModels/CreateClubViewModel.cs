﻿using RunGroopApp.Data.Enums;
using RunGroopApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroopApp.ViewModels
{
	public class CreateClubViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public IFormFile Image { get; set; }
		public Address Address { get; set; }
		public ClubCategory ClubCategory { get; set; }
		public string AppUserId { get; set; }
	}
}
