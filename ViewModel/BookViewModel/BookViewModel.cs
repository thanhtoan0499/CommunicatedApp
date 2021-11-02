
using DatingApp.Controllers;
using DatingApp.DTOs;
using DatingApp.Models;
using DatingApp.ViewModel.CommentViewModel;
using System;
using System.Collections.Generic;

namespace DatingApp.ViewModel.BookViewModel
{
    public class BookViewModel 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.Now;

        public string Description { get; set; }

        public bool? Private { get; set; } = false;
        //Navigation Properties
        public CategoryViewModel  CategoryVM { get; set; }
        public IList<CommentDto> CommentDto { get; set; }

        public IList<ImageDto> ImageDto { get; set; }
    }
}