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

    [ForeignKey("OrderId")]
    [InverseProperty("Order")]
    public virtual Item OrderNavigation { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Orders")]
    public virtual User User { get; set; } = null!;
}
