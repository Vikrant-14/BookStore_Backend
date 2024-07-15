using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<BookEntity> UpdateBookAsync(int bookId, BookML model, int adminId)
        {
            var existingBook = await _context.Books.FindAsync(bookId);

            if (existingBook == null)
            {
                throw new BookException($"Book Id : {bookId} doesnot exists");   
            }

            existingBook.BookName = model.BookName;
            existingBook.Description = model.Description;
            existingBook.Price = model.Price;
            existingBook.DiscountedPrice = model.DiscountedPrice;
            existingBook.Author = model.Author;
            existingBook.Quantity = model.Quantity;
            existingBook.BookImage = model.BookImage;
            existingBook.UserEntityId = adminId;

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();

            return existingBook;
        }

        public async Task<BookEntity> DeleteBookAsync(int bookId)
        {
            var existingBook = await _context.Books.FindAsync(bookId);

            if (existingBook == null)
            {
                throw new BookException($"Book Id : {bookId} doesnot exists");
            }

            _context.Books.Remove(existingBook); 
            await _context.SaveChangesAsync();

            return existingBook;
        }

        public async Task<BookEntity> GetBookByIdAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);

            if( book == null)
            {
                throw new BookException($"Book Id : {bookId} doesnot exists");
            }

            return book;
        }

        public async Task<List<BookEntity>> GetAllBookAsync()
        {
            var books = await _context.Books.ToListAsync();

            if (books == null)
            {
                throw new BookException($"Books doesnot exists");
            }

            return books;
        }
    }
}
