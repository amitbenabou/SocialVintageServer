﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

[Table("User")]
[Index("UserMail", Name = "UQ__User__52ABC69B9AB4934A", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(50)]
    public string UserMail { get; set; } = null!;

    [StringLength(50)]
    public string Pswrd { get; set; } = null!;

    [StringLength(50)]
    public string UserAdress { get; set; } = null!;

    public bool HasStore { get; set; }

    [StringLength(50)]
    public string PhoneNumber { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("StoreNavigation")]
    public virtual Store? Store { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<WishListItem> WishListItems { get; set; } = new List<WishListItem>();
}
