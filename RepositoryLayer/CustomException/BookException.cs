﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.CustomException
{
    public class BookException : Exception
    {
        public BookException(string message) : base(message) { }
    }
}
