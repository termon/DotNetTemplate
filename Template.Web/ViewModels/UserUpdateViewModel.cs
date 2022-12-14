using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Template.Core.Models;

namespace Template.Web.ViewModels
{
    public class UserUpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
 
        [Required]
        [EmailAddress]
        [Remote(action: "GetUserByEmailAddress", controller: "User")]
        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string PasswordConfirm  { get; set; }

        [Required]
        public Role Role { get; set; }

    }
}