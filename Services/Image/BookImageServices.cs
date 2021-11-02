using DatingApp.Extensions;
using DatingApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Services
{
    public class BookImageServices
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public BookImageServices(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        internal  Task SaveImageList(Models.Book book, List<IFormFile> image, string userId)
        {
            //foreach (var file in image)
            for(int i = 0;i<image.Count;i++)
            {
                if(image[i].Length > 0)
                {
                    long time = DateTimeOffset.Now.ToUnixTimeSeconds();
                    string imageName = (time+i).ToString()+"_" + userId + "_" + image[i].FileName;
                    //string imageName = new String(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '-');
                    //imageName = imageName + DateTime.Now.ToString("yymmssffff") + Path.GetExtension(file.FileName);
                    var imagePath = Path.Combine(webHostEnvironment.WebRootPath, "images", imageName);
                    image[i].CopyToAsync(new FileStream(imagePath, FileMode.Create));
                    //using (var fs = new FileStream(imagePath, FileMode.Create))
                    //{
                    //    image[i].CopyToAsync(fs);
                    //}
                    if (book.Images == null) book.Images = new List<Image>();
                    
                        book.Images.Add(new Image
                        {
                            Url = (imageName).ToString()
                        });
                    book.Images[0].IsMain = true;
                }
            }
            return Task.CompletedTask;
        }
    }
}
