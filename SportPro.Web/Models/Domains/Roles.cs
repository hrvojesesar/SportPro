using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;

public class Roles
{
    [Key]
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public bool Selected { get; set; }
}
