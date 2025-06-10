using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

[PrimaryKey("UserId", "ItemId")]
[Table("WishListItem")]
public partial class WishListItem
{
    [Key]
    public int UserId { get; set; }

    [Key]
    public int ItemId { get; set; }

    public bool FakeCol { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("WishListItems")]
    public virtual Item Item { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("WishListItems")]
    public virtual User User { get; set; } = null!;
}
