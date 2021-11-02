using DatingApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.ViewModel.BookViewModel
{
    public class UpdateBookViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Slug { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
        public DateTime? PublicationDate { get; set; } = DateTime.Now;

        public string Description { get; set; }

        public bool? Private { get; set; } = false;
        //Navigation Properties

        public List<IFormFile> Images { get; set; }
        [ForeignKey("id")]
        public int CategoryId { get; set; }
    }
}
