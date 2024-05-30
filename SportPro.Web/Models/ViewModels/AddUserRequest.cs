using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddUserRequest
{
    [Key]
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
