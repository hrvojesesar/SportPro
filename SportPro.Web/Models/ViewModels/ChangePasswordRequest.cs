using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class ChangePasswordRequest
{
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string CurrentPassword { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Neispravan format lozinke!", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword { get; set; }
    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = "Lozinke se moraju podudarati.")]
    public string ConfirmNewPassword { get; set; }
}
