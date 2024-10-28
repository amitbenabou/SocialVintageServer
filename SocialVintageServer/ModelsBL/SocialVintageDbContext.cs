using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

public partial class SocialVintageDbContext : DbContext
{
    public User? GetUser(string email)
    {
        return this.Users.Where(u => u.UserMail == email)
                            .Include(u => u.Pswrd)
                            .FirstOrDefault();
    }
}

