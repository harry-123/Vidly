using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels 
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

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
