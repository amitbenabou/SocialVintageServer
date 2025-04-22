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

    public int CatagoryId { get; set; }

    [ForeignKey("CatagoryId")]
    [InverseProperty("Stores")]
    public virtual Catagory Catagory { get; set; } = null!;

    [InverseProperty("Store")]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    [ForeignKey("OptionId")]
    [InverseProperty("Stores")]
    public virtual Shipping Option { get; set; } = null!;

    [ForeignKey("StoreId")]
    [InverseProperty("Store")]
    public virtual User StoreNavigation { get; set; } = null!;
}
