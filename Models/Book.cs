using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class Book
    {
        public Book()
        { }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Slug {  get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.Now;

        public string Description { get; set; }

        public bool Private { get; set; } = false;
        //Navigation Properties
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public IList<Image>? Images { get; set; }
        //
        public IList<Comment>? Comments { get; set; }
    }
}
