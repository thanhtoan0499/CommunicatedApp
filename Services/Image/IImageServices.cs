using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.Services
{
    public interface IImageServices
    {
        Task SaveImageList(Models.Book book, List<IFormFile> image);
    }
}
