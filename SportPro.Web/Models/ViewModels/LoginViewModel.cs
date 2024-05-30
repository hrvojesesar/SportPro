using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
