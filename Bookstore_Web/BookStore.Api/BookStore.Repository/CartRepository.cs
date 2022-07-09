using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class CartRepository : BaseRepository
    {
        
        public ListResponse<Cart> GetCart(int userId)
        {
            var query = _context.Carts.Where(c => c.Userid == userId);
            int totalRecords = query.Count();
            List<Cart> cart = query.ToList();

            foreach (Cart cartItem in cart)
            {
                cartItem.Book = _context.Books.FirstOrDefault(c => c.Id == cartItem.Bookid);
            }

            return new ListResponse<Cart>()
            {
                Results = cart,
                TotalRecords = totalRecords,
            };
        }

        public Cart GetCarts(int id)
        {
            return _context.Carts.FirstOrDefault(c => c.Id == id);
        }

        public Cart AddCart(Cart category)
        {
            var entry = _context.Carts.Add(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Cart UpdateCart(Cart category)
        {
            var entry = _context.Carts.Update(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCart(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            if(cart == null)
            {
                return false;
            }

            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }
    }
}
