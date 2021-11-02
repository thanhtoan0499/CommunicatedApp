using System.Collections.Generic;

namespace DatingApp.Models
{
    public class Category
    {
        public Category()
        {
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Book> Books { get; set; }
    }
}
