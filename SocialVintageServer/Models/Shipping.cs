using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

[Table("Shipping")]
public partial class Shipping
{
    [Key]
    public int OptionId { get; set; }

    [StringLength(50)]
    public string OptionName { get; set; } = null!;

    [InverseProperty("Option")]
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
