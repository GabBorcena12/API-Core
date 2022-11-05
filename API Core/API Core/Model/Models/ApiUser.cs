using Microsoft.AspNetCore.Identity;

namespace API_Core.Model.Models
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
