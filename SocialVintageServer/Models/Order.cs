using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

[Table("Order")]
public partial class Order
{
    [Key]
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateOnly OrderDate { get; set; }

    public int StatusId { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("StatusId")]
    [InverseProperty("Orders")]
    public virtual Status Status { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Orders")]
    public virtual User User { get; set; } = null!;
}
