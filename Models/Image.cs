using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class Image
    {
        public int Id { get; set; }
#nullable enable
        public string Url { get; set; }
        public bool IsMain { get; set; } = false; //
#nullable disable
        public int BookId { get; set; }
        public Book Book { get; set; }

    }
}
