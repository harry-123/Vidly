using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Driving License")]
        [StringLength(255)]
        public string DrivingLicense { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression("^[1-9][0-9]{9}$", ErrorMessage = "Phone Number should be a 10-digit number.")]
        public string PhoneNumber { get; set; }
    }
}