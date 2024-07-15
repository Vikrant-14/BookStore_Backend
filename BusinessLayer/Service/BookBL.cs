using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Commands.Interface;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class BookBL : IBookBL
    {
        private IBookCommand _bookCommand;
        private IBookQuery _bookQuery;

        public BookBL(IBookCommand bookCommand, IBookQuery bookQuery)
        {
            _bookCommand = bookCommand;
            _bookQuery = bookQuery;
        }

        public async Task<BookEntity> AddBookAsync(BookML model, int adminId)
        {
            try
            {
               var book = await _bookCommand.AddBookAsync(model, adminId);

                return book;
            }
            catch (BookException)
            {
                throw;
            }
        }

        public async Task<BookEntity> UpdateBookAsync(int bookId, BookML model, int adminId)
        {
            try
            {
                return await _bookCommand.UpdateBookAsync(bookId, model, adminId);
            }
            catch (BookException)
            {
                throw;
            }
        }

        public async Task<BookEntity> DeleteBookAsync(int bookId)
        {
            try
            {
                return await _bookCommand.DeleteBookAsync(bookId);
            }
            catch (BookException)
            {
                throw;
            }
        }

        public async Task<BookEntity> GetBookByIdAsync(int bookId)
        {
            try
            {
                return await _bookQuery.GetBookByIdAsync(bookId);
            }
            catch(BookException)
            {
                throw;
            }
        }

        public async Task<List<BookEntity>> GetAllBookAsync()
        {
            try
            {
                return await _bookQuery.GetAllBookAsync();
            }
            catch (BookException)
            {
                throw;
            }
        }
    }
}
