using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization;

namespace BMStore.Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    //public string PhoneNumber { get; set; }
    //public string AuthenticatorKey { get; set; }
    public bool IsEnabled { get; set; }
    public bool IsDeleted { get; set; }

    [IgnoreDataMember]
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
}
