using Microsoft.AspNetCore.Identity;

namespace todoListApi.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}