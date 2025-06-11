using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

public partial class SocialVintageDbContext : DbContext
{

    //פעולה מקבלת מייל של משתמש ומחזירה את המשתמש כולו
    public User? GetUser(string email)
    {
        return this.Users.Include(u=>u.WishListItems).
            ThenInclude(it => it.Item).ThenInclude(it=>it.ItemsImages).Where(u => u.UserMail == email)
                            .FirstOrDefault();
    }

    //מקבלת מספר של פריט ומחזירה את כל פרטי הפריט
    public Item? GetItem(int id)
    {
        return this.Items.Where(it => it.ItemId== id).Include(it => it.Store).Include(it=>it.ItemsImages)
                            .FirstOrDefault();
    }
    
    //פעולה מחזירה רשימה של כל החנויות מבסיס הנתונים
    public List<Store> GetAllStores()
    {
        return this.Stores.ToList();
    }


    //פעולה מקבלת id של משתמש ומחזירה את החנות שלו
    public Store? GetStoreById (int id)
    {
        return this.Stores.Where(s=> s.StoreId == id).FirstOrDefault();
    }

    //פעולה מקבלת id של חנות ומחזירה את הפריטים של החנות
    public List<Item>? GetItemsByStoreId(int id)
    {
        return this.Items.Where(i => i.StoreId == id).Where(ii => ii.IsAvailable).Include(it => it.ItemsImages).ToList();
    }

    //מקבלת id של משתמש ומחזירה את הפריטים שנמצאים בוויש ליסט שלו. 
    //public List<Item>? GetWishListByUsereId(int id)
    //{
    //    return this.Users.Where(i => i.Items == id).ToList();
    //}

    //public ShoppingCartItem? GetCartItem(int id)
    //{
    //    return this.ShoppingCartItems.Where(it => it.CartItemId == id).Include(it => it.Store).Include(it => it.ItemsImages)
    //                        .FirstOrDefault();
    //}
}

