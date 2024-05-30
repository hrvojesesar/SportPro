using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;

public class Users
{
    [Key]
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
}
