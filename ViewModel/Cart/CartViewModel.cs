using DatingApp.Models;

namespace DatingApp.ViewModel.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Amount { get; set; }
    }
}
