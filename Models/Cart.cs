using DatingApp.Entities;

namespace DatingApp.Models
{
    public class Cart
    {
        public Cart()
        {
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int Amount { get; set; } = 0;
        public bool Status { get; set; } = false;  
    }
}
