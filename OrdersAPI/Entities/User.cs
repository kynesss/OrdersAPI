using Microsoft.AspNetCore.Identity;

namespace OrdersAPI.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}