using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class CartRL : ICartRL
    {
        private readonly ApplicationDBContext _context;
        public CartRL(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<CartEntity> AddToCartAsync(CartML model, int userId) 
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == model.BookId);

            if (book == null)
            {
                throw new CartException("Book doesnot exists");
            }

            CartEntity newCart;

            if ((book.Quantity - model.Quantity) >= 0)
            {
                newCart = new CartEntity()
                {
                    BookId = model.BookId,
                    UserId = userId,
                    Quantity = model.Quantity,
                    IsPurchased = false
                };

                await _context.Carts.AddAsync(newCart);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CartException("Books out of stock");
            }

            return newCart;
        }

        public async Task<CartEntity> UpdateCartQuantityAsync(CartML model, int userId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == model.BookId);

            if (book == null)
            {
                throw new CartException("Book doesnot exists");
            }

            var existingCart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && c.BookId == model.BookId);
            if (existingCart == null)
            {
                throw new CartException("Book doesnot exists inside cart");
            }

            if ((book.Quantity - model.Quantity) >= 0)
            {
                existingCart.Quantity = model.Quantity; 
                
                _context.Carts.Update(existingCart);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CartException("Books out of stock");
            }

            return existingCart;
        }


        public async Task<CartEntity> RemoveItemFromCartAsync(int userId, int bookId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(b => b.UserId == userId && b.BookId == bookId);

            if(cart == null)
            {
                throw new CartException("No such item exists inside cart");
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return cart;
        }

        public async Task<List<CartEntity>> GetItemListFromCartAsync(int userId)
        {
            var cartList = await _context.Carts.Where(c => c.UserId == userId).Include(c => c.Book).ToListAsync();   

            if( cartList == null)
            {
                throw new CartException("No Items exists inside carts");
            }

            return cartList;
        }
    }
}
