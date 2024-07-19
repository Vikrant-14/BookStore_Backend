using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.CustomException
{
    public class WishlistException : Exception
    {
        public WishlistException(string message) : base(message) { }    
    }
}
