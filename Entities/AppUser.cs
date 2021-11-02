using DatingApp.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime DateOfBirth {  get; set; }
        public string KnownAs {  get; set; }
        public DateTime Created {  get; set; } = DateTime.Now;
        public DateTime LastActives {  get; set; } = DateTime.Now;
        public bool? Gender {  get; set; }

        public ICollection<Photo> Photos {  get; set; }
        public ICollection<AppUserRole> UserRoles {  get; set; }
        //public int GetAge()
        //{
        //    return DateOfBirth.CalculateAge();
        //}
    }
}
