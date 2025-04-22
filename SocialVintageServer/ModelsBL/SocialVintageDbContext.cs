using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

public partial class SocialVintageDbContext : DbContext
{
    public User? GetUser(string email)
    {
        return this.Users.Include(u=>u.Items).ThenInclude(it=>it.ItemsImages).Where(u => u.UserMail == email)
                            .FirstOrDefault();
    }

    public Item? GetItem(int id)
    {
        return this.Items.Where(it => it.ItemId== id).Include(it => it.Store).Include(it=>it.ItemsImages)
                            .FirstOrDefault();
    }

    //public ShoppingCartItem? GetCartItem(int id)
    //{
    //    return this.ShoppingCartItems.Where(it => it.CartItemId == id).Include(it => it.Store).Include(it => it.ItemsImages)
    //                        .FirstOrDefault();
    //}
}

