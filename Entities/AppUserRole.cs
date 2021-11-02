using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.Entities
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser User {  get; set; }
        public AppRole Role {  get; set; }
    }
}
