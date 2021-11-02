using DatingApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.Services.Book
{
    public class BookServices
    {
        internal Task CreateBookUrl(Models.Book book, List<IFormFile> image)
        {
            // Check anh co hop le hay khong
            if (image == null) return Task.FromResult(0);
            if (image.Count == 0) return Task.FromResult(0);
            image.ForEach(img =>
            {
                if (false)
                {
                }
                return;
            });

            if (book.Images == null) book.Images = new List<Image>();
            // Dat ten cho anh
            long url = DateTimeOffset.Now.ToUnixTimeSeconds();
            for (int i = 0;i< image.Count; i++)
            {
                book.Images.Add(new Image
                {
                    Url = (url + i).ToString(),
                });
            }

            //Anh chinh
            book.Images[0].IsMain = true;
            return Task.CompletedTask;
        }
    }
}
