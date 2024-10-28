using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

[Table("Store")]
public partial class Store
{
    [Key]
    public int StoreId { get; set; }

    [StringLength(50)]
    public string StoreName { get; set; } = null!;

    [StringLength(50)]
    public string Adress { get; set; } = null!;

    public int OptionId { get; set; }

    [StringLength(5)]
    public string LogoExt { get; set; } = null!;

    public int CatagoryId { get; set; }

    [ForeignKey("CatagoryId")]
    [InverseProperty("Stores")]
    public virtual Catagory Catagory { get; set; } = null!;

    [InverseProperty("Store")]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    [ForeignKey("OptionId")]
    [InverseProperty("Stores")]
    public virtual Shipping Option { get; set; } = null!;

    [InverseProperty("Store")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [ForeignKey("StoreId")]
    [InverseProperty("Store")]
    public virtual User StoreNavigation { get; set; } = null!;

    [ForeignKey("StoreId")]
    [InverseProperty("Stores")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
