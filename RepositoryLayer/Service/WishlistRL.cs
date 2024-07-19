using Microsoft.Data.SqlClient;
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
    public class WishlistRL : IWishlistRL
    {
        private readonly ApplicationDBContext _context;

        public WishlistRL(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<WishlistEntity> AddToWishlistAsync(WishlistML model, int userId)
        {
            var book = await _context.Books.FindAsync(model.BookId);

            if (book == null)
            {
                throw new WishlistException($"Book ID : {model.BookId} doesnot exists");
            }

            WishlistEntity wishlist = new WishlistEntity() 
            {
                UserId = userId,
                BookId = model.BookId
            };
            
            await _context.Wishlists.AddAsync(wishlist);
            await _context.SaveChangesAsync();

            return wishlist;
        }

        public async Task<WishlistEntity> RemoveFromWishlistasync(WishlistML model, int userId)
        {
            var existingWishlist = await _context.Wishlists.FirstOrDefaultAsync(w => w.UserId == userId && w.BookId == model.BookId);

            if(existingWishlist == null)
            {
                throw new WishlistException("Book doesnot exists in wishlist");
            }

            _context.Wishlists.Remove(existingWishlist);
            await _context.SaveChangesAsync();

            return existingWishlist;
        }

        public async Task<List<WishlistWithBookDetailsDto>> GetAllItemFromWishlistByUserIdAsync(int userId)
        {
            var userIdParameter = new SqlParameter("@UserId", userId);
            var wishList = await _context.Set<WishlistWithBookDetailsDto>()
                                         .FromSqlRaw("EXEC GetWishlistByUserId @UserId", userIdParameter)
                                         .ToListAsync();

            if (wishList.Count == 0)
            {
                throw new WishlistException("Wishlist is empty!!!");
            }

            return wishList;
        }
    }
}
