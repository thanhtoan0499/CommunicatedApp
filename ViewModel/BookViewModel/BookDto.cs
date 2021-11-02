using DatingApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace DatingApp.ViewModel.BookViewModel
{
    public class BookDto
    {

        public string Name { get; set; }

        public string? Slug { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public DateTime? PublicationDate { get; set; } = DateTime.Now;

        public string Description { get; set; }

        public bool? Private { get; set; } = false;

        public int CategoryId { get; set; }

        //Navigation Properties
        public List<IFormFile> ImageFiles { get; set; }
    }
}
