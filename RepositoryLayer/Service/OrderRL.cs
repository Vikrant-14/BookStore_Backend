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
            var cartList = await _context.Carts
                .Where(c => c.UserId == userId && !c.IsPurchased)
                .Include(c => c.Book)
                .ToListAsync();

            if (!cartList.Any())
            {
                throw new OrderException("No items inside cart");
            }

            var newOrders = new List<OrderEntity>();
            var updatedBooks = new List<BookEntity>();
            var updatedCarts = new List<CartEntity>();

            foreach (var cart in cartList)
            {
                var newOrder = new OrderEntity
                {
                    UserId = userId,
                    CustomerDetailsId = model.CustomerDetailsId,
                    CartId = cart.Id,
                    Quantity = cart.Quantity,
                    TotalPrice = cart.Quantity * cart.Book.DiscountedPrice
                };

                newOrders.Add(newOrder);

                var existingBook = cart.Book;
                existingBook.Quantity -= cart.Quantity;
                updatedBooks.Add(existingBook);

                cart.IsPurchased = true;
                updatedCarts.Add(cart);
            }

            await _context.Orders.AddRangeAsync(newOrders);
            _context.Books.UpdateRange(updatedBooks);
            _context.Carts.UpdateRange(updatedCarts);

            await _context.SaveChangesAsync();

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
