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
    public class OrderRL : IOrderRL
    {
        private readonly ApplicationDBContext _context;

        public OrderRL(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<string> PlacedOrder(OrderML model, int userId)
        {
            var cartList = await _context.Carts.Where(c => c.UserId == userId && c.IsPurchased == false).Include(c => c.Book).ToListAsync();
            OrderEntity newOrder;
            if (cartList.Any())
            {
                foreach(var cart in cartList)
                {
                    newOrder = new OrderEntity() 
                    {
                        UserId = userId,
                        CustomerDetailsId = model.CustomerDetailsId,
                        CartId = cart.Id,
                        Quantity = cart.Quantity,
                        TotalPrice = cart.Quantity * cart.Book.DiscountedPrice
                    };

                    await _context.Orders.AddAsync(newOrder);
                    await _context.SaveChangesAsync();

                    var existingBook = await _context.Books.FindAsync(cart.Book.Id);
                    existingBook.Quantity = existingBook.Quantity - cart.Quantity;

                    _context.Books.Update(existingBook);
                    await _context.SaveChangesAsync();

                    cart.IsPurchased = true;
                    _context.Carts.Update(cart);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                throw new OrderException("No items inside cart");
            }

            return $"User Id : {userId} your Order placed successfully";
        }


        public async Task<List<OrderEntity>> GetAllPlacedOrderByUserId(int userId)
        {
            var orderList = await _context.Orders
                                          .Include(o => o.User)
                                          .Include(o => o.CustomerDetails)
                                          .Include(o => o.Cart)
                                          .ThenInclude(c => c.Book) 
                                          .Where(o => o.UserId == userId)
                                          .ToListAsync();

            if (orderList.Count == 0)
            {
                throw new OrderException("No order placed yet!!!");
            }

            return orderList;
        }

    }
}
