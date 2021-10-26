using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class RegisterInputModel
    {
    
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(3)]
        public string FullName { get; set; }

        [MinLength(3)]
        public string Password { get; set; }

        [MinLength(3)]
         [Compare("Password",  ErrorMessage = "The Password and Confirm Password fields do not match.")]
        public string PasswordConfirmation { get; set; }
    }
}