using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

[Table("OrderItem")]
public partial class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int ItemId { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("OrderItems")]
    public virtual Item Item { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order Order { get; set; } = null!;
}
