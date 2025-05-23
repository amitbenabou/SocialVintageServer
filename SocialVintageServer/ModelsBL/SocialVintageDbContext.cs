﻿using System;
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
    
    public List<Store> GetAllStores()
    {
        return this.Stores.ToList();
    }

    public Store? GetStoreById (int id)
    {
        return this.Stores.Where(s=> s.StoreId == id).FirstOrDefault();
    }

    public List<Item>? GetItemsByStoreId(int id)
    {
        return this.Items.Where(i => i.StoreId == id).ToList();
    }

    //public ShoppingCartItem? GetCartItem(int id)
    //{
    //    return this.ShoppingCartItems.Where(it => it.CartItemId == id).Include(it => it.Store).Include(it => it.ItemsImages)
    //                        .FirstOrDefault();
    //}
}

