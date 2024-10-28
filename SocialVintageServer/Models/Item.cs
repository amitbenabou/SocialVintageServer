using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

[Table("Item")]
public partial class Item
{
    [Key]
    public int ItemId { get; set; }

    [StringLength(50)]
    public string Size { get; set; } = null!;

    [StringLength(50)]
    public string Brand { get; set; } = null!;

    [StringLength(50)]
    public string Color { get; set; } = null!;

    [StringLength(50)]
    public string Price { get; set; } = null!;

    public int StoreId { get; set; }

    [StringLength(100)]
    public string ItemInfo { get; set; } = null!;

    [ForeignKey("StoreId")]
    [InverseProperty("Items")]
    public virtual Store Store { get; set; } = null!;
}
