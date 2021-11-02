using DatingApp.Models;
using DatingApp.ViewModel.BookViewModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DatingApp.Services.Book
{
    public interface IBookServices
    {
        void CreateBookUrl(IList<Image> images, List<IFormFile> image);
    }
}
