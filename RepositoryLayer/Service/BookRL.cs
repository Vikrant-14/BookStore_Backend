using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class BookRL : IBookRL
    {
        public ApplicationDBContext _context { get; }
        public BookRL(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<BookEntity> AddBookAsync(BookML model, int adminId)
        {
            var newBook = new BookEntity()
            {
                BookName = model.BookName,
                Author = model.Author,
                Description = model.Description,
                Price = model.Price,
                DiscountedPrice = model.DiscountedPrice,
                Quantity = model.Quantity,
                BookImage = model.BookImage,
                createdAt = DateTime.Now,
                UserEntityId = adminId
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook;
        }
    }
}
