using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditUserRequest
{
    [Key]
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
