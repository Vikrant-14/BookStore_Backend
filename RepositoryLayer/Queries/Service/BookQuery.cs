using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.Service
{
    public class BookQuery : IBookQuery
    {
        private readonly IBookRL _bookRL;

        public BookQuery(IBookRL bookRL)
        {
            _bookRL = bookRL;
        }

        public async Task<BookEntity> GetBookByIdAsync(int bookId)
        {
            try
            {
                return await _bookRL.GetBookByIdAsync(bookId);
            }
            catch (BookException)
            {
                throw;
            }
        }

        public async Task<List<BookEntity>> GetAllBookAsync()
        {
            try
            {
                return await _bookRL.GetAllBookAsync();
            }
            catch (BookException)
            {
                throw;
            }
        }
    }
}
