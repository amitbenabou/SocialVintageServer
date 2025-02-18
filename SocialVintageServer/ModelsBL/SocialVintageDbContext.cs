using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

public partial class SocialVintageDbContext : DbContext
{
    public User? GetUser(string email)
    {
        return this.Users.Where(u => u.UserMail == email)
                            .FirstOrDefault();
    }

    public Item? GetItem(int id)
    {
        return this.Items.Where(it => it.ItemId== id).Include(it => it.Store).Include(it=>it.ItemsImages)
                            .FirstOrDefault();
    }
}

