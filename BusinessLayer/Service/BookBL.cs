using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Commands.Interface;
using RepositoryLayer.CustomException;
using RepositoryLayer.Entity;
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

        public BookBL(IBookCommand bookCommand)
        {
            _bookCommand = bookCommand;
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
    }
}
