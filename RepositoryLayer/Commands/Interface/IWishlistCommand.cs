﻿using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Commands.Interface
{
    public interface IWishlistCommand
    {
        public Task<WishlistEntity> AddToWishlistAsync(WishlistML model, int userId);
        public Task<WishlistEntity> RemoveFromWishlistasync(WishlistML model, int userId);
    }
}
