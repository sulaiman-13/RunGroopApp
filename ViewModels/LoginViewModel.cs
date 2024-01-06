﻿using System.ComponentModel.DataAnnotations;

namespace RunGroopApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
