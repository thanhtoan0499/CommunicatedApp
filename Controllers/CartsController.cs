using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingApp.Data;
using DatingApp.Models;
using DatingApp.Services;
using System.Security.Claims;
using DatingApp.ViewModel.ApiResponse;
using AutoMapper;
using DatingApp.ViewModel.Cart;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly CartServices cartServices;
        private readonly IMapper mapper;

        public CartsController(DataContext context, CartServices cartServices, IMapper mapper)
        {
            _context = context;
            this.cartServices = cartServices;
            this.mapper = mapper;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult> GetCartsByIdUser()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return Unauthorized(new Response() { StatusCode = 401, Message = "Loi phan quyen" });
            }
            var intUsertId = Int32.Parse(userId.ToString());
            var cart = await _context.Carts
                .Where(c => c.UserId == intUsertId)
                .Include(u => u.AppUser)
                .Include(b => b.Book)
                .ToListAsync();
            return Ok(new Response(mapper.Map<List<CartViewModel>>(cart)));
        }


        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(CartPostVM cartPostVM)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
          
            if(userId is null)
            {
                return Unauthorized(new Response() {StatusCode = 401, Message="Loi phan quyen" });
            }
            var intUsertId = Int32.Parse(userId.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response() { StatusCode = 400, Message = "Bad request"});
            }

            Book book = await _context.Books.FindAsync(cartPostVM.BookId);
            if(book == null)
            {
                return BadRequest(new Response("Khong tim thay sach", StatusType.NOT_FOUND) { });
            }

            if(book.Quantity - cartPostVM.Amount <= 0)
            {
                return Ok(new Response("So luong co han", StatusType.BAD_REQUEST));//so luong con lai book.Quantity
            }

            Cart CurrentCart = await _context.Carts
                .Where(c => c.UserId == intUsertId)
                .Where(c => c.BookId == cartPostVM.BookId)
                .FirstOrDefaultAsync();
            if (CurrentCart is not null)
            {
                if (cartPostVM.Amount < 0  && cartPostVM.Amount - CurrentCart.Amount > 0)
                {
                    CurrentCart.Amount -= cartPostVM.Amount;
                    await _context.SaveChangesAsync();
                    List<Cart> carts = await _context.Carts.Where(u=>u.UserId == intUsertId).ToListAsync();
                    return Ok(new Response(carts));
                }
                if (cartPostVM.Amount < 0 && cartPostVM.Amount + CurrentCart.Amount <= 0)
                {
                    List<Cart> carts = await cartServices.DeleteCartById(CurrentCart);
                    return Ok(new Response(carts));
                }

                if (CurrentCart.Amount >= book.Quantity)
                {
                    return BadRequest(new Response("So luong co han", StatusType.BAD_REQUEST));
                }

                CurrentCart.Amount = CurrentCart.Amount + cartPostVM.Amount;
                if (await cartServices.UpdateAsync(CurrentCart))
                {
                    return Ok(new Response(await cartServices.GetCartFromUser(intUsertId)));
                }
                else
                {
                    return Ok(new Response("Loi cap nhat gio hang", StatusType.BAD_REQUEST));
                }
            }
            else
            {
                if (cartPostVM.Amount <= 0)
                {
                    return Ok(new Response("So luong khong hop le", StatusType.BAD_REQUEST));
                }
                Cart cart = new Cart
                {
                    UserId = intUsertId,
                    BookId = cartPostVM.BookId,
                    Amount = cartPostVM.Amount,
                };
                if (await cartServices.AddNewCardAsync(cart))
                {
                    return Ok( new Response(await cartServices.GetCartFromUser(intUsertId)));
                }
                else
                {
                    return Ok(new Response("Loi them gio hang", StatusType.SERVER_ERROR));
                }
            }
        }

        // DELETE: api/Carts/5
        [HttpDelete]
        public async Task<IActionResult> DeleteCart()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return Unauthorized(new Response() { StatusCode = 401, Message = "Loi phan quyen" });
            }
            var intUsertId = Int32.Parse(userId.ToString());
            var cart = await _context.Carts.Where(x => x.UserId == intUsertId).ToListAsync();
            _context.Carts.RemoveRange(cart);
            await _context.SaveChangesAsync();

            return Ok(new Response("Xoa thanh cong"));
        }

        [HttpDelete]
        [Route("DeleteById/{id}")]
        public async Task<IActionResult> DeleteCartByIdCart(int idCart)
        {
            Cart carts = await _context.Carts.FindAsync(idCart);
            if (carts is null)
            {
                return NotFound();
            }
            else
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId is null)
                {
                    return Unauthorized(new Response() { StatusCode = 401, Message = "Loi phan quyen" });
                }
                var intUsertId = Int32.Parse(userId.ToString());
                var cart = await _context.Carts.Where(x => x.UserId == intUsertId)
                    .Where(c => c.Id == idCart)
                    .ToListAsync();
                _context.Carts.RemoveRange(cart);
                await _context.SaveChangesAsync();

                return Ok(new Response("Xoa thanh cong"));
            }
        }
    }
}
