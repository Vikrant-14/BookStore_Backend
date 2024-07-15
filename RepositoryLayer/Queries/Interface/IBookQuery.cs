using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Queries.Interface
{
    public interface IBookQuery
    {
        public Task<BookEntity> GetBookByIdAsync(int bookId);
        public Task<List<BookEntity>> GetAllBookAsync();
    }
}
