using ModelLayer;
using RepositoryLayer.Commands.Interface;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Service
{
    public class BookCommand : IBookCommand
    {
        private IBookRL _bookRL { get; }
        public BookCommand(IBookRL bookRL)
        {
            _bookRL = bookRL;
        }


        public async Task<BookEntity> AddBookAsync(BookML model, int adminId)
        {
            try
            {
                var book = await _bookRL.AddBookAsync(model,adminId);

                return book;
            }
            catch (BookException)
            {
                throw;
            }
        }

    }
}
