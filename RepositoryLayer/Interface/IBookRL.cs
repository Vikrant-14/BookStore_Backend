using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        public Task<BookEntity> AddBookAsync(BookML model, int adminId);
        public Task<BookEntity> UpdateBookAsync(int bookId, BookML model, int adminId);
        public Task<BookEntity> DeleteBookAsync(int bookId);
        public Task<BookEntity> GetBookByIdAsync(int bookId);
        public Task<List<BookEntity>> GetAllBookAsync();
    }
}
