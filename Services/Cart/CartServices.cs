using DatingApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Services
{
    public class CartServices 
    {
        public readonly DataContext dataContext;
        public CartServices(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        internal Task<List<Cart>> DeleteCartById(Cart currentCart)
        {
            dataContext.Remove(currentCart);
            dataContext.SaveChangesAsync();
            return dataContext.Carts
                .Where(u => u.UserId == currentCart.UserId)
                .Include(b => b.Book)
                .Include(u => u.AppUser)
                .ToListAsync();
        }
        
        internal async Task<bool> UpdateAsync(Cart getCartByUserId)
        {
            dataContext.Entry(getCartByUserId).State = EntityState.Modified;
            return await dataContext.SaveChangesAsync() != 0;
        }
        
        internal async Task<IList<Cart>> GetCartFromUser(int UseId)
        {
            return await dataContext.Carts
                .Include(b => b.Book)
                .Where(c => c.UserId == UseId)
                .ToListAsync();
        }

        internal async Task<bool> AddNewCardAsync(Cart cart)
        {
            dataContext.Carts.Add(cart);
            return await dataContext.SaveChangesAsync() != 0;
        }
    }
}
