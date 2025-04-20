using Microsoft.AspNetCore.Identity;

namespace PastebinClone.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; private set; }

        public User()
        {
        }

        public User(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
            UserName = email;
        }
    }
}
