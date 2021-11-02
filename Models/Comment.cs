using DatingApp.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; } = 2.5;
        //
        public int BookId { get; set; }
        public Book Book { get; set; }
        //
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
