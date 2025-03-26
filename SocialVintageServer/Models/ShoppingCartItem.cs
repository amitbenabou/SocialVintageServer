using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

[Table("ShoppingCartItem")]
public partial class ShoppingCartItem
{
    [Key]
    public int CartItemId { get; set; }

    public int UserId { get; set; }

    public int ItemId { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("ShoppingCartItems")]
    public virtual Item Item { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("ShoppingCartItems")]
    public virtual User User { get; set; } = null!;
}
