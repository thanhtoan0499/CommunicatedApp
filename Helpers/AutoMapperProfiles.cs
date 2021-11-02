using AutoMapper;
using DatingApp.Controllers;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Extensions;
using DatingApp.Models;
using DatingApp.ViewModel.BookViewModel;
using DatingApp.ViewModel.Cart;
using DatingApp.ViewModel.CommentViewModel;
using System.Linq;

namespace DatingApp.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, AppUser>()
                .ForMember(s => s.UserName, o => o.MapFrom(s => s.Username))
                .ForSourceMember(s=>s.Password, o=>o.DoNotValidate());
            CreateMap<AppUser, MemberDto>();
            //Category
            CreateMap<CategoryDto, Category>();
            CreateMap<CategoryViewModel, Category>();
            CreateMap<Category, CategoryViewModel>();
            //Comment
            CreateMap<CommentDto, Comment>();
            CreateMap<Comment, CommentDto>();
            //Image
            CreateMap<Image, ImageDto>();
            //Book
            CreateMap<BookDto, Book>();
            CreateMap<UpdateBookViewModel, Book>();
            CreateMap<Book, BookViewModel>()
                .ForMember(s => s.CategoryVM, o => o.MapFrom(s => s.Category))
                .ForMember(s => s.CommentDto, o => o.MapFrom(s => s.Comments))
                .ForMember(s => s.ImageDto, o => o.MapFrom(s => s.Images));
            //Cart
            CreateMap<Cart, CartViewModel>();
            CreateMap<Cart, CartDeleteByIdCartDto>();
        }
    }
}
